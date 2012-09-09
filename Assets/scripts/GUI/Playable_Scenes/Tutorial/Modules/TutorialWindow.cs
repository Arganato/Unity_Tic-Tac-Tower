using UnityEngine;
using System.Collections;

public class TutorialWindow {
	
	private Transform camera;
	private bool solutionAccepted = true;
	private Rect tutorialWindowRect = new Rect(Screen.width/2-150,40,300,400);
	private bool showTutorialWindow = true;
	private TutorialScene tutorialScene;
	private InfoWindow infoText = new InfoWindow();
	private Control control;
	
	public TutorialWindow(TutorialScene tutScene, Transform cam, Control control){
		camera = cam;
		tutorialScene = tutScene;
		this.control = control;
	}
	
	public void PrintGUI(){
		if(showTutorialWindow){
			GUI.Window(5,tutorialWindowRect,Window,"Tutorial");
		}else{
			if(GUI.Button(new Rect(0,0,100,25),"Open window")){
				showTutorialWindow = true;
			}

		}
	}
	
	public void SolutionAccepted(){
		solutionAccepted = true;
		DoContinue();
	}
	
	private void Window(int windowID){
//		GUILayout.BeginArea(new Rect(0,0,tutorialWindowRect.width,20));
//		GUILayout.BeginHorizontal();
//		GUILayout.FlexibleSpace();
//		if(GUILayout.Button("x")){
//			showTutorialWindow = false;
//		}
//		GUILayout.EndHorizontal();
//		GUILayout.EndArea();
		if(GUI.Button(new Rect(5,tutorialWindowRect.height-25,tutorialWindowRect.width-110,25),"Close")){
			showTutorialWindow = false;
		}
		//----end header----//
		GUI.BeginGroup(new Rect(0,20,tutorialWindowRect.width,tutorialWindowRect.height-45));
		infoText.PrintTutorialText();
		GUI.EndGroup();
		if(solutionAccepted){
			if(GUI.Button(new Rect(tutorialWindowRect.width-105,tutorialWindowRect.height-25,100,25),"Continue")){
				DoContinue();
			}
		}
	}

	private void DoContinue(){
		// gameGUI settes false, men aldri true igjen...
		switch(Tutorial.chapter){
		case Tutorial.Chapter.intro:
			Tutorial.chapter = Tutorial.Chapter.textStr;
			break;
		case Tutorial.Chapter.textStr:
			Tutorial.chapter = Tutorial.Chapter.tutStr;
			camera.animation.Play("anim1");
			Tutorial.SetTutorial();
			tutorialScene.EnableGameGUI(true);
			control.StartNewGame();
			solutionAccepted = false;
			break;
		case Tutorial.Chapter.tutStr:
			showTutorialWindow = true;
			camera.animation.Play("anim2");
			Tutorial.chapter = Tutorial.Chapter.textDiag;
			tutorialScene.EnableGameGUI(false);
			Control.cState.activePlayer = 0;
			break;
		case Tutorial.Chapter.textDiag:
			Tutorial.chapter = Tutorial.Chapter.tutDiag;
			camera.animation.Play("anim1");
			Tutorial.SetTutorial();
			tutorialScene.EnableGameGUI(true);
			control.StartNewGame();
			solutionAccepted = false;
			break;
		case Tutorial.Chapter.tutDiag:
			showTutorialWindow = true;
			camera.animation.Play("anim2");
			Tutorial.chapter = Tutorial.Chapter.end;
			tutorialScene.EnableGameGUI(false);
			break;
		case Tutorial.Chapter.end:
			Application.LoadLevel("MainMenu");
			break;
		}
	}
}
