using UnityEngine;
using System.Collections;

public class GameState {

	public Field<Route> field;
	public int turn;
	public int activePlayer; // 0 or 1
	public int globalSkillCap;
	public int placedPieces; 
	public Player[] player = new Player[2];
	
	public GameState(){
		field = new Field<Route>(Route.empty);
		turn = 0;
		activePlayer = 0;
		placedPieces = 0;
		globalSkillCap = 0;
		player[0] = new Player();
		player[1] = new Player();
	}
	
	public GameState(GameState copy){
		field = new Field<Route>(copy.field);
		turn = copy.turn;
		activePlayer = copy.activePlayer;
		globalSkillCap = copy.globalSkillCap;
		placedPieces = copy.placedPieces;
		player[0] = new Player(copy.player[0]);
		player[1] = new Player(copy.player[1]);
	}
	
	public void SetDefault(){
		field[5,4] = Route.red;
		field[3,4] = Route.blue;		
		placedPieces = 2;	
	}
	public void SetTutorialBuild1(){
		//TODO
	}	
	public void Reset(){
		field = new Field<Route>(Route.empty);
		turn = 0;
		activePlayer = 0;
		placedPieces = 0;
		globalSkillCap = 0;
		player[0].Reset();
		player[1].Reset();
		
	}
	
	//----gameplay-related functions----//
	
	public void IncPieceCount(){
		// first skill cap increase: after piece nr. 28
		// second skill cap increase: after piece nr. 54
		// (consistent with giving player 2 the first turn with extra cap)
		placedPieces++;
		if(Stats.rules == Stats.Rules.SOLID_TOWERS){
			if(placedPieces > 2*Stats.totalArea/3){
				globalSkillCap = 2;
			}else if(placedPieces > Stats.totalArea/3){
				globalSkillCap = 1;
			}
		}
	}
	
		public void ChangeActivePlayer(){
		player[activePlayer].EndTurn(player[activePlayer].playerSkill.square);
		if(activePlayer == 1){
			turn++;
		}
		activePlayer = (activePlayer+1)%2;
		Skill.skillInUse = 0;
		Skill.skillsUsed.Reset();
	}

}
