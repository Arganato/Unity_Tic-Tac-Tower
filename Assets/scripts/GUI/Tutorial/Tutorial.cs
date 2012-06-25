using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	Vector2 scrollPos = new Vector2(0,0);
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
	enum Chapter {intro, exampleStr, tutStr, exampleDiag, tutDiag, end};
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
	
	private int menuWidth = 300;
	private int menuChangeSpeed = 50; //100px/2sec
	
	public static void SetupTutorial(){
		tutorialState = new GameState();
		chapter = Chapter.intro;
	}
	
	//Stats.startState.SetTutorialBuild1();
	//Stats.skillEnabled.SetAll(false);
	//Stats.skillEnabled.build = true;
	
	void Start () {
		control = (Control)FindObjectOfType(typeof(Control));
		control.StartNewGame();
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
				chapter = Chapter.exampleStr;
			}
			break;
		case Chapter.exampleStr:
			if(GUI.Button(new Rect(Screen.width/2-buttonWidth/2, Screen.height-buttonHeight-border, buttonWidth, buttonHeight), "Continue")){
				chapter = Chapter.tutStr;
			}
			infoText.PrintTutorialText();
			//control.StartNewGame();
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
		case Chapter.tutStr:
			if(GUI.Button(new Rect(Screen.width/2-buttonWidth/2, Screen.height-buttonHeight-border, buttonWidth, buttonHeight), "Continue")){
				chapter = Chapter.exampleDiag;
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
		case Chapter.exampleDiag:
			if(GUI.Button(new Rect(Screen.width/2-buttonWidth/2, Screen.height-buttonHeight-border, buttonWidth, buttonHeight), "Continue")){
				chapter = Chapter.tutDiag;
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
		case Chapter.tutDiag:
			if(GUI.Button(new Rect(Screen.width/2-buttonWidth/2, Screen.height-buttonHeight-border, buttonWidth, buttonHeight), "Continue")){
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
