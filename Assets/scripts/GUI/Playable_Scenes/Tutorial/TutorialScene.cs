using UnityEngine;
using System.Collections;

public class TutorialScene : SceneTemplate {
	
	public GUISkin customSkin;	
	public bool enable;
	public Transform gameCamera;
	private TutorialWindow tutorialWindow;
	
	private GameGUIFactory gui;
	protected override void Start () {
		base.Start();
		gui = GameGUIFactory.Create(GameGUIOptions.Create(Stats.skillEnabled,false),(IGUIMessages)this);
		tutorialWindow = new TutorialWindow(this,gameCamera, control);
	}
	
	public void EnableGameGUI(bool b){
		gui.gameGUIEnabled = b;
	}
	
	public void OnVictory(){ //event sent from control
		tutorialWindow.SolutionAccepted();
		Stats.gameRunning = true;
	}
	
	//IGUIMessages
	
	public override void UserEndTurn (){
		if(Tutorial.CheckSolution()){
			tutorialWindow.SolutionAccepted();
		}
	}

	void OnGUI() {
		if(!enable){
			return;
		}
		GUI.skin = customSkin;
		
		gui.PrintGUI();
		tutorialWindow.PrintGUI();
		
//		if(Tutorial.chapter == Tutorial.Chapter.tutStr || Tutorial.chapter == Tutorial.Chapter.tutDiag){
//			skillGUI.PrintGUI();
//		}
//		if(buttonRow.PrintGUI()){
//			tutorial.CheckSolution();
//		}

		base.HandleMouseInput();
	}
}
