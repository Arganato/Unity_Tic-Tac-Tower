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
			tutorialWindow.AddMission("Build a shoot-tower", "First out is shoot, that you know from the basic tutorial. Build a shoot-tower now");
			
			break;
		case 2:

			break;
		case 3:

			break;
		case 4:
			break;
		case 5:
			break;
		case 6:
			break;
		case 7:
			break;
		case 8:
			break;
		case 9:
			break;
		case 10:
			break;
		case 11:
			break;
		}
		
		tutorialScene.tutorialStep++;
	}	
}
