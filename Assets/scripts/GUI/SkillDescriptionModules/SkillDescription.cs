using UnityEngine;
using System.Collections;

public class SkillDescription{

	private static Texture[] towerTextures = new Texture[8]; //fikse denne i en static-klasse
	private static Texture cancelTexture;
	
	private static string basepath = "GUI/Towers/"; // relative to the Resources-folder
	private static string[] paths = new string[8]{ "shoot_rett", "build_rett", "emp_rett", "square_rett", "shoot_skra", "build_skra", "emp_skra", "square_skra"};
	private static bool isLoaded = false;

	public string descrString;
	public Rect position = new Rect(0,0,Screen.width/2,150);

	private int textureSize = 75;


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
		if(!isLoaded){
			LoadTextures();
		}
		return towerTextures[i];
	}
	
	public static Texture GetCancelTexture(){
		if( cancelTexture == null){
			cancelTexture = Resources.Load("GUI/Misc/Stop") as Texture;
		}
		return cancelTexture;
	}
	
	private static void LoadTextures(){
	
		for( int i=0; i<towerTextures.Length;i++){
			towerTextures[i] = Resources.Load(basepath+paths[i]) as Texture;
		}
		isLoaded = true;
	}

	public SkillDescription(TowerType tower){
		descrString = GetDescription(tower);
		switch(tower){
		case TowerType.shoot:
			break;
		case TowerType.build:
			position.x = position.width;
			break;
		case TowerType.emp:
			position.y = position.height;
			break;
		case TowerType.square:
			position.x = position.width;
			position.y = position.height;
			break;
		default:
			Debug.LogWarning("TowerDescription not initialized with a valid tower");
			break;
		}
		if(!isLoaded){
			LoadTextures();
		}
	}

	public void PrintGUI(){
		GUI.BeginGroup(position);
		GUI.Box(new Rect(0,0,position.width,position.height),"");
		GUI.Box(new Rect(0,0,textureSize,textureSize),towerTextures[0]);
		GUI.Box(new Rect(0,textureSize,textureSize,textureSize), towerTextures[4]);
		GUI.Box(new Rect(textureSize,0,position.width-textureSize,position.height), descrString,"invisBox");
		GUI.EndGroup();
	}
}
