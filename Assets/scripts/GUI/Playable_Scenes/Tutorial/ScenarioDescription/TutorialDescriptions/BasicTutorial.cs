using UnityEngine;
using System.Collections;

public class BasicTutorial : IScenarioDescription{
	
	private ScenarioDescriptionGUI tutorialWindow;
	private Control control;
	TutorialScene tutorialScene;
	
	public BasicTutorial(TutorialScene scene, Control control){
		tutorialScene = scene;
		this.control = control;
	}
	
	public GameGUIOptions GetGUIOptions(){
		SkillEnabled guiButtonsEnabled = new SkillEnabled();
		guiButtonsEnabled.SetAll(false);
		guiButtonsEnabled.shoot = true;
		GameGUIOptions guiOptions = GameGUIOptions.Create(guiButtonsEnabled,false);
		Stats.skillEnabled = guiButtonsEnabled;
		return guiOptions;
	}
	
	public TutorialCondition GetCondition()
	{
		return new TutorialBasicCondition((IScenarioDescription)this, tutorialScene);
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
	
	public void PropagateTutorial(int step){
		switch(step){
		case 0:
			tutorialWindow.AddNote("Welcome to this Tic-Tac Tower tutorial. Start the tutorial by pressing the " +
				"continue-button below.");
			tutorialWindow.ShowContinue(true);
			tutorialWindow.ShowFinish(false);
			tutorialScene.enableControl = false;
			break;
		case 1:
			tutorialWindow.AddNote("Tic-Tac Tower is a turn based board game like Tic-Tac-Toe, where the " +
				"goal is to get 5 pieces in a row");
			//animation of 5-in-a-row on the board
			break;
		case 2:
			tutorialWindow.AddNote("But in addition, you can build towers that gives you special " +
				"advantages, called skills. We will go through each of them later, but " +
				"for now, lets look at the shoot-skill as an example.");
			break;
		case 3:
			tutorialWindow.AddPicture(ResourceFactory.GetSkillTexture(0));
			tutorialWindow.FlashArrow((MonoBehaviour)control);
			tutorialWindow.AddMission("Build shoot-tower","Above you can see the tower you need to " +
				"construct to get the shoot-skill. Now, minimize this window by pressing " +
				"the arrow on the top of the window, and build a shoot tower on the board below.");
			tutorialWindow.ShowContinue(false);
			tutorialScene.enableControl = true;
			break;
		case 4:
			PopupMessage.DisplayMessage("End your turn with the end-turn button");
			tutorialScene.FlashEndTurnButton();
			break;
		case 5:
			control.UserEndTurn();
			Control.cState.field[4,4] = Route.empty; //making sure 4,4 is empty, so that a blue piece can be placed there
			control.UserFieldSelect(new FieldIndex(4,4));
			control.UserEndTurn();
			if(!tutorialWindow.isMaximized){
				tutorialWindow.FlashArrow((MonoBehaviour)control);
			}
			tutorialWindow.AddMission("Mission Completed!","Congratulations, you built the shoot-tower, The tower is " +
				"consumed, and a skill is added to your skill list. You can build the shoot-tower " +
				"in any rotation, or even diagonally.");
			tutorialWindow.ShowContinue(true);
			tutorialScene.enableControl = false;
			//animation of shoot-tower
			break;
		case 6:
			tutorialWindow.AddNote("The shoot-skill we just aquired can be used to destroy a piece on the board. " +
				"The skill is symbolized by its icon");
			tutorialWindow.AddPicture(ResourceFactory.GetSkillIcon(0));
			break;
		case 7:

			tutorialScene.enableControl = true;
			tutorialWindow.ShowContinue(false);
			tutorialWindow.AddMission("Shoot the blue piece", "Now select the shoot skill by pressing the button with the " +
				"shoot-icon, and then shoot the blue piece on the board by clicking it.");
			tutorialScene.FlashSkillButton(0);
			tutorialScene.FlashBoard(new FieldIndex(4,4));
			break;
		case 8:
			if(!tutorialWindow.isMaximized){
				tutorialWindow.FlashArrow((MonoBehaviour)control);
			}
			tutorialWindow.AddMission("Mission Completed!","Well done! If you forget what a skill does, or how its tower looks, " +
				"you can always get a description by clicking the \"?\"-button."); 
				tutorialWindow.AddNote("This turns all the " +
				"skill-buttons into a menu where you can read about all the skills.");
			tutorialWindow.ShowContinue(true);
			break;
		case 9:
			tutorialWindow.AddNote("To close the help menu and " +
				"return the function of the buttons to normal, simply click the \"?\"-" +
				"button again.");
			tutorialWindow.AddMission("Open the shoot-help"," Try to open the help for the shoot skill now.");
			tutorialScene.FlashHelpButton();
			break;
		case 10:
			tutorialWindow.AddMission("Close the shoot-help","And close it by clicking the \"?\"-button again");
			break;
		case 11:
			tutorialWindow.AddNote("Congratulations, that completes the first part of the tutorial. The " +
				"next part will look at each of the four skills of the game in more detail.");
			tutorialWindow.ShowFinish(true);
			break;
		}
		
		tutorialScene.tutorialStep++;
	}	
}
