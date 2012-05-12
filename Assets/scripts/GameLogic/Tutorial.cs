using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	Vector2 scrollPos = new Vector2(0,0);
	public GUIStyle textBox;
	new public Transform camera;
	public Texture[] towerTextures;
	public int border = 0;

	private Control control;
	
	//camera positions

	//private Vector3 camPreviewPos = new Vector3(-30,150,35);
	//private Vector3 camPreviewRot = new Vector3(90,0,0);
	
	//event points
	
	public bool changeChapter = false;
	public int chapter = 0;
	//private int section = 0;
	
	private int menuWidth = 300;
	private int menuChangeSpeed = 50; //100px/2sec
	
	//text
	private string introText = "this is a scroll text \n\n\n\n\n\n\n this is further down\n\n\n\n\nthis is even further down\n\n this sentence is so long only because I wanted to see if the line is clipped or not. Probably it is clipped, and that would make me kinda sad.";
	
	private string[] towerExpl = new string[3]; //put skill explanations here
	
	void Start () {
		control = (Control)FindObjectOfType(typeof(Control));
		control.StartNewGame();		
	}
	void Update(){
		if(changeChapter && chapter == 0 && menuWidth > 200){
			menuWidth -= Mathf.RoundToInt(Time.deltaTime*menuChangeSpeed);
		}
	}
	
	void OnGUI(){
		switch(chapter){
		case 0:
			PrintTextWindow();
			break;
		case 1:
			PrintSkillInfoWindow();
			break;
		case 2:
			break;
		}
		if(chapter < 2 && !changeChapter && GUI.Button(new Rect(menuWidth-100,600,100,25),"Next")){
			StartCoroutine(ChangeChapter());
		}
		
		//deler skjermen i tre like deler:
		int buttonWidth = 100;
		int buttonHeight = 40;
//		int b1Start = (Screen.width-2*buttonWidth)/3;
//		int b2Start = 2*b1Start + buttonWidth;
		//int b1Start = (Screen.width-buttonWidth)/2;
		//int b2Start = Screen.width-buttonWidth;
		
		if(GUI.Button(new Rect(Screen.width/2-buttonWidth/2, 200, buttonWidth, buttonHeight), "Build Tower")){
			Stats.SetDefaultSettings();
			Stats.SetTutorialBuild1();
			Application.LoadLevel("tutorialBuild");
		}
		
		if(GUI.Button(new Rect(Screen.width/2-buttonWidth/2, 250, buttonWidth, buttonHeight), "Shoot Build")){
			Stats.SetDefaultSettings();
			Stats.SetTutorialBuild1();
			Application.LoadLevel("tutorialShoot");
		}
		
		if(GUI.Button(new Rect(Screen.width/2-buttonWidth/2, 300, buttonWidth, buttonHeight), "Silence Build")){
			Stats.SetDefaultSettings();
			Stats.SetTutorialBuild1();
			Application.LoadLevel("tutorialSilence");
		}
		
		if(GUI.Button(new Rect(Screen.width/2-buttonWidth/2, 350, buttonWidth, buttonHeight), "Skill Build")){
			Stats.SetDefaultSettings();
			Stats.SetTutorialBuild1();
			Application.LoadLevel("tutorialSkill");
		}
		
		if(GUI.Button(new Rect(Screen.width/2-buttonWidth/2, Screen.height-buttonHeight-border, buttonWidth, buttonHeight), "Main Menu")){
			Stats.SetDefaultSettings();
			Stats.SetTutorialBuild1();
			Application.LoadLevel("mainMenu");
		}
		
	}
	

	private void PrintTextWindow(){

		GUI.BeginScrollView(new Rect(0,100,menuWidth, 500),scrollPos, new Rect(0,0,menuWidth,800));
		GUI.Box(new Rect(0,0,menuWidth,800),introText,textBox);
		GUI.EndScrollView();
	}
	private void PrintSkillInfoWindow(){
		if(!changeChapter){
			GUI.BeginScrollView(new Rect(0,100,200, 500),scrollPos, new Rect(0,0,200,800));
			GUI.Box(new Rect(0,0,200,100),towerExpl[0],textBox);
			GUI.Box(new Rect(0,100,100,100),towerTextures[0]);
			GUI.EndScrollView();
		}
	}

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

}
