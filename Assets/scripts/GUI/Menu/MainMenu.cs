using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	public Texture[] towerTextures;
	public int textureSize = 75;
	
	public GUIStyle emptyTextArea;
	public GUISkin customSkin;
	
	public int towDescr = 130;
	private SkillDescription[] skillDescr = new SkillDescription[4];
	private bool showChooseRules = false;
	private SelectGameTime selectGameTime = new SelectGameTime();
	
	void Start () {
		skillDescr[0] = new SkillDescription(TowerType.shoot);
		skillDescr[1] = new SkillDescription(TowerType.build);
		skillDescr[2] = new SkillDescription(TowerType.silence);
		skillDescr[3] = new SkillDescription(TowerType.skillCap);
	}
	
	void OnGUI () {
		GUI.skin = customSkin;
		string gameIntro = "This boardgame is inspired by the traditional game of Tic-Tac-Toe, where you can build tetris-like towers to gain strategic advantages. The goal of the game is to build five-in-a-row, or (if no ones does) the player with the highest score wins.";
		string generalRules = "The towers is at the core of the game. Towers can be built with any rotation and mirroring, straight and diagonal, and the second you make the shape they will be built. If you, build something that can be several towers, you will get them all. Each tower will let you use its skill once (you may save it). The same type of skill can be used as many times as you have skill cap.";
		
		// ** Game Intro **
		GUI.Box(new Rect(0,0,Screen.width-2*0,20),"Welcome to Tic-Tac-Tower!");
		
		GUI.Box(new Rect(0,20+0,Screen.width/2-(3/2)*0,towDescr-(20+0)-0/2),"");
		GUI.Box(new Rect(0,20+0,Screen.width/2-(3/2)*0,towDescr-(20+0)-0/2),gameIntro,emptyTextArea);
		
		GUI.Box(new Rect(Screen.width/2 + 0/2,20+0,Screen.width/2-(3/2)*0,towDescr-(20+0)-0/2),"");
		GUI.Box(new Rect(Screen.width/2 + 0/2,20+0,Screen.width/2-(3/2)*0,towDescr-(20+0)-0/2),generalRules,emptyTextArea);
		
		
		// ** Tower Descriptions **

		GUI.BeginGroup(new Rect(0, towDescr,Screen.width,300));
		if(showChooseRules){
			selectGameTime.PrintGUI();
		}else{
			foreach(SkillDescription s in skillDescr){
				s.PrintGUI();
			}
		}
		GUI.EndGroup();
		// ** Start Game Buttons **
		

		//deler skjermen i tre like deler:
		int buttonWidth = 200;
		int b1Start = (Screen.width-2*buttonWidth)/2;
		int b2Start = Screen.width-buttonWidth;
		

		
		if(showChooseRules){
			GUI.Box(new Rect(0, Screen.height-50, b1Start-0,50-0),"When building, the pieces disapears","invisbox");
			GUI.Box(new Rect(Screen.width/2+0, Screen.height-50, b1Start-0,50-0),"When building, the pices turns solid and unusable","invisbox");
			if(GUI.Button(new Rect(0+b1Start, Screen.height-50, buttonWidth-0, 50-0), "Towers disapears")){
				Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
				showChooseRules = false;
				selectGameTime.SetGameTime();
				Application.LoadLevel("game");
			}
	
			if(GUI.Button(new Rect(0+b2Start,Screen.height-50, buttonWidth-0, 50-0), "Normal game")){
				Stats.rules = Stats.Rules.SOLID_TOWERS;
				showChooseRules = false;
				selectGameTime.SetGameTime();
				Application.LoadLevel("game");
			}
			
		}else{
			GUI.Box(new Rect(0, Screen.height-50, b1Start-0,50-0),"A game with only straight towers:",emptyTextArea);
			GUI.Box(new Rect(Screen.width/2+0, Screen.height-50, b1Start-0,50-0),"A game with all the towers:",emptyTextArea);
			if(GUI.Button(new Rect(0+b1Start, Screen.height-50, buttonWidth-0, 50-0), "Start simple Game")){
				Stats.SetDefaultSettings();
				Stats.skillEnabled.SetDiag(false);
				showChooseRules = true;
			}
	
			if(GUI.Button(new Rect(0+b2Start,Screen.height-50, buttonWidth-0, 50-0), "Start full Game")){
				Stats.SetDefaultSettings();
				showChooseRules = true;
			}
		}
		//Tutorial-buttton:
//		if(GUI.Button(new Rect(0+b1Start, Screen.height-200, buttonWidth-0, 50-0), "Tutorial")){
//			Stats.SetDefaultSettings();
//			Stats.SetTutorialBuild1();
//			Application.LoadLevel("tutorial");
//		}
	}
}

