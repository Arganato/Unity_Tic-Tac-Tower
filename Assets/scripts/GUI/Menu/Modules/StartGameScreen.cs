using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StartGameScreen {


	public Rect position = new Rect(0,40,Screen.width,Screen.height-80);
	public bool singlePlayer;
	
	private SelectGameTime selectGameTime = new SelectGameTime();
	private GUIList selectRules;
	private GUIList selectTowers;
	
	public StartGameScreen(){
		selectRules = new GUIList(new Rect(100,50,180,25));
		selectRules.AddElement("Disappearing towers");
		selectRules.AddElement("Solid towers");
		selectTowers = new GUIList(new Rect(100,100,180,25));
		selectTowers.AddElement("All towers");
		selectTowers.AddElement("Straight only");
		selectGameTime.position.y = 180;
		selectGameTime.position.x = 0;
	}
	
	public void PrintGUI(){
		GUI.BeginGroup(position);
		if(singlePlayer)
			GUI.Box(new Rect(0,0,position.width,25),"Start Local Game");
		else
			GUI.Box(new Rect(0,0,position.width,25),"Networked play not implemented yet");
			
		selectRules.PrintGUI();
		selectTowers.PrintGUI();
		
		selectGameTime.enable = GUI.Toggle(new Rect(20,160,100,25),selectGameTime.enable, "Time Cap");
		selectGameTime.PrintGUI();
		
		if(GUI.Button(new Rect(20,position.height-25,100,25),"Start Game")){
			StartGame();
		}
		
		GUI.EndGroup();
	}
	
	private void StartGame(){
		Stats.SetDefaultSettings();
		
		switch(selectRules.choise){
		case 0:
			Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
			break;
		case 1:
			Stats.rules = Stats.Rules.SOLID_TOWERS;
			break;
		}
		
		switch(selectTowers.choise){
		case 0:
			Stats.skillEnabled.SetAll(true);
			break;
		case 1:
			Stats.skillEnabled.SetDiag(false);
			Stats.skillEnabled.SetStraight(true);
			break;
		}
		
		if(selectGameTime.enable){
			selectGameTime.SetGameTime();
		}
		Application.LoadLevel("game");
	}
}
