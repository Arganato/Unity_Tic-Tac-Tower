using UnityEngine;
using System.Collections;

public class GameState {

	public Field<Route> field;
	public int turn;
	public int startingPlayer; // 0 or 1
	public int globalSkillCap;
	public int placedPieces; //??
	public SkillContainer player0Skills;
	public SkillContainer player1Skills;
	public Score player0Score;
	public Score player1Score;
	
	public GameState(){
		field = new Field<Route>(Stats.fieldSize, Route.empty);
		turn = 1;
		startingPlayer = 0;
		placedPieces = 0;
		globalSkillCap = 0;
		player0Skills = new SkillContainer();
		player1Skills = new SkillContainer();
		player0Score = new Score();
		player1Score = new Score();
	}
				
}