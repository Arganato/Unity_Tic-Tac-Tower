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
		Debug.Log(Tutorial.guiOptions.ToString());
		gui = GameGUIFactory.Create(Tutorial.guiOptions,(IGUIMessages)this);
		tutorialWindow = new TutorialWindow(this,gameCamera, control);
		gui.gameGUIEnabled = false;
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
