using UnityEngine;
using System.Collections;

public class SkillTutorial : IScenarioDescription{
	
	private ScenarioDescriptionGUI tutorialWindow;
	private Control control;
	TutorialScene tutorialScene;
	
	public SkillTutorial(TutorialScene scene, Control control){
		tutorialScene = scene;
		this.control = control;
	}
	
	public GameGUIOptions GetGUIOptions(){
		SkillEnabled guiButtonsEnabled = new SkillEnabled();
		guiButtonsEnabled.SetAll(false);
		guiButtonsEnabled.shoot = true;
		guiButtonsEnabled.diagShoot = true;
		GameGUIOptions guiOptions = GameGUIOptions.Create(guiButtonsEnabled,false);
		Stats.skillEnabled = guiButtonsEnabled;
		return guiOptions;
	}
	
	public TutorialCondition GetCondition()
	{
		return new TutorialSkillCondition((IScenarioDescription)this, tutorialScene);
	}
	
	public void Start(){
		Debug.Log("starting tutorialWindow");
		tutorialWindow = tutorialScene.GetTutorialGUI();
		PropagateTutorial(tutorialScene.tutorialStep);
	}
	
	public void OnContinue ()
	{
		PropagateTutorial(tutorialScene.tutorialStep);
	}
	
	public void OnFinished ()
	{
		Application.LoadLevel("mainMenu");
	}
	
	private void CreateTriangleState(){
		Control.cState.field = new Field<Route>(Route.empty);
		control.ExecuteTurn(new Turn(new FieldIndex(4,5)));
		Control.cState.activePlayer = 0;
		control.ExecuteTurn(new Turn(new FieldIndex(5,5)));
		Control.cState.activePlayer = 0;
		control.ExecuteTurn(new Turn(new FieldIndex(5,4)));
		Control.cState.activePlayer = 0;		
		Control.SetUndoPoint(Control.cState);
	}
	private void CreateDiagTriangleState(){
		Control.cState.field = new Field<Route>(Route.empty);
		control.ExecuteTurn(new Turn(new FieldIndex(5,5)));
		control.ExecuteTurn(new Turn(new FieldIndex(5,4)));
		control.ExecuteTurn(new Turn(new FieldIndex(4,4)));
		Control.cState.activePlayer = 0;
		control.ExecuteTurn(new Turn(new FieldIndex(6,4)));
		Control.cState.activePlayer = 0;
		Control.SetUndoPoint(Control.cState);		
	}
	private void CreateSilenceState(){
		Control.cState.player[0].playerSkill.Reset();
		Field<Route> f = new Field<Route>(Route.empty);
		f[4,4] = Route.red;
		f[5,4] = Route.red;
		f[4,5] = Route.red;
		f[6,5] = Route.red;
		f[2,3] = Route.red;
		f[2,2] = Route.red;
		f[2,4] = Route.red;
		f[2,5] = Route.red;
		Control.cState.field = f;
		Control.cState.player[1].playerSkill.silence = 1;
		Control.cState.activePlayer = 1;
		Control.cState.playerDone = true;
		Order o = Order.Create(new FieldIndex(5,5),SkillType.silence,true);
		control.ExecuteOrder(o);
	}
	
	private void CreatePowerState(){
		Field<Route> f = new Field<Route>(Route.empty);
		f[6,7] = Route.red;
		f[6,6] = Route.red;
		f[7,7] = Route.red;
		f[3,2] = Route.red;
		f[5,2] = Route.red;
		Control.cState.player[0].playerSkill.Reset();
		Control.cState.field = f;
		Control.cState.player[0].playerSkill.build = 10;
		Control.cState.playerDone = true;
		control.UserEndTurn();
		Control.cState.activePlayer = 0;
		Control.SetUndoPoint(Control.cState);
	}
	
	private void CreateCombTowerState(){
		Control.cState.player[0].playerSkill.Reset();
		Field<Route> f = new Field<Route>(Route.empty);
		f[3,5] = Route.red;
		f[3,4] = Route.red;
		f[3,3] = Route.red;
		f[5,4] = Route.red;
		Control.cState.field = f;
		Control.cState.playerDone = true;
		control.UserEndTurn();
		Control.cState.activePlayer = 0;
		Control.SetUndoPoint(Control.cState);
	}
	
	private void BuildGUI(){
		SkillEnabled skl = new SkillEnabled();
		skl.SetAll(false);
		skl.build = true;
		GameGUIOptions opt = GameGUIOptions.Create(skl,false);
		tutorialScene.ChangeGUIOptions(opt);
		Stats.skillEnabled = skl;
	}
	private void SilenceGUI(){
		SkillEnabled skl = new SkillEnabled();
		skl.SetAll(true);
		skl.skillCap = false;
		skl.diagSkillCap = false;
		GameGUIOptions opt = GameGUIOptions.Create(skl,false);
		tutorialScene.ChangeGUIOptions(opt);
		Stats.skillEnabled = skl;
		Control.cState.player[0].playerSkill.Reset();
		Control.SetUndoPoint(Control.cState);
	}
	private void PowerGUI(){
		GameGUIOptions opt = GameGUIOptions.Create(SkillEnabled.AllActive(),false);
		tutorialScene.ChangeGUIOptions(opt);
		Stats.skillEnabled.SetAll(true);
	}

	public void PropagateTutorial(int step){
		switch(step){
		case 0:
			tutorialWindow.AddNote("Welcome to this Tic-Tac Tower tutorial. In this tutorial we will cover the four skills;" +
				" what they do and how to get them.");
			tutorialWindow.ShowContinue(true); //enables the continue-button, which enables the user to move forward
			tutorialWindow.ShowFinish(false); //the finish-button ends the tutorial
			tutorialScene.enableControl = false; //the user may not place pieces on the board (yet)
			break;
		case 1:
			tutorialWindow.AddPicture(ResourceFactory.GetSkillTexture(0));
			tutorialWindow.AddMission("Build a shoot-tower", "This shoot-tower is known from the first tutorial, but it is also possible to build the tower up side down, or flipped to the left or right." +
				" Use this knowledge to finish the tower below. (End turn when finished)");
			CreateTriangleState();
			tutorialScene.enableControl = true;
			tutorialWindow.ShowContinue(false);
			break;
		case 2:
			if(Control.cState.player[0].playerSkill.shoot == 1){
				tutorialWindow.AddMission("Mission Completed!","Well done!\nNow lets look at the diagonal version of the " +
					"shoot-tower.",true);
				tutorialWindow.ShowContinue(true);
				tutorialScene.enableControl = false;
			}else{
				PopupMessage.DisplayMessage("Wrong move! Try again.");
				CreateTriangleState();
				tutorialScene.tutorialStep -= 1; //do this step again
			}
			break;
		case 3:
			CreateDiagTriangleState();
			tutorialWindow.AddPicture(ResourceFactory.GetSkillTexture(4));
			tutorialWindow.AddMission("Build a diagonal shoot tower","Above, you can see a diagonal shoot tower. Like the " +
				"straight tower, this can also be built with any rotation. Build a diagonal tower now. (End turn when finished)");
			tutorialScene.enableControl = true;
			tutorialWindow.ShowContinue(false);
			break;
		case 4:
			if(Control.cState.player[0].playerSkill.shoot == 2){
				tutorialWindow.AddMission("Mission Completed!","Well done!\nNo matter what kind of shoot-tower you build, " +
					"the skill it gives is the same: Shoot.\nNext, we will look at the three other skills in the game.");
				tutorialWindow.ShowContinue(true);
				tutorialScene.enableControl = false;
			}else{
				PopupMessage.DisplayMessage("Wrong move! Try again.");
				CreateDiagTriangleState();
				tutorialScene.tutorialStep -= 1; //do this step again
			}
			break;
		case 5:
			tutorialWindow.AddNote("All of the skills can be acquired by building the corresponding tower, and like shoot, there are two " +
				"kind of towers for each skill, which can be built in any orientation.");
			break;
			
		case 6:
			//***********BUILD************//
			tutorialWindow.AddNote("Build:");
			tutorialWindow.AddPicture(ResourceFactory.GetSkillIcon(1));
			tutorialWindow.AddNote("The next skill is Build. It can be used to place an extra piece one turn. " +
				"Use it by clicking the skill button with the icon above.");
			BuildGUI();
			break;
		case 7:
			tutorialWindow.AddPicture(ResourceFactory.GetSkillTexture(1));
			tutorialWindow.AddMission("Construct a Build-tower", "Now, construct a tower like the one above");
			tutorialScene.enableControl = true;
			tutorialWindow.ShowContinue(false);
			break;
		case 8: //condition: build is built
			tutorialWindow.AddMission("Mission Completed!","Well done!",true);
			tutorialWindow.ShowContinue(true);
			break;
		case 9:
			tutorialWindow.AddMission("Place an extra piece","Now use the skill to place an extra piece on the board.");
			tutorialWindow.ShowContinue(false);
			break;
		case 10:
			control.UserEndTurn();
			Control.cState.activePlayer = 0;
			Control.SetUndoPoint(Control.cState);
			tutorialWindow.AddMission("Mission Completed!","Well done!",true);
			tutorialWindow.ShowContinue(true);
			tutorialScene.enableControl = false;
			break;
		case 11:
			Stats.skillEnabled.build = false;
			Stats.skillEnabled.diagBuild = true;
			tutorialWindow.AddPicture(ResourceFactory.GetSkillTexture(5));
			tutorialWindow.AddNote("Like the shoot-tower, the build-tower also has a diagonal version which you can see above. " +
				"You may test this version. When you're done move to the next part.");
			tutorialScene.enableControl = true;
			break;

		case 12:
			//*********SILENCE**********//
			tutorialScene.enableControl = false;
			SilenceGUI();
			tutorialWindow.AddNote("Silence:");
			tutorialWindow.AddPicture(ResourceFactory.GetSkillIcon(2));
			tutorialWindow.AddNote("The next skill we shall look at is silence. " +
				"Silence stops the opponent from building any towers in the next turn. This is done " +
				"by not allowing him to place pieces where they would make a tower. More on this later.");
			break;
		case 13:
			tutorialWindow.AddPicture(ResourceFactory.GetSkillTexture(6));
			tutorialWindow.AddPicture(ResourceFactory.GetSkillTexture(2));			
			tutorialWindow.AddMission("Construct a silence tower", "First lets look at how the tower is made. " +
				"Above you can see pictures of the two different kinds of silence-towers. Construct one of them to get a silence-skill");
			tutorialScene.enableControl = true;
			tutorialWindow.ShowContinue(false);
			break;
		case 14: //Condition: silence-tower constructed
			tutorialWindow.AddMission("Mission completed", "Well done!\nNext, we will look at what happens when silence is used. " +
				"In the next scenario, blue will use a silence, and we will look at the effect.",true);
			tutorialWindow.ShowContinue(true);
			tutorialScene.enableControl = false;
			break;
		case 15:
			CreateSilenceState();
			tutorialWindow.AddMission("Experiment with the effect of silence", "Blue player used silence! " +
				"This stops you from placing your piece in places where it would build a tower. Experiment with this to get a feeling of " +
				"where you cannot place. When you're done, do a legal move and end your turn.");
			tutorialScene.enableControl = true;
			tutorialWindow.ShowContinue(false);
			break;
		case 16:
			tutorialWindow.AddMission("Mission completed!", "Nice work! As you see, the silence skill can block quite a few positions, " +
				"and it also blocks 5-in-a-row. It does not stop the player from using any skills he already has, though, " +
				"like throwing a silence back.",true);
			tutorialWindow.ShowContinue(true);
			tutorialScene.enableControl = false;
			break;
		case 17:
			//**********POWER**********//
			PowerGUI();
			tutorialWindow.AddNote("Power");
			tutorialWindow.AddPicture(ResourceFactory.GetSkillIcon(3));
			tutorialWindow.AddNote("In the last part of this tutorial we will look at the power-skill. Power, unlike the other skills, " +
				"is a passive skill. You need power to use many of the other skills in the same turn");
			break;
		case 18:
			tutorialWindow.AddNote("You start the game with one power. That means that you are able to use 1 shoot, 1 build and 1 silence " +
				"on the same turn. If you build another power-tower you can use 2 of each of the skills in one turn");
			break;
		case 19:
			CreatePowerState();
			tutorialWindow.AddPicture(ResourceFactory.GetSkillTexture(3));
			tutorialWindow.AddPicture(ResourceFactory.GetSkillTexture(7));
			tutorialWindow.AddMission("Make power-towers to be able to use builds", "Above you can see the different power-towers. In the following scenario you can have many builds, but " +
				"power is limiting how many you can use. Use builds and make power-towers to be able to use more builds. (End turn when done)");
			tutorialScene.enableControl = true;
			tutorialWindow.ShowContinue(false);
			break;
		case 20: //condition: endturn
			if(Control.cState.player[0].playerSkill.skillCap == 2){
				tutorialWindow.AddMission("Mission Completed!", "Good! The last thing we shall look at in this tutorial is that it is possible to " +
					"combine towers to get more skills from your pieces. In the next scenario, place a piece in the given position " +
					"to create several skills with one move.",true);
				tutorialWindow.ShowContinue(true);
				tutorialScene.enableControl = false;
			}else{
				CreatePowerState();
				PopupMessage.DisplayMessage("Make 2 Power-towers");
				tutorialScene.tutorialStep--;
			}
			break;
		case 21:
			CreateCombTowerState();
			tutorialWindow.AddMission("Build a combined tower", "Place a piece in the given position to create one shoot and two builds in one go. (End turn when finished)");
			tutorialScene.FlashBoard(new FieldIndex(4,4));
			tutorialScene.enableControl = true;
			tutorialWindow.ShowContinue(false);
			break;
		case 22: //condition: combined tower made
			if(Control.cState.player[0].playerSkill.shoot >= 1 && Control.cState.player[0].playerSkill.build >= 2){
				tutorialWindow.AddMission("Mission completed!", "Well done! There are almost an infinite number of combinations of towers " +
					"that exists, many of which brings unique tactical advantages in different situations. Experiment to find more combinations on your own.",true);
				tutorialWindow.ShowContinue(true);
			}else{
				CreateCombTowerState();
				PopupMessage.DisplayMessage("place your piece on the given position to create a combined tower");
				tutorialScene.FlashBoard(new FieldIndex(4,4));
			}
			break;
		case 23:
			tutorialWindow.AddNote("This concludes this tuturial. You should now be ready to try your own games.");
			tutorialWindow.ShowFinish(true);
			break;
		}
		tutorialScene.tutorialStep++;
	}
}
