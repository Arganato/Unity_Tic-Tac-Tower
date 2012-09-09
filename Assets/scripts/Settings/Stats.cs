using UnityEngine;
using System.Collections;

public static class Stats{
	// Stats contains global constants and variables that define stats for the game
	// Should not be edited during a game
	
	public enum PlayerController {localPlayer, remotePlayer, AI}
	


	public enum Rules{ SOLID_TOWERS, INVISIBLE_TOWERS, CHOOSE_TO_BUILD, SKILLS_PR_TEN}
		
	//member variables&functions:
	
	//game-related
	public static int fieldSize = 9;
	public static GameState startState = new GameState();
	public static SkillEnabled skillEnabled = SkillEnabled.AllActive();
	public static Rules rules;
	public static bool gameRunning = true;
	//network-related
	public static bool hasConnection = false; //set to true when a network connection is established
	public static PlayerController[] playerController = new PlayerController[2];
	public const string uniqueGameID = "TicTacTower_0.9000";
	public static bool networkEnabled = false;
	public static string playerName = "Player 1";
	
	private static bool hasRanStartupRoutine = false;
	public static int totalArea{
		get{return fieldSize*fieldSize;}
	}
	
	public static void SetDefaultSettings(){
		fieldSize = 9;
		startState = new GameState();
		startState.SetDefault();
		skillEnabled.SetAll(true);
		rules = Rules.INVISIBLE_TOWERS;
	}
	
	public static void StartUpRoutine(){
		if(!hasRanStartupRoutine){
			Application.targetFrameRate = 15;
		}
		hasRanStartupRoutine = true;
	}
	
	public static string MakeNetworkPackage(){
		int intRules = (int)rules;
		string s = intRules.ToString();
		s+="."+skillEnabled.ToString();
		return s;
	}
	
	public static bool ReadNetworkPackage(string s){
		string[] split = s.Split('.');
		if(split.Length == 2){
			int rulesReceived = System.Convert.ToInt32(split[0]);
			Debug.Log("String: "+s+". Rules found: "+rulesReceived+".");
			rules = (Rules)rulesReceived;
			return skillEnabled.ReadFromString(split[1]);
		}
		Debug.Log("string: "+s+". discarded.");
		return false;
	}
}
