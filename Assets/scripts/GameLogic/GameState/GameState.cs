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
	
	public void SetTutorialBuild1(){	//Win during this round (red)
		field[5,4] = Route.red;
		field[3,4] = Route.blue;
		field[5,5] = Route.red;
		field[5,3] = Route.blue;
		field[4,6] = Route.red;
		field[6,3] = Route.blue;
		field[7,3] = Route.red;
		field[4,5] = Route.blue;
		field[7,4] = Route.red;
		field[7,5] = Route.blue;
		field[3,6] = Route.red;
		field[4,3] = Route.blue;
		field[3,3] = Route.red;
		field[3,5] = Route.blue;
		field[2,6] = Route.red;
		field[1,6] = Route.blue;
		player[0].playerSkill.skillCap = 1;
		player[1].playerSkill.skillCap = 1;
		placedPieces = 16;
	}
	
	public void SetTutorialBuild2(){	//Win during this round (red)
		field[5,4] = Route.red;
		field[3,4] = Route.blue;
		field[4,5] = Route.red;
		field[3,6] = Route.blue;
		field[3,5] = Route.red;
		field[2,5] = Route.blue;
		field[2,6] = Route.red;
		field[2,4] = Route.blue;
		field[4,6] = Route.red;
		field[5,2] = Route.blue;
		field[3,3] = Route.red;
		field[6,1] = Route.blue;
		field[1,3] = Route.red;
		field[2,2] = Route.blue;
		field[2,1] = Route.red;
		field[3,2] = Route.blue;
		field[2,1] = Route.red;
		field[6,1] = Route.blue;
		field[1,0] = Route.red;
		field[5,5] = Route.blue;
		field[1,1] = Route.red;
		field[3,1] = Route.blue;
		player[0].playerSkill.skillCap = 1;
		player[1].playerSkill.skillCap = 1;
		placedPieces = 22;
	}
	
	public void SetTutorialShoot1(){	//Stop red from winning next round (blue)
		field[5,4] = Route.blue;
		field[5,5] = Route.blue;
		field[5,6] = Route.blue;
		field[5,3] = Route.blue;
		field[3,4] = Route.red;
		field[3,5] = Route.red;
		field[3,6] = Route.red;
	}
	
	public void SetTutorialShoot2(){	//Stop red from winning next round (blue)
		field[5,4] = Route.blue;
		field[3,4] = Route.red;
		field[5,5] = Route.blue;
		field[5,6] = Route.red;
		field[4,5] = Route.blue;
		field[3,5] = Route.red;
		field[3,6] = Route.blue;
		field[6,3] = Route.red;
		field[2,7] = Route.blue;
		field[1,8] = Route.red;
		field[7,2] = Route.blue;
		field[5,3] = Route.red;
		field[5,2] = Route.blue;
		field[6,5] = Route.red;
		field[6,2] = Route.blue;
		field[4,7] = Route.red;
		field[3,7] = Route.blue;
		field[7,3] = Route.red;
		field[4,2] = Route.blue;
		field[6,6] = Route.red;
		field[5,7] = Route.blue;
		field[2,8] = Route.red;
		field[5,8] = Route.blue;
		player[0].playerSkill.skillCap = 1;
		player[1].playerSkill.skillCap = 1;
		activePlayer = 0;
		placedPieces = 21;
	}
	
	public void SetTutorialSilence1(){
		field[5,4] = Route.blue;
		field[5,5] = Route.blue;
		field[5,6] = Route.blue;
		field[5,3] = Route.blue;
		field[3,4] = Route.red;
		field[3,5] = Route.red;
		field[3,6] = Route.red;
		field[2,7] = Route.red;
	}
	
	public void SetTutorialSilence2(){
		field[5,4] = Route.blue;
		field[3,4] = Route.red;
		field[5,5] = Route.blue;
		field[5,6] = Route.red;
		field[4,6] = Route.blue;
		field[4,5] = Route.red;
		field[2,3] = Route.blue;
		field[6,6] = Route.red;
		field[6,7] = Route.blue;
		field[7,6] = Route.red;
		field[6,3] = Route.blue;
		field[7,3] = Route.red;
		field[4,3] = Route.blue;
		field[6,4] = Route.red;
		field[2,4] = Route.blue;
		field[3,7] = Route.red;
		field[1,3] = Route.blue;
		field[7,2] = Route.red;
		field[3,5] = Route.blue;
	}
	
	public void SetTutorialPower1(){
		field[5,2] = Route.red;
		field[4,2] = Route.red;
		field[5,5] = Route.red;
		field[4,5] = Route.red;
		field[4,6] = Route.red;

		field[3,3] = Route.blue;
		field[5,3] = Route.blue;
		field[4,3] = Route.blue;
		
		player[0].playerSkill.build = 2;
		player[0].playerSkill.skillCap = 0;
		player[1].playerSkill.build = 2;
		player[1].playerSkill.skillCap = 1;
		activePlayer = 0;
	}	
	
	public void SetTutorialPower2(){
		field[5,4] = Route.blue;
		field[3,4] = Route.red;
		field[5,5] = Route.blue;
		field[4,5] = Route.red;
		field[6,4] = Route.blue;
		field[3,6] = Route.red;
		field[4,6] = Route.blue;
		field[7,3] = Route.red;
		field[3,7] = Route.blue;
		field[8,2] = Route.red;
		field[4,3] = Route.blue;
		field[6,5] = Route.red;
		field[2,4] = Route.blue;
		field[1,4] = Route.red;
		field[6,7] = Route.blue;
		field[1,6] = Route.red;
		field[1,5] = Route.blue;
		field[5,7] = Route.red;
		field[2,7] = Route.blue;
		field[6,6] = Route.red;
		field[2,6] = Route.blue;
		field[7,7] = Route.red;
		field[8,3] = Route.blue;
		player[0].playerSkill.build = 1;
		player[0].playerSkill.skillCap = 1;
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
		player[activePlayer].EndTurn(player[activePlayer].playerSkill.skillCap);
		if(activePlayer == 1){
			turn++;
		}
		activePlayer = (activePlayer+1)%2;
		Skill.skillInUse = 0;
		Skill.skillsUsed.Reset();
	}

}
