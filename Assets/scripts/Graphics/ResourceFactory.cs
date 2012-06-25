using UnityEngine;
using System.Collections;

public static class ResourceFactory{
	//maybe not a proper factory; 
	//it holds links to all texts and textures accessed through scripts
	
	//SkillRelated
	private static Texture[] towerTextures = new Texture[8]; //fikse denne i en static-klasse
	private static Texture[] skillIconTextures = new Texture[4];
	private static Texture cancelTexture;
	
	//Arrows
	private static Texture arrowUp;
	private static Texture arrowDown;
	
	private static string towerBasepath = "GUI/Towers/"; // relative to the Resources-folder
	private static string[] towerPaths = new string[8]{ "shoot_rett", "build_rett", "emp_rett", "square_rett", "shoot_skra", "build_skra", "emp_skra", "square_skra"};
	private static string skillIconsBP = "GUI/Icons/Skills/";
	private static string[] skillIconpaths = new string[4]{ "Shoot_Icon", "Build_Icon", "Silence_Icon", "SkillCap_Icon"};
	private static bool isLoaded = false;

	public static string GetDescription(TowerType tower){
		switch(tower){
		case TowerType.shoot:
			return "Shoot Tower: \nThe player may destroy another unused, hostile piece on the board.The piece is ruined, and the tile cannot be built upon.";
		case TowerType.build:
			return "Build Tower: \nAllows the player to place one more piece on the board.This will not, however, reset the amount of skills used, as if starting a new round.";
		case TowerType.silence:
			return "Silence Tower: \nThe opponent is rendered unable to place a piece where he/she would normally be able to build a tower.";
		case TowerType.skillCap:
			return "Skill Cap Tower: \nIncreases the skill cap by one for the player who builds it, allowing the player to use the same skill one more time during the same round. In addition, the player will gain five score points at the end of each turn.";
		default:
			return "";
		}
	}
	
	public static string GetTutorialMessage(){
		switch(Tutorial.chapter){
		case Tutorial.Chapter.exampleStr:
			switch(Tutorial.towerTut){
			case TowerType.build:
				return "The following shows how to build a straight Build Tower.";
			case TowerType.shoot:
				return "The following shows how to build a straight Shoot Tower.";
			case TowerType.silence:
				return "The following shows how to build a straight Silence Tower.";
			case TowerType.skillCap:
				return "The following shows how to build a straight Power Tower.";
			default:
				return "";
			}
		case Tutorial.Chapter.tutStr:
			switch(Tutorial.towerTut){
			case TowerType.build:
				return "You'll now encounter a board mid-game (using only straight towers).\n During this turn, win the game!";
			case TowerType.shoot:
				return "You'll now encounter a board mid-game (using only straight towers)." +
					"\n Your opponent is about to win the game next round. It's up to you to prevent it!";
			case TowerType.silence:
				return "You'll now encounter a board mid-game (using only straight towers)." +
					"\n Your opponent is about to build a strong building. It's up to you to prevent it!";
			case TowerType.skillCap:
				return "You'll now encounter a board mid-game (using only straight towers).\n During this turn, win the game!";
			default:
				return "";
			}
		case Tutorial.Chapter.exampleDiag:
			switch(Tutorial.towerTut){
			case TowerType.build:
				return "The following shows how to build a diagonal Build Tower.";
			case TowerType.shoot:
				return "The following shows how to build a diagonal Shoot Tower.";
			case TowerType.silence:
				return "The following shows how to build a diagonal Silence Tower.";
			case TowerType.skillCap:
				return "The following shows how to build a diagonal Power Tower.";
			default:
				return "";
			}
		case Tutorial.Chapter.tutDiag:
			switch(Tutorial.towerTut){
			case TowerType.build:
				return "This game includes diagonal towers.\n During this turn, win the game!";
			case TowerType.shoot:
				return "This game includes diagonal towers.\n Your opponent is about to win the game next round. It's up to you to prevent it!";
			case TowerType.silence:
				return "This game includes diagonal towers.\n Your opponent is about to build a strong building. It's up to you to prevent it!";
			case TowerType.skillCap:
				return "This game includes diagonal towers.\n During this turn, win the game!";
			default:
				return "";
			}
		case Tutorial.Chapter.end:
			switch(Tutorial.towerTut){
			case TowerType.build:
				return "You've just finished the tutorial for the Build Tower. Good luck in your games.";
			case TowerType.shoot:
				return "You've just finished the tutorial for the Shoot Tower. Good luck in your games.";
			case TowerType.silence:
				return "You've just finished the tutorial for the Silence Tower. Good luck in your games.";
			case TowerType.skillCap:
				return "You've just finished the tutorial for the Power Tower. Good luck in your games.";
			default:
				return "";
			}
		default:
				return "";
		}
	}			
	
	public static Texture GetSkillTexture(int i){ 
		CheckLoaded();
		return towerTextures[i];
	}
	
	public static Texture GetSkillIcon(int i){
		CheckLoaded();
		if(i<skillIconTextures.Length && i>=0){
			return skillIconTextures[i];
		}else{
			Debug.LogError("tried to access skillIconTextures["+i+"]; Out of range (L = "+skillIconTextures.Length+")");
			return null;
		}
	}
	
	public static Texture GetCancelTexture(){
		CheckLoaded();
		if( cancelTexture == null){
		}
		return cancelTexture;
	}
	
	public static Texture GetArrowUp(){
		CheckLoaded();
		return arrowUp;
	}
	public static Texture GetArrowDown(){
		CheckLoaded();
		return arrowDown;
	}
	
	private static void CheckLoaded(){
		if (!isLoaded){
			LoadTextures();
		}
	}

	
	private static void LoadTextures(){
		LoadTowerTextures();
		LoadSkillIcons();
		arrowUp = Resources.Load("GUI/Icons/Arrows/arrow_up") as Texture;
		cancelTexture = Resources.Load("GUI/Misc/Stop") as Texture;
		arrowDown = Resources.Load("GUI/Icons/Arrows/arrow_down") as Texture;

		isLoaded = true;
	}
	private static void LoadTowerTextures(){
		for( int i=0; i<towerTextures.Length;i++){
			towerTextures[i] = Resources.Load(towerBasepath+towerPaths[i]) as Texture;
			if(towerTextures[i] == null){ //this doesnt work... must probably catch some exceptions instead...
				Debug.LogError("Texture not found: "+towerBasepath+towerPaths[i]);
			}
		}		
	}
	private static void LoadSkillIcons(){
		for (int i=0; i<skillIconTextures.Length;i++){
			skillIconTextures[i] = Resources.Load(skillIconsBP+skillIconpaths[i]) as Texture;
			if(towerTextures[i] == null){
				Debug.LogError("Texture not found: "+skillIconsBP+skillIconpaths[i]);
			}

		}
	}
}
