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
	private static string skillIconsBP = "GUI/Icons/Skills";
	private static string[] skillIconpaths = new string[4]{ "Shoot_Icon", "Build_Icon", "Silence_Icon", "SkillCap_Icon"};
	private static bool isLoaded = false;

	public static string GetDescription(TowerType tower){
		switch(tower){
		case TowerType.shoot:
			return "Shoot Tower: \nThe player may destroy another unused, hostile piece on the board.The piece is ruined, and the tile cannot be built upon.";
		case TowerType.build:
			return "Build Tower: \nAllows the player to place one more piece on the board.This will not, however, reset the amount of skills used, as if starting a new round.";
		case TowerType.emp:
			return "Silence Tower: \nThe opponent is rendered unable to place a piece where he/she would normally be able to build a tower. Also, the opponent will not benefit from any abilities next turn.";
		case TowerType.square:
			return "Skill Cap Tower: \nIncreases the skill cap by one for the player who builds it, allowing the player to use the same skill one more time during the same round. In addition, the player will gain five score points at the end of each turn.";
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
		if(i<skillIconTextures.Length && i>0){
			return skillIconTextures[i];
		}else{
			Debug.LogError("tried to access skillIconTextures["+i+"]; Out of range");
			return null;
		}
			
	}
	
	public static Texture GetCancelTexture(){
		CheckLoaded();
		if( cancelTexture == null){
			cancelTexture = Resources.Load("GUI/Misc/Stop") as Texture;
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
		if(arrowUp == null){
			Debug.LogError("Texture not found: GUI/Icons/Arrows/arrow_up"); 
		}
		arrowDown = Resources.Load("GUI/Icons/Arrows/arrow_down") as Texture;
		if(arrowDown == null){
			Debug.LogError("Texture not found: GUI/Icons/Arrows/arrow_down"); 
		}
		isLoaded = true;
	}
	private static void LoadTowerTextures(){
		for( int i=0; i<towerTextures.Length;i++){
			towerTextures[i] = Resources.Load(towerBasepath+towerPaths[i]) as Texture;
			if(towerTextures[i] == null){
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
