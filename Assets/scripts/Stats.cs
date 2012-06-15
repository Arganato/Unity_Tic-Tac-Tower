using UnityEngine;
using System.Collections;

public static class Stats{
	// Stats contains global constants and variables that define stats for the game
	// Should not be edited during a game
	
	public struct SkillEnabled{
		public bool shoot;
		public bool build;
		public bool silence;
		public bool skillCap;
		public bool diagShoot;
		public bool diagBuild;
		public bool diagEmp;
		public bool diagSkillCap;
		
		public void SetDiag(bool b){
		diagShoot = b;
		diagBuild = b;
		diagEmp = b;
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
	}

	public enum Rules{ SOLID_TOWERS, INVISIBLE_TOWERS, CHOOSE_TO_BUILD, SKILLS_PR_TEN}
		
	public static int fieldSize = 9;
	public static GameState startState = new GameState();
	public static SkillEnabled skillEnabled = SkillEnabled.AllActive();
	public static Rules rules;
	public static bool gameRunning = true;
	
	
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
	
	public static void SetTutorialBuild1(){
		fieldSize = 9;
		startState.SetTutorialBuild1();
		skillEnabled.SetAll(false);
		skillEnabled.build = true;
	}
}
