using UnityEngine;
using System.Collections;

public class TutorialShoot : MonoBehaviour {

	public GUIStyle textBox;
	new public Transform camera;
	
	public Texture[] towerTextures;
	public int textureSize = 75;
	public GUIStyle emptyTextArea;
	public int border = 0;
	public int towDescr = 130;
	
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
		
		ShootDescrGUI();
		
		//deler skjermen i tre like deler:
		int buttonWidth = 200;
//		int b1Start = (Screen.width-2*buttonWidth)/3;
//		int b2Start = 2*b1Start + buttonWidth;
		int b1Start = (Screen.width-4*buttonWidth)/2;
		int b2Start = Screen.width-3*buttonWidth; //Not used here
		
		if(GUI.Button(new Rect(border+b1Start, Screen.height-50, buttonWidth-border, 50-border), "Shoot 1")){
			Stats.SetDefaultSettings();
			//Application.LoadLevel("tutorialShoot");
		}
		
		if(GUI.Button(new Rect(border+b2Start, Screen.height-50, buttonWidth-border, 50-border), "Shoot 2")){
			Stats.SetDefaultSettings();
			//Application.LoadLevel("tutorialShoot");
		}
		
	}
	
	private void ShootDescrGUI(){
		string descrString = "Shoot Tower: \nThe player may destroy another unused, hostile piece on the board.The piece is ruined, and the tile cannot be built upon.";
		int width = Screen.width/2-border;
		int height = textureSize*2-border;
		
		GUI.BeginGroup(new Rect(border, towDescr+border,width,height));
		GUI.Box(new Rect(0,0,width,height),"");
		GUI.Box(new Rect(0,0,textureSize-border,textureSize-border),towerTextures[0]);
		GUI.Box(new Rect(0,textureSize,textureSize-border,textureSize-border), towerTextures[4]);
		GUI.Box(new Rect(textureSize,0,width-textureSize,height), descrString,emptyTextArea);
		GUI.EndGroup();
	}
	
}
