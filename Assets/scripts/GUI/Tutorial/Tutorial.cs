using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	public GUISkin skin;
	new public Transform camera;

	private Control control;
	private Tutorial_GUI tutorialGUI;

								//  Str = Straight
	public enum Chapter {intro, textStr, tutStr, textDiag, tutDiag, end};
	public static Chapter chapter;
	public static TowerType towerTut;	//Used to know which tutorial is run. TowerType.five is used when none is run.
	public static GameState tutorialState;

	private InfoWindow infoText = new InfoWindow();
	private TutorialHeader header;
	
	private bool solutionAccepted = true;
	private Rect tutorialWindowRect = new Rect(Screen.width/2-150,40,300,400);
	private bool showTutorialWindow = true;
	
	public static TowerType tutorialType{
        get { return towerTut; }
        set {towerTut = value; }
    }
	public static void StartTutorial(){
		Stats.startState = new GameState();
		chapter = Chapter.intro;
	}
	public static void SetTutorialBuild1(){
		SetTutorialBuild1(Stats.startState);
		Stats.skillEnabled.SetAll(false);
		Stats.skillEnabled.build = true;
		Stats.skillEnabled.five = true;
		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
	}
	public static void SetTutorialBuild2(){
		SetTutorialBuild2(Stats.startState);
		Stats.skillEnabled.SetAll(false);
		Stats.skillEnabled.build = true;
		Stats.skillEnabled.diagBuild = true;
		Stats.skillEnabled.five = true;
		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
	}
	public static void SetTutorialShoot1(){
		SetTutorialShoot1(Stats.startState);
		Stats.skillEnabled.SetAll(false);
		Stats.skillEnabled.shoot = true;
		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
	}
	public static void SetTutorialShoot2(){
		SetTutorialShoot2(Stats.startState);
		Stats.skillEnabled.SetAll(false);
		Stats.skillEnabled.shoot = true;
		Stats.skillEnabled.diagShoot = true;
		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
	}
	public static void SetTutorialSilence1(){
		SetTutorialSilence1(Stats.startState);
		Stats.skillEnabled.SetAll(false);
		Stats.skillEnabled.SetStraight(true);
		Stats.skillEnabled.skillCap = false;
		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
	}
	public static void SetTutorialSilence2(){
		SetTutorialSilence2(Stats.startState);
		Stats.skillEnabled.SetAll(true);
		Stats.skillEnabled.skillCap = false;
		Stats.skillEnabled.diagSkillCap = false;
		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
	}
	public static void SetTutorialPower1(){
		SetTutorialPower1(Stats.startState);
		Stats.skillEnabled.SetAll(false);
		Stats.skillEnabled.SetStraight(true);
		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
	}
	public static void SetTutorialPower2(){
		SetTutorialPower2(Stats.startState);
		Stats.skillEnabled.SetAll(true);
		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
	}
	public void SetTutorial(){
		Stats.startState = new GameState();
		switch(towerTut){
		case TowerType.build:
			if(chapter == Chapter.tutStr) SetTutorialBuild1();
			else if(chapter == Chapter.tutDiag) SetTutorialBuild2();
			break;
		case TowerType.shoot:
			if(chapter == Chapter.tutStr) SetTutorialShoot1();
			else if(chapter == Chapter.tutDiag) SetTutorialShoot2();
			break;
		case TowerType.silence:
			if(chapter == Chapter.tutStr) SetTutorialSilence1();
			else if(chapter == Chapter.tutDiag) SetTutorialSilence2();
			break;	
		case TowerType.skillCap:
			if(chapter == Chapter.tutStr) SetTutorialPower1();
			else if(chapter == Chapter.tutDiag) SetTutorialPower2();
			break;
		default:
			break;
		}
		tutorialGUI.enable = true;
	}
	
	void Start () {
		control = (Control)FindObjectOfType(typeof(Control));
		tutorialGUI = (Tutorial_GUI)FindObjectOfType(typeof(Tutorial_GUI));
		tutorialGUI.enable = false;
		header = new TutorialHeader(control);
	}
	
	
	void OnGUI(){
		
		GUI.skin = skin;

		if(showTutorialWindow){
			GUI.Window(5,tutorialWindowRect,TutorialWindow,"Tutorial");
		}else{
			if(GUI.Button(new Rect(0,0,100,25),"Open window")){
				showTutorialWindow = true;
			}

		}
		header.PrintGUI();
		
	}	
	
	public void CheckSolution(){
		if(SolutionChecker.CheckSolution(chapter,towerTut)){
			solutionAccepted = true;
			DoContinue();
		}else{
			PopupMessage.DisplayMessage("Wrong solution!\nUndo to try again");
		}
	}
	
	private void TutorialWindow(int windowID){
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
	
	/*
	private void SimpleChangeChapter(){
		Debug.Log("simple change chapter");
		camera.animation.Play("anim1");
		chapter++;
	}
	
	private IEnumerator ChangeChapter(){
		//check for end of section
		Debug.Log("changing chapter...");
		changeChapter = true;

		switch(chapter){
		case 0:
			camera.animation.Play("anim1");			
			break;
		case 1:
			camera.animation.Play("anim2");	
			GUI_script guis = (GUI_script)gameObject.GetComponent(typeof(GUI_script));
			guis.enable = true;
			break;
		case 2:

			break;
		}

		yield return new WaitForSeconds(2);
		changeChapter = false;
		chapter++;
	}
	 */


	private void DoContinue(){
		switch(chapter){
		case Chapter.intro:
			chapter = Chapter.textStr;
			break;
		case Chapter.textStr:
			chapter = Chapter.tutStr;
			camera.animation.Play("anim1");
			SetTutorial();
			control.StartNewGame();
			solutionAccepted = false;
			break;
		case Chapter.tutStr:
			showTutorialWindow = true;
			camera.animation.Play("anim2");
			chapter = Chapter.textDiag;
			tutorialGUI.enable = false;
			Control.cState.activePlayer = 0;
			break;
		case Chapter.textDiag:
			chapter = Chapter.tutDiag;
			camera.animation.Play("anim1");
			SetTutorial();
			control.StartNewGame();
			solutionAccepted = false;
			break;
		case Chapter.tutDiag:
			showTutorialWindow = true;
			camera.animation.Play("anim2");
			chapter = Chapter.end;
			tutorialGUI.enable = false;
			break;
		case Chapter.end:
			Application.LoadLevel("MainMenu");
			break;
		}
	}
	
	public void OnVictory(){ //event sent from control
		CheckSolution(); //assumed to check in
		Stats.gameRunning = true;
	}
	
	//****************Setup-Functions from gamestate***************//
	
	public static void SetTutorialBuild1(GameState state){	//Win during this round (red)
		state.field[5,4] = Route.red;
		state.field[3,4] = Route.blue;
		state.field[5,5] = Route.red;
		state.field[5,3] = Route.blue;
		state.field[4,6] = Route.red;
		state.field[6,3] = Route.blue;
		state.field[7,3] = Route.red;
		state.field[4,5] = Route.blue;
		state.field[7,4] = Route.red;
		state.field[7,5] = Route.blue;
		state.field[3,6] = Route.red;
		state.field[4,3] = Route.blue;
		state.field[3,3] = Route.red;
		state.field[3,5] = Route.blue;
		state.field[2,6] = Route.red;
		state.field[1,6] = Route.blue;
		state.player[0].playerSkill.skillCap = 1;
		state.player[1].playerSkill.skillCap = 1;
		state.placedPieces = 16;
	}
	
	public static void SetTutorialBuild2(GameState state){	//Win during this round (red)
		state.field[5,4] = Route.red;
		state.field[3,4] = Route.blue;
		state.field[4,5] = Route.red;
		state.field[3,6] = Route.blue;
		state.field[3,5] = Route.red;
		state.field[2,5] = Route.blue;
		state.field[2,6] = Route.red;
		state.field[2,4] = Route.blue;
		state.field[4,6] = Route.red;
		state.field[5,2] = Route.blue;
		state.field[3,3] = Route.red;
		state.field[6,1] = Route.blue;
		state.field[1,3] = Route.red;
		state.field[2,2] = Route.blue;
		state.field[2,1] = Route.red;
		state.field[3,2] = Route.blue;
		state.field[2,1] = Route.red;
		state.field[6,1] = Route.blue;
		state.field[1,0] = Route.red;
		state.field[5,5] = Route.blue;
		state.field[1,1] = Route.red;
		state.field[3,1] = Route.blue;
		state.player[0].playerSkill.skillCap = 1;
		state.player[1].playerSkill.skillCap = 1;
		state.placedPieces = 22;
	}
	
	public static void SetTutorialShoot1(GameState state){	//Stop red from winning next round (blue)
		state.field[5,4] = Route.blue;
		state.field[5,5] = Route.blue;
		state.field[5,6] = Route.blue;
		state.field[5,3] = Route.blue;
		state.field[3,4] = Route.red;
		state.field[3,5] = Route.red;
		state.field[3,6] = Route.red;
	}
	
	public static void SetTutorialShoot2(GameState state){	//Stop red from winning next round (blue)
		state.field[5,4] = Route.blue;
		state.field[3,4] = Route.red;
		state.field[5,5] = Route.blue;
		state.field[5,6] = Route.red;
		state.field[4,5] = Route.blue;
		state.field[3,5] = Route.red;
		state.field[3,6] = Route.blue;
		state.field[6,3] = Route.red;
		state.field[2,7] = Route.blue;
		state.field[1,8] = Route.red;
		state.field[7,2] = Route.blue;
		state.field[5,3] = Route.red;
		state.field[5,2] = Route.blue;
		state.field[6,5] = Route.red;
		state.field[6,2] = Route.blue;
		state.field[4,7] = Route.red;
		state.field[3,7] = Route.blue;
		state.field[7,3] = Route.red;
		state.field[4,2] = Route.blue;
		state.field[6,6] = Route.red;
		state.field[5,7] = Route.blue;
		state.field[2,8] = Route.red;
		state.field[5,8] = Route.blue;
		state.player[0].playerSkill.skillCap = 1;
		state.player[1].playerSkill.skillCap = 1;
		state.activePlayer = 0;
		state.placedPieces = 21;
	}
	
	public static void SetTutorialSilence1(GameState state){
		state.field[5,4] = Route.blue;
		state.field[5,5] = Route.blue;
		state.field[5,6] = Route.blue;
		state.field[5,3] = Route.blue;
		state.field[3,4] = Route.red;
		state.field[3,5] = Route.red;
		state.field[3,6] = Route.red;
		state.field[2,7] = Route.red;
	}
	
	public static void SetTutorialSilence2(GameState state){
		state.field[5,4] = Route.blue;
		state.field[3,4] = Route.red;
		state.field[5,5] = Route.blue;
		state.field[5,6] = Route.red;
		state.field[4,6] = Route.blue;
		state.field[4,5] = Route.red;
		state.field[2,3] = Route.blue;
		state.field[6,6] = Route.red;
		state.field[6,7] = Route.blue;
		state.field[7,6] = Route.red;
		state.field[6,3] = Route.blue;
		state.field[7,3] = Route.red;
		state.field[4,3] = Route.blue;
		state.field[6,4] = Route.red;
		state.field[2,4] = Route.blue;
		state.field[3,7] = Route.red;
		state.field[1,3] = Route.blue;
		state.field[7,2] = Route.red;
		state.field[3,5] = Route.blue;
	}
	
	public static void SetTutorialPower1(GameState state){
		state.field[5,2] = Route.red;
		state.field[4,2] = Route.red;
		state.field[5,5] = Route.red;
		state.field[4,5] = Route.red;
		state.field[4,6] = Route.red;

		state.field[3,3] = Route.blue;
		state.field[5,3] = Route.blue;
		state.field[4,3] = Route.blue;
		
		state.player[0].playerSkill.build = 2;
		state.player[0].playerSkill.skillCap = 0;
		state.player[1].playerSkill.build = 2;
		state.player[1].playerSkill.skillCap = 1;
		state.activePlayer = 0;
	}	
	
	public static void SetTutorialPower2(GameState state){
		state.field[5,4] = Route.blue;
		state.field[3,4] = Route.red;
		state.field[5,5] = Route.blue;
		state.field[4,5] = Route.red;
		state.field[6,4] = Route.blue;
		state.field[3,6] = Route.red;
		state.field[4,6] = Route.blue;
		state.field[7,3] = Route.red;
		state.field[3,7] = Route.blue;
		state.field[8,2] = Route.red;
		state.field[4,3] = Route.blue;
		state.field[6,5] = Route.red;
		state.field[2,4] = Route.blue;
		state.field[1,4] = Route.red;
		state.field[6,7] = Route.blue;
		state.field[1,6] = Route.red;
		state.field[1,5] = Route.blue;
		state.field[5,7] = Route.red;
		state.field[2,7] = Route.blue;
		state.field[6,6] = Route.red;
		state.field[2,6] = Route.blue;
		state.field[7,7] = Route.red;
		state.field[8,3] = Route.blue;
		state.player[0].playerSkill.build = 1;
		state.player[0].playerSkill.skillCap = 1;
	}
	
	
}
