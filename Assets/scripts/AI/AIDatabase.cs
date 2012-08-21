using UnityEngine;
using System.Collections;

public class AIDatabase{

	public int buildValue = 100;
	public int shootValue = 100;
	public int silenceValue = 100;
	public int skillCapValue = 150;
	public int pieceValue = 5; //value of a piece on the board
	
	public int EvaluateGameState(GameState state, int maximizingPlayer){
		int value = 0;
		value += EvaluatePlayersSkills(state, maximizingPlayer);
		value -= EvaluatePlayersSkills(state, (maximizingPlayer+1)%2); //minimizing player
		for(int i=0;i<Stats.fieldSize*Stats.fieldSize;i++){
			if( state.field[i] == Field<int>.GetPlayerColor(maximizingPlayer)){
				value += pieceValue;
			}if( state.field[i] == Field<int>.GetPlayerColor((maximizingPlayer+1)%2)){
				value -= pieceValue;
			}
		}
		return value;
	}
	
	private int EvaluatePlayersSkills(GameState state, int player){
		int value = 0;
		value += Control.cState.player[player].playerSkill.build*buildValue;
		value += Control.cState.player[player].playerSkill.shoot*shootValue;
		value += Control.cState.player[player].playerSkill.silence*silenceValue;
		value += Control.cState.player[player].playerSkill.skillCap*skillCapValue;
		return value;
	}
}
