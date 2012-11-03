using UnityEngine;
using System.Collections;

public class BasicTutorial : IScenarioDescription{
	
	private ScenarioDescriptionGUI tutorialWindow;
	TutorialScene tutorialScene;
	
	public BasicTutorial(TutorialScene scene){
		tutorialScene = scene;
	}
	
	public void SetGUI(ScenarioDescriptionGUI guiwindow){
		tutorialWindow = guiwindow;
	}
	
	public void Start(){
		Debug.Log("starting tutorialWindow");
		PropagateTutorial(tutorialScene.tutorialStep);
	}
	
	public void OnContinue ()
	{
		Debug.Log("Continue called");
		PropagateTutorial(tutorialScene.tutorialStep);
	}
	
	public void OnFinished ()
	{
		throw new System.NotImplementedException ();
	}
	
//	IEnumerable<int> PropagateTutorial(){
//		tutorialWindow.AddMission("This is a mission","This is a mission that should be carried out like this");
//		tutorialWindow.ShowContinue(true);
//		tutorialWindow.ShowFinish(false);
////		yield break;
//		yield return 0;
//		tutorialWindow.AddNote("This is a note");
//	}
	
	public void PropagateTutorial(int step){
		switch(step){
		case 0:
			tutorialWindow.AddNote("Welcome to this Tic-Tac Tower tutorial. Start the tutorial by pressing the " +
				"continue-button below.");
			tutorialWindow.ShowContinue(true);
			tutorialWindow.ShowFinish(false);
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
			//picture of shoot-tower
			tutorialWindow.AddNote("Above you can see the tower you need to " +
				"construct to get the shoot-skill. Now, minimize this window by pressing " +
				"the arrow, and build a shoot tower on the board below");
			//Flash arrow-button
			tutorialWindow.AddMission("Build shoot-tower","Build a shoot-tower by placing " +
				"pieces on the board in the given pattern");
			break;
		case 4:
			PopupMessage.DisplayMessage("You must end your turn by pressing the end-turn button " +
				"before you can place a new piece");
			//flash end turn button
			break;
		case 5:
			tutorialWindow.AddNote("Congratulations, you built the shoot-tower, The tower is " +
				"consumed, and a skill is added to your skill list. You can build the shoot-tower " +
				"in any rotation, or even diagonally.");
			//animation of shoot-tower
			break;
		case 6:
			tutorialWindow.AddNote("The shoot-skill we just aquired can be used to destroy one of your " +
				"opponents pieces on the board (or even your own, if you'd want that). " +
				"The skill is symbolized by its icon");
			//picture of shoot-icon
			break;
		case 7:
			tutorialWindow.AddNote("Now select the shoot skill by pressing the button with the " +
				"shoot-icon, and then shoot the blue piece on the board by clicking it.");
			//blink shoot icon
			//blink board
			break;
		case 8:
			tutorialWindow.AddNote("Well done! If you forget what a skill does, or how its tower looks, " +
				"you can always get a description by activating the help menu by " +
				"clicking the \"?\"-button. This turns all the skill-buttons into a menu " +
				"where you can read about all the skills. To close the help menu and " +
				"return the function of the buttons to normal, simply click the \"?\"-" +
				"button again. Try to open the help for the shoot skill now.");
			tutorialWindow.AddMission("Open the shoot-help","Open the shoot-help");
			//blink the "?"-button
			break;
		case 9:
			//blink shoot button
			break;
		case 10:
			tutorialWindow.AddMission("Close the shoot-help","And close it by clicking the \"?\"-button again");
			break;
		case 11:
			tutorialWindow.AddNote("Congratulations, that completes the first part of the tutorial. The " +
				"next part will look at each of the four skills of the game in more detail.");
			break;
		case 12:
			Debug.Log("Tutorial is finished");
			break;
		}
		
		tutorialScene.tutorialStep++;
	}	
}
