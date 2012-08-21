using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState {

	public Field<Route> field;
	public int turn;
	public int activePlayer; // 0 or 1
	public int globalSkillCap;
	public int placedPieces; 
	public Player[] player = new Player[2];
	public SkillContainer skillsUsed; //skill used this turn by activePlayer
	public VictoryType victory = VictoryType.noVictory;
	public bool playerDone = false;
	public EffectInterface effectInterface = null; //if this is set to a value other than null, 
	 									//the GameState is assumed to be the playing game state
	
	public GameState(){
		field = new Field<Route>(Route.empty);
		turn = 0;
		activePlayer = 0;
		placedPieces = 0;
		globalSkillCap = 0;
		player[0] = new Player();
		player[1] = new Player();
		skillsUsed = new SkillContainer();
	}
	
	public GameState(GameState copy){
		field = new Field<Route>(copy.field);
		turn = copy.turn;
		activePlayer = copy.activePlayer;
		globalSkillCap = copy.globalSkillCap;
		placedPieces = copy.placedPieces;
		player[0] = new Player(copy.player[0]);
		player[1] = new Player(copy.player[1]);
		skillsUsed = copy.skillsUsed;
	}
	
	public void SetDefault(){
		field[5,4] = Route.red;
		field[3,4] = Route.blue;
		placedPieces = 2;
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
		skillsUsed.Reset();
	}
	
	public bool EvaluateTurn(Turn t){
		if(t.IsValid()){
			bool flag = true;
			foreach( Order o in t.GetOrderList()){
				if(!EvaluateOrder(o)){
					flag = false;
					break;
				}
			}
			return flag;
		}
		return false;
	}
	
	public bool EvaluateOrder(Order o){
		// Executes an order from the order-format
		bool validMove = false;
		switch(o.skill){
		case SkillType.noSkill:
			validMove = true;
			break;
		case SkillType.place:
			validMove = PlacePiece(o.position);
			break;
		case SkillType.shoot:
			if(Skill.CanUseShoot(this) == SkillSelectError.NO_ERROR){
				validMove = Shoot(o.position);
			}else{
				validMove = false;
			}
			break;
		case SkillType.build:
			if(Skill.CanUseBuild(this) == SkillSelectError.NO_ERROR){
				validMove = ExtraBuild(o.position);
			}else{
				validMove = false;
			}
			break;
		case SkillType.silence:
			if(Skill.CanUseSilence(this) == SkillSelectError.NO_ERROR){
				validMove = EMP();
			}else{
				validMove = false;
			}
			break;
		}
		return validMove;
	}
	
	private bool CheckCluster(FieldIndex index){ //rename?
		//Finds a cluster from a field index recursively
		//calls appropriate FindTower-functions on this cluster
		//reports found towers
		//returns true if a piece was placed
		
		Field<bool> cluster = new Field<bool>(false); 

		cluster = Tower.FindAllClusterRecurse(index,cluster);
		List<Tower> foundTowers = Tower.FindTower(cluster);

//			Debug.Log(activePlayer + " silenced: " + player[activePlayer].silenced);
		if(!this.player[this.activePlayer].silenced){
			if(foundTowers.Count > 0){
				this.player[this.activePlayer].AddScore(foundTowers.Count);
				if(effectInterface != null)
					effectInterface.PlaySound(SoundType.newTower);
			}
			Tower fiveTower = null;
			foreach( Tower t in foundTowers){

				if(t.type == TowerType.five){
					fiveTower = t;
				}				
				//Coloring the towers:
				foreach(FieldIndex i in t.GetList()){ 

					if(Stats.rules == Stats.Rules.SOLID_TOWERS){
						this.field[i] = Field<int>.GetDarkRoute(this.field[i]);
					}else if(Stats.rules == Stats.Rules.INVISIBLE_TOWERS){
						this.field[i] = Route.empty;
					}
				}
				//adding skills:
				ReportTower(t);
			}
			if(fiveTower != null){
				foreach(FieldIndex i in fiveTower.GetList()){ //recoloring five-towers
					this.field[i] = Field<int>.GetPlayerColor(this.activePlayer);
				}
				VictoryToPlayer(activePlayer);
			}
			if (effectInterface != null)
				effectInterface.PlayBuildingConstructionEffect(foundTowers);
		}else if(foundTowers.Count > 0){ //if a tower was found that was blocked by Silence
			return false;
		}
		return true;
	}
	
	private void VictoryToPlayer(int player){
		if (player == 0){
			victory = VictoryType.playerOne;
		}else if(player == 1){
			victory = VictoryType.playerTwo;
		}
	}
	
	
	private void ReportTower(Tower t){
		//called from CheckCluster
		switch(t.type){
			case TowerType.shoot:
				this.player[this.activePlayer].playerSkill.shoot++;
				break;
			case TowerType.build:
				this.player[this.activePlayer].playerSkill.build++;
				break;
			case TowerType.silence:
				this.player[this.activePlayer].playerSkill.silence++;
				break;
			case TowerType.skillCap:
				this.player[this.activePlayer].playerSkill.skillCap++;
				break;
		}
	}
	
	private bool PlacePiece(FieldIndex index){ //placing piece in a normal turn
		if (playerDone == false && this.field[index] == Route.empty){
			field[index] = Field<int>.GetPlayerColor(activePlayer);

			if(CheckCluster(index)){
				IncPieceCount();
				playerDone = true;
				if (effectInterface != null)
					effectInterface.PlaySound(SoundType.onClick);
				return true;
			}else{ //the move is illegal due to silence
				if (effectInterface != null){
					effectInterface.PlaySound(SoundType.error);
					Console.PrintToConsole("You are silenced; You can't build towers",Console.MessageType.ERROR);
				}
				this.field[index] = Route.empty;
			}
		}
		else if(effectInterface != null){
			if(playerDone){
				Console.PrintToConsole("Can only place one piece each turn",Console.MessageType.ERROR);
			}else{
				Console.PrintToConsole("Cannot place there",Console.MessageType.ERROR);
			}
			PopupMessage.DisplayMessage("Invalid move",4f);
			effectInterface.PlaySound(SoundType.error);
		}
		return false;
		
	}
	private bool ExtraBuild(FieldIndex index){ //placing an extra piece with the build-skill
		if (this.field[index] == Route.empty){
		
			field[index] = Field<int>.GetPlayerColor(activePlayer);
			player[activePlayer].playerSkill.build--;
			skillsUsed.build++;
			if(CheckCluster(index)){
				IncPieceCount();
				if (effectInterface != null)
					effectInterface.PlaySound(SoundType.build);
				return true;
			}
		}
		else if (effectInterface != null){
			Debug.Log("invalid move");
			PopupMessage.DisplayMessage("Invalid move",4f);
			effectInterface.PlaySound(SoundType.error);
		}
		return false;
	}
	private bool Shoot(FieldIndex index){ //select an enemy piece to destroy it
	
		if (this.field[index] == Field<int>.GetPlayerColor( (this.activePlayer+1)%2 ) || this.field[index] == Field<int>.GetPlayerColor(this.activePlayer) ){
			this.field[index] = Route.destroyed;
			this.player[this.activePlayer].playerSkill.shoot--;
			skillsUsed.shoot++;
			if (effectInterface != null)
				effectInterface.PlaySound(SoundType.shoot);
			return true;
		}else if (effectInterface != null){
			effectInterface.PlaySound(SoundType.error);
			PopupMessage.DisplayMessage("Invalid move",4);
		}
		return false;
	}	
	public bool EMP(){
		Debug.Log("player "+(this.activePlayer+1)+" has used EMP");
		Console.PrintToConsole("player "+(this.activePlayer+1)+" has used EMP!",Console.MessageType.INFO);
		skillsUsed.silence++;
		player[activePlayer].playerSkill.silence--;
		player[(activePlayer+1)%2].silenced = true;
		if (effectInterface != null){
			effectInterface.PlaySound(SoundType.silence);
			effectInterface.PlaySilenceEffect();
		}
		return true;
	}

}
