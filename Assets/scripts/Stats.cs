using UnityEngine;
using System.Collections;

public struct SkillEnabled{
	public bool shoot;
	public bool build;
	public bool emp;
	public bool square;
	public bool diagShoot;
	public bool diagBuild;
	public bool diagEmp;
	public bool diagSquare;
	
	public void SetDiag(bool b){
	diagShoot = b;
	diagBuild = b;
	diagEmp = b;
	diagSquare = b;
	}
	
	public void SetStraight(bool b){
	shoot = b;
	build = b;
	emp = b;
	square = b;
	}
	public void SetAll(bool b){
		SetDiag(b);
		SetStraight(b);
	}
}

public static class Stats{
	// Stats contains global constants and variables that define stats for the game
	// Should not be edited during a game
	
	public static int fieldSize = 9;
	public static GameState startState;
	public static SkillEnabled skillEnabled;

	public static void SetDefaultSettings(){
		fieldSize = 9;
		startState = new GameState();
		skillEnabled.SetAll(true);
	}
}
