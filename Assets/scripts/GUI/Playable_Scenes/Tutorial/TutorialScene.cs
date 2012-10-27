using UnityEngine;
using System.Collections;

public class TutorialScene : SceneTemplate{
	
	public GUISkin customSkin;
	public bool enable;
	public Transform gameCamera;
	
//	private TutorialWindow tutorialWindow;
	private GameGUIFactory gui;
	private ScenarioDescriptionGUI scenarioWindow;
	private BasicTutorial tutorialPropagator;
	
	protected override void Start () {
		base.Start();
		tutorialPropagator = new BasicTutorial();
		scenarioWindow = new ScenarioDescriptionGUI((IScenarioDescription)tutorialPropagator);
		tutorialPropagator.SetGUI(scenarioWindow);
		gui = GameGUIFactory.Create(Tutorial.guiOptions,(IGUIMessages)this);
//		tutorialWindow = new TutorialWindow(this,gameCamera, control);
		gui.gameGUIEnabled = false;
		tutorialPropagator.Start();
	}
	
	public void EnableGameGUI(bool b){
		gui.gameGUIEnabled = b;
	}
	
	public void OnVictory(){ //event sent from control
//		tutorialWindow.SolutionAccepted();
		Stats.gameRunning = true;
	}
	
	
	//IGUIMessages
	
	public override void UserEndTurn (){
		if(Tutorial.CheckSolution()){
//			tutorialWindow.SolutionAccepted();
		}
	}

	void OnGUI() {
		if(!enable){
			return;
		}
		GUI.skin = customSkin;
		
		gui.PrintGUI();
//		tutorialWindow.PrintGUI();
		scenarioWindow.PrintGUI();
		
//		if(Tutorial.chapter == Tutorial.Chapter.tutStr || Tutorial.chapter == Tutorial.Chapter.tutDiag){
//			skillGUI.PrintGUI();
//		}
//		if(buttonRow.PrintGUI()){
//			tutorial.CheckSolution();
//		}

		base.HandleMouseInput();
	}
}
