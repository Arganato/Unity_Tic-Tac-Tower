using UnityEngine;
using System.Collections;

public class TutorialBuild : MonoBehaviour {

	public GUIStyle textBox;
	new public Transform camera;
	
	public Texture[] towerTextures;
	public int textureSize = 75;
	public GUIStyle emptyTextArea;
	public int border = 0;
	public int towDescr = 130;
	
	private Control control;
	
	public SkillDescription description = new SkillDescription(TowerType.build);
	
	//camera positions

	//private Vector3 camPreviewPos = new Vector3(-30,150,35);
	//private Vector3 camPreviewRot = new Vector3(90,0,0);
	
	//event points
	
	private enum Chapter {intro, exampleStr, tutStr, exampleDiag, tutDiag};
	private Chapter chapter;
	//private int section = 0;
	
	private int menuWidth = 300;
	private int menuChangeSpeed = 50; //100px/2sec
	
	private string[] towerExpl = new string[3]; //put skill explanations here
	
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
		
		switch(chapter){
		case Chapter.intro:
			BuildDescrGUI();
			break;
		case Chapter.exampleStr:
			break;
		}
		
		//deler skjermen i tre like deler:
		int buttonWidth = 200;
//		int b1Start = (Screen.width-2*buttonWidth)/3;
//		int b2Start = 2*b1Start + buttonWidth;
		int b1Start = (Screen.width-4*buttonWidth)/2;
		int b2Start = Screen.width-3*buttonWidth; //Not used here
		
		if(GUI.Button(new Rect(border+b1Start, Screen.height-50, buttonWidth-border, 50-border), "Continue")){
			chapter = Chapter.exampleStr;
			Stats.SetDefaultSettings();
			//Application.LoadLevel("tutorialBuild");
		}
		
	}
	
	private void BuildDescrGUI(){
		string descrString = "Build Tower: \nAllows the player to place one more piece on the board. This will not, however, reset the amount of skills used, as if starting a new round.";
		int width = Screen.width/2-border;
		int height = textureSize*2-border;
		
		GUI.BeginGroup(new Rect(border+50, towDescr+border,width,height));
		GUI.Box(new Rect(0,0,width,height),"");
		GUI.Box(new Rect(0,0,textureSize-border,textureSize-border),towerTextures[1]);
		GUI.Box(new Rect(0,textureSize,textureSize-border,textureSize-border), towerTextures[5]);
		GUI.Box(new Rect(textureSize,0,width-textureSize,height), descrString,emptyTextArea);
		GUI.EndGroup();
	}
	
}
