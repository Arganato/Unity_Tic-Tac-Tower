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
			return "Shoot Tower: \nThe player may destroy another piece on the board. The piece is ruined, and the tile cannot be built upon. It is not possible to shoot gray pieces";
		case TowerType.build:
			return "Build Tower: \nAllows the player to place one more piece on the board, however, to be able to use this more than once, aditional Power is required";
		case TowerType.silence:
			return "Silence Tower: \nFor the next turn, the opponent cannot place a piece where it would make a tower, or 5 in a row.";
		case TowerType.skillCap:
			return "Power Tower: \nIncreases the power by one for the player who builds it. Power is the cap for how many times one skilltype can be used in one turn.";
		default:
			return "";
		}
	}
	public static string GetSkillName(int i){
		switch(i){
		case 0:
			return "Shoot";
		case 1:
			return "Build";
		case 2: 
			return "Silence";
		case 3:
			return "Power";
		default:
			return "Invalid";
		}
	}
	public static string GetSkillName(TowerType type){
		return GetSkillName((int)type);
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
