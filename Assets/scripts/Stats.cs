using UnityEngine;
using System.Collections;

public static class Stats{
	// Stats contains global constants and variables that define stats for the game
	// Should not be edited during a game
	
	public enum PlayerController {localPlayer, remotePlayer, AI}
	
	public struct SkillEnabled{
		public bool shoot;
		public bool build;
		public bool silence;
		public bool skillCap;
		public bool diagShoot;
		public bool diagBuild;
		public bool diagSilence;
		public bool diagSkillCap;
		
		public void SetDiag(bool b){
		diagShoot = b;
		diagBuild = b;
		diagSilence = b;
		diagSkillCap = b;
		}
		
		public void SetStraight(bool b){
		shoot = b;
		build = b;
		silence = b;
		skillCap = b;
		}
		public void SetAll(bool b){
			SetDiag(b);
			SetStraight(b);
		}
		
		public static SkillEnabled AllActive(){
			SkillEnabled ret = new SkillEnabled();
			ret.SetAll(true);
			return ret;
		}
		
		public override string ToString(){
			string s = "{";
			s += (shoot ? "1" : "0");
			s += (build ? "1" : "0");
			s += (silence ? "1" : "0");
			s += (skillCap ? "1" : "0");
			s += (diagShoot ? "1" : "0");
			s += (diagBuild ? "1" : "0");
			s += (diagSilence ? "1" : "0");
			s += (diagSkillCap ? "1" : "0");
			s += "}";
			return s;
		}
		public bool ReadFromString(string s){
			if(s.StartsWith("{") && s.EndsWith("}") && s.Length == 10){
				shoot = s[1] == '1';
				build = s[2] == '1';
				silence = s[3] == '1';
				skillCap = s[4] == '1';
				
				diagShoot = s[5] == '1';
				diagBuild = s[6] == '1';
				diagSilence = s[7] == '1';
				diagSkillCap = s[8] == '1';
				Debug.Log("string: "+s+". sh: "+shoot+", bu: "+build+", si: "+silence+". sk: "+skillCap+", osv...");
				return true;
			}else{
				Debug.Log("string: "+s+"; found to be incorrect");
				return false;
			}
		}
	}

	public enum Rules{ SOLID_TOWERS, INVISIBLE_TOWERS, CHOOSE_TO_BUILD, SKILLS_PR_TEN}
		
	//member variables&functions:
	
	public static int fieldSize = 9;
	public static GameState startState = new GameState();
	public static SkillEnabled skillEnabled = SkillEnabled.AllActive();
	public static Rules rules;
	public static bool gameRunning = true;
	public static bool hasConnection = false; //set to true when a network connection is established
	public static PlayerController[] playerController = new PlayerController[2];
	public const string uniqueGameID = "TicTacTower_0.9000";
	public static bool networkEnabled = false;
	
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
		Application.targetFrameRate = 15;
	}
	
	public static string MakeNetworkPackage(){
		int intRules = (int)rules;
		string s = intRules.ToString();
		s+="."+rules.ToString();
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
