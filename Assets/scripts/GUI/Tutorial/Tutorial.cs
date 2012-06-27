using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	public GUISkin skin;
	new public Transform camera;
	public int border = 0;

	private Control control;
	
	//camera positions
	
	int buttonWidth = 100;
	int buttonHeight = 40;
	
	//private Vector3 camPreviewPos = new Vector3(-30,150,35);
	//private Vector3 camPreviewRot = new Vector3(90,0,0);
	
	//event points
	
	public
	enum Chapter {intro, textStr, tutStr, textDiag, tutDiag, end};
	public static Chapter chapter;
	public static TowerType towerTut;	//Used to know which tutorial is run. TowerType.five is used when none is run.
	public static GameState tutorialState;
	
	private static SkillDescription buildDescr = new SkillDescription(TowerType.build);
	private static SkillDescription shootDescr = new SkillDescription(TowerType.shoot);
	private static SkillDescription silenceDescr = new SkillDescription(TowerType.silence);
	private static SkillDescription skillDescr = new SkillDescription(TowerType.skillCap);
	private static SkillDescription activeDescr;
	private InfoWindow infoText = new InfoWindow();
	
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
		Stats.skillEnabled.build = true;
		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
	}
	
	public static void SetTutorialBuild2(){
		Stats.startState.SetTutorialBuild2();
		Stats.skillEnabled.build = true;
		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
	}
	
	public static void SetTutorialShoot1(){
		Stats.startState.SetTutorialShoot1();
		Stats.skillEnabled.shoot = true;
		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
	}
	
	public static void SetTutorialShoot2(){
		Stats.startState.SetTutorialShoot2();
		Stats.skillEnabled.shoot = true;
		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
	}
	
	public static void SetTutorialSilence1(){
		Stats.startState.SetTutorialSilence1();
		Stats.skillEnabled.silence = true;
		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
	}
	
	public static void SetTutorialSilence2(){
		Stats.startState.SetTutorialSilence2();
		Stats.skillEnabled.silence = true;
		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
	}
	
	public static void SetTutorialPower1(){
		Stats.startState.SetTutorialPower1();
		Stats.skillEnabled.skillCap = true;
		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
	}
	
	public static void SetTutorialPower2(){
		Stats.startState.SetTutorialPower2();
		Stats.skillEnabled.skillCap = true;
		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
	}
	
	public static void SetTutorial(){
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
	}
	
	//Stats.startState.SetTutorialBuild1();
	//Stats.skillEnabled.SetAll(false);
	//Stats.skillEnabled.build = true;
	
	void Start () {
		control = (Control)FindObjectOfType(typeof(Control));
	}
	void Update(){
		//if(changeChapter && chapter == 0 && menuWidth > 200){
		//	menuWidth -= Mathf.RoundToInt(Time.deltaTime*menuChangeSpeed);
		//}
	}
	
	void OnGUI(){
		
		GUI.skin = skin;
		switch(chapter){
		case Chapter.intro:
			activeDescr.PrintGUI();
			if(GUI.Button(new Rect(Screen.width/2-buttonWidth/2, Screen.height-buttonHeight-border, buttonWidth, buttonHeight), "Continue")){
				chapter = Chapter.textStr;
			}
			break;
		case Chapter.textStr:
			infoText.PrintTutorialText();
			if(GUI.Button(new Rect(Screen.width/2-buttonWidth/2, Screen.height-buttonHeight-border, buttonWidth, buttonHeight), "Continue")){
				chapter = Chapter.tutStr;
				camera.animation.Play("anim1");
				SetTutorial();
				control.StartNewGame();
			}
			break;
		case Chapter.tutStr:
			if(GUI.Button(new Rect(Screen.width/2-buttonWidth/2, Screen.height-buttonHeight-border, buttonWidth, buttonHeight), "Continue")){
				camera.animation.Play("anim2");
				chapter = Chapter.textDiag;
			}
			infoText.PrintTutorialText();
			switch(towerTut){
			case TowerType.build:
				//For Build Skill Tutorial.
				break;
			case TowerType.shoot:
				//For Shoot Skill Tutorial.
				break;
			case TowerType.silence:
				//For Silence Skill Tutorial.
				break;
			case TowerType.skillCap:
				//For Skill Skill Tutorial.
				break;
			default:
				Debug.LogError("Tried to access invalid skill tutorial");
				break;
			}
			break;
		case Chapter.textDiag:
			infoText.PrintTutorialText();
			if(GUI.Button(new Rect(Screen.width/2-buttonWidth/2, Screen.height-buttonHeight-border, buttonWidth, buttonHeight), "Continue")){
				chapter = Chapter.tutDiag;
				camera.animation.Play("anim1");
				SetTutorial();
				control.StartNewGame();
			}
			break;
		case Chapter.tutDiag:
			if(GUI.Button(new Rect(Screen.width/2-buttonWidth/2, Screen.height-buttonHeight-border, buttonWidth, buttonHeight), "Continue")){
				camera.animation.Play("anim2");
				chapter = Chapter.end;
			}
			infoText.PrintTutorialText();
			switch(towerTut){
			case TowerType.build:
				//For Build Skill Tutorial.
				break;
			case TowerType.shoot:
				//For Shoot Skill Tutorial.
				break;
			case TowerType.silence:
				//For Silence Skill Tutorial.
				break;
			case TowerType.skillCap:
				//For Skill Skill Tutorial.
				break;
			default:
				Debug.LogError("Tried to access invalid skill tutorial");
				break;
			}
			break;
		case Chapter.end:
			if(GUI.Button(new Rect(Screen.width/2-buttonWidth/2, Screen.height-buttonHeight-border, buttonWidth, buttonHeight), "Finish.")){
				Application.LoadLevel("MainMenu");
			}
			infoText.PrintTutorialText();
			break;
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
}
