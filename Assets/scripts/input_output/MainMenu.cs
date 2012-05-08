using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	public Texture[] towerTextures;
	public int textureSize = 75;
	
	public GUIStyle emptyTextArea;
	
	public int border = 0;
	public int towDescr = 130;
	
	void Start () {
	
	}
	
	void OnGUI () { 	
		string gameIntro = "This boardgame is inspired by the traditional game of Tic-Tac-Toe, where you can build tetris-like towers to gain strategic advantages. The goal of the game is to build five-in-a-row, or (if no ones does) the player with the highest score wins.";
		string generalRules = "The towers is at the core of the game. Towers can be built with any rotation and mirroring, straight and diagonal, and the second you make the shape they will be built. If you, build something that can be several towers, you will get them all. Each tower will let you use its skill once (you may save it). The same type of skill can be used as many times as you have skill cap.";
		
		// ** Game Intro **
		GUI.Box(new Rect(border,border,Screen.width-2*border,20),"Welcome to Tic-Tac-Tower!");
		
		GUI.Box(new Rect(border,20+border,Screen.width/2-(3/2)*border,towDescr-(20+border)-border/2),"");
		GUI.Box(new Rect(border,20+border,Screen.width/2-(3/2)*border,towDescr-(20+border)-border/2),gameIntro,emptyTextArea);
		
		GUI.Box(new Rect(Screen.width/2 + border/2,20+border,Screen.width/2-(3/2)*border,towDescr-(20+border)-border/2),"");
		GUI.Box(new Rect(Screen.width/2 + border/2,20+border,Screen.width/2-(3/2)*border,towDescr-(20+border)-border/2),generalRules,emptyTextArea);
		
		
		// ** Tower Descriptions **
		ShootDescrGUI();
		BuildDescrGUI();
		EmpDescrGUI();
		SquareDescrGUI();
		
		// ** Start Game Buttons **
		
		//deler skjermen i tre like deler:
		int buttonWidth = 200;
//		int b1Start = (Screen.width-2*buttonWidth)/3;
//		int b2Start = 2*b1Start + buttonWidth;
		int b1Start = (Screen.width-2*buttonWidth)/2;
		int b2Start = Screen.width-buttonWidth;
		
		GUI.Box(new Rect(border, Screen.height-50, b1Start-border,50-border),"A game with only straight towers:",emptyTextArea);
		if(GUI.Button(new Rect(border+b1Start, Screen.height-50, buttonWidth-border, 50-border), "Start simple Game")){
			Stats.SetDefaultSettings();
			Stats.skillEnabled.SetDiag(false);
			Application.LoadLevel("game");
		}
		
		GUI.Box(new Rect(Screen.width/2+border, Screen.height-50, b1Start-border,50-border),"A game with all the towers:",emptyTextArea);
		if(GUI.Button(new Rect(border+b2Start,Screen.height-50, buttonWidth-border, 50-border), "Start full Game")){
			Stats.SetDefaultSettings();
			Application.LoadLevel("game");
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
	private void BuildDescrGUI(){
		string descrString = "Build Tower: \nAllows the player to place one more piece on the board.This will not, however, reset the amount of skills used, as if starting a new round.";
		int width = Screen.width/2-border;
		int height = textureSize*2-border;
		
		GUI.BeginGroup(new Rect(border+width, towDescr+border,width,height));
		GUI.Box(new Rect(0,0,width,height),"");
		GUI.Box(new Rect(0,0,textureSize-border,textureSize-border),towerTextures[1]);
		GUI.Box(new Rect(0,textureSize,textureSize-border,textureSize-border), towerTextures[5]);
		GUI.Box(new Rect(textureSize,0,width-textureSize,height), descrString,emptyTextArea);
		GUI.EndGroup();
	}
	private void EmpDescrGUI(){
		string descrString = "EMP Tower: \nThe opponent is rendered unable to place a piece where he/she would normally be able to build a tower. Also, the opponent will not benefit from any abilities next turn.";
		int width = Screen.width/2-border;
		int height = textureSize*2-border;
		
		GUI.BeginGroup(new Rect(border, towDescr+height+border,width,height));
		GUI.Box(new Rect(0,0,width,height),"");
		GUI.Box(new Rect(0,0,textureSize-border,textureSize-border),towerTextures[2]);
		GUI.Box(new Rect(0,textureSize,textureSize-border,textureSize-border), towerTextures[6]);
		GUI.Box(new Rect(textureSize,0,width-textureSize,height), descrString,emptyTextArea);
		GUI.EndGroup();
	}
	private void SquareDescrGUI(){
		string descrString = "Square Tower: \nIncreases the skill cap by one for the player who builds it, allowing the player to use the same skill one more time during the same round. In addition, the player will gain five score points at the end of each turn.";
		int width = Screen.width/2-border;
		int height = textureSize*2-border;
		
		GUI.BeginGroup(new Rect(border+width, towDescr+height+border,width,height));
		GUI.Box(new Rect(0,0,width,height),"");
		GUI.Box(new Rect(0,0,textureSize-border,textureSize-border),towerTextures[3]);
		GUI.Box(new Rect(0,textureSize,textureSize-border,textureSize-border), towerTextures[7]);
		GUI.Box(new Rect(textureSize,0,width-textureSize,height), descrString,emptyTextArea);
		GUI.EndGroup();
	}
}
