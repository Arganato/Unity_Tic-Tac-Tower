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
	
	private static SkillDescription buildDescr = new SkillDescription(TowerType.build);
	private static SkillDescription shootDescr = new SkillDescription(TowerType.shoot);
	private static SkillDescription silenceDescr = new SkillDescription(TowerType.silence);
	private static SkillDescription skillDescr = new SkillDescription(TowerType.skillCap);
	private static SkillDescription activeDescr;
	private InfoWindow infoText = new InfoWindow();
	private TutorialHeader header;
	
	private Rect tutorialWindowRect = new Rect(Screen.width/2-150,40,300,400);
	private bool showTutorialWindow = true;
	
	public static TowerType tutorialType{
        get { return towerTut; }
        set {
			towerTut = value;
			switch(towerTut){
			case TowerType.build:
				activeDescr = buildDescr;
				break;
			case TowerType.shoot:
				activeDescr = shootDescr;
				break;
			case TowerType.silence:
				activeDescr = silenceDescr;
				break;
			case TowerType.skillCap:
				activeDescr = skillDescr;
				break;
			default:
				Debug.LogError("Tried to access invalid skill tutorial");
				break;
			}
		}
    }
	public static void StartTutorial(){
		Stats.startState = new GameState();
		chapter = Chapter.intro;
	}
	public static void SetTutorialBuild1(){
		Stats.startState.SetTutorialBuild1();
		Stats.skillEnabled.SetAll(false);
		Stats.skillEnabled.SetStraight(true);
		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
	}
	public static void SetTutorialBuild2(){
		Stats.startState.SetTutorialBuild2();
		Stats.skillEnabled.SetAll(true);
		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
	}
	public static void SetTutorialShoot1(){
		Stats.startState.SetTutorialShoot1();
		Stats.skillEnabled.SetAll(false);
		Stats.skillEnabled.SetStraight(true);
		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
	}
	public static void SetTutorialShoot2(){
		Stats.startState.SetTutorialShoot2();
		Stats.skillEnabled.SetAll(true);
		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
	}
	public static void SetTutorialSilence1(){
		Stats.startState.SetTutorialSilence1();
		Stats.skillEnabled.SetAll(false);
		Stats.skillEnabled.SetStraight(true);
		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
	}
	public static void SetTutorialSilence2(){
		Stats.startState.SetTutorialSilence2();
		Stats.skillEnabled.SetAll(true);
		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
	}
	public static void SetTutorialPower1(){
		Stats.startState.SetTutorialPower1();
		Stats.skillEnabled.SetAll(false);
		Stats.skillEnabled.SetStraight(true);
		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
	}
	public static void SetTutorialPower2(){
		Stats.startState.SetTutorialPower2();
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
	
	void Update(){
		//if(changeChapter && chapter == 0 && menuWidth > 200){
		//	menuWidth -= Mathf.RoundToInt(Time.deltaTime*menuChangeSpeed);
		//}
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
//		if(chapter == Chapter.tutStr  || chapter == Chapter.tutDiag){
//			//do something with case...
//			switch(towerTut){
//			case TowerType.build:
//				//For Build Skill Tutorial.
//				break;
//			case TowerType.shoot:
//				//For Shoot Skill Tutorial.
//				break;
//			case TowerType.silence:
//				//For Silence Skill Tutorial.
//				break;
//			case TowerType.skillCap:
//				//For Skill Skill Tutorial.
//				break;
//			default:
//				Debug.LogError("Tried to access invalid skill tutorial");
//				break;
//			}
//		}
		
	}	
	
	public void CheckSolution(){
		SolutionChecker.CheckSolution(chapter,towerTut);
	}
	
	private void TutorialWindow(int windowID){
		GUILayout.BeginArea(new Rect(0,0,tutorialWindowRect.width,20));
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if(GUILayout.Button("x")){
			showTutorialWindow = false;
		}
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
		//----end header----//
		GUI.BeginGroup(new Rect(0,20,tutorialWindowRect.width,tutorialWindowRect.height-45));
		if(chapter == Chapter.intro){
			activeDescr.PrintGUI();
		}else{
			infoText.PrintTutorialText();
		}
		GUI.EndGroup();
		if(GUI.Button(new Rect(tutorialWindowRect.width-105,tutorialWindowRect.height-25,100,25),"Continue")){
			DoContinue();
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
			break;
		case Chapter.tutStr:
			camera.animation.Play("anim2");
			chapter = Chapter.textDiag;
			tutorialGUI.enable = false;
			break;
		case Chapter.textDiag:
			chapter = Chapter.tutDiag;
			camera.animation.Play("anim1");
			SetTutorial();
			control.StartNewGame();
			break;
		case Chapter.tutDiag:
			camera.animation.Play("anim2");
			chapter = Chapter.end;
			tutorialGUI.enable = false;
			break;
		case Chapter.end:
			Application.LoadLevel("MainMenu");
			break;
		}
	}
}
