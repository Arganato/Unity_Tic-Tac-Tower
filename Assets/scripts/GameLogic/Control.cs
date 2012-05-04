using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Control: MonoBehaviour {

	public int placedPieces = 0;
	public int totalArea;
	public int extraSkillCap = 0;
	
	public static Field<Route> playField;
	public bool playerDone = false; //Is true when player has placed a piece. Allows user to "End Turn".
	
	public int turn = 1; //1-indexed
	public int currPlayer = 0; //player 1 starting; changed to 0-indexing
	public int skillInUse; // skill used by currPlayer. 0 = no skill, 1 = shoot, 2 = build, etc
	
	//Skills:
	public SkillContainer[] playerSkill = new SkillContainer[2];

	public Player[] player = new Player[2];
	
	public SkillContainer skillsUsed; //skill used this turn by currPlayer
	
	private Sound sound;
	
	void Awake () {
		sound = (Sound)FindObjectOfType(typeof(Sound));
		if (sound == null){
			Debug.LogError("sound-object not found");
		}

	}
	
	public void UserFieldSelect(FieldIndex index){
		//Called when the user clicks on the field
		switch(skillInUse){
			case 0:
				PlacePiece(index);
				break;
			case 1:
				Shoot(index);
				break;
			case 2:
				ExtraBuild(index);
				break;
			case 3:
				//Silence should not be called like this **Debug** sjekk ut dette
				Debug.LogError("emp should not be reported to Control");
				break;
		}
	}
	
	private bool CheckCluster(FieldIndex index){ //rename?
		//Finds a cluster from a field index recursively
		//calls appropriate FindTower-functions on this cluster
		//reports found towers
		
		// **DEBUG** tilpasse funksjonalitet til silence
		if(playField[index] != Route.empty){
			Field<bool> cluster = new Field<bool>(false); 

			cluster = Tower.FindAllClusterRecurse(index,cluster);
			List<Tower> tower = Tower.FindTower(cluster);
			//Debug.Log(currPlayer + " silenced: " + player[currPlayer].silenced);
			
			
			//Coloring towers, and adding skills and score to players
			if(!player[currPlayer].silenced || tower.Count == 0){
				player[currPlayer].AddScore(tower.Count);
				foreach( Tower t in tower){
					//Checking for victory
					if(t.towerType == TowerType.five){
						player[currPlayer].score += 1000;
						// *DEBUG* lage game-over screen her
						sound.PlaySound(SoundType.victory);
					}
					//Coloring the towers:
					foreach(FieldIndex i in t.GetList()){ 
						
						// **DEBUG** lage GetDarkColor-funksjon
						playField[i] = Field<int>.GetDarkRoute(playField[i]);
						
					}
					//reporting the towers:
					ReportTower(t);
				}
			}else{return false;}
		}
		BroadcastMessage("UpdateField");
		return true;
	}
	// move to Skill-class
	public int UseSkill(int skill){
		//Returns an error code 
		switch (skill){
			case 0: //no skill
				skillInUse = 0;
				return 0;
			case 1: //shoot
				Debug.Log("shoot selected");
				if (playerSkill[currPlayer].shoot > 0){
					if( skillsUsed.shoot <= playerSkill[currPlayer].square + extraSkillCap){
						skillInUse = 1;
						return 0;
						//no error
					}else{
						return 2;
						//"not enough squares"-error
					}
				}else{
					return 1;
					//"not enough skill-ammo"-error
				}
			case 2: //build
				Debug.Log("build selected");
				
				if ( playerSkill[currPlayer].build > 0){
					if ( skillsUsed.build <= playerSkill[currPlayer].square + extraSkillCap){
						skillInUse = 2;
						return 0;
					}else{
						return 2;
						//"not enough squares"-error
					}
				}else{
					return 1;
					//"not enough skill-ammo"-error
				}

			case 3:
				Debug.Log("emp selected");
				if (skillsUsed.emp < 1){
					if (playerSkill[currPlayer].emp > 0){
						EMP();
						return 0;
					}else{
						return 1;
						//"not enough skill-ammo"-error
					}
				}else{
					return 2;
					//"not enough squares"-error
				}
		}
		return 3;
		//unknown error
	}
	
	public void ExecuteOrder(Order o){
		// Executes an order from the order-format
		// TODO: make all orders go through this by having a wrapper function
		if (o.player == currPlayer){
			switch(o.skill){
			case -1:
				if(o.endTurn){
					ChangeCurrPlayer();
				}
				break;
			case 0:
				PlacePiece(o.position);
				break;
			case 1:
				Shoot(o.position);
				break;
			case 2:
				ExtraBuild(o.position);
				break;
			case 3:
				EMP();
				break;
			}
			if(o.endTurn){
				ChangeCurrPlayer();
			}
		}else{
			Debug.LogWarning("ExecuteOrder Called with wrong player");
		}
	}
	
	private void IncPieceCount(){
		// first skill cap increase: after piece nr. 28
		// second skill cap increase: after piece nr. 54
		// (consistent with giving player 2 the first turn with extra cap)
		placedPieces++;
		if(placedPieces > 2*totalArea/3){
			extraSkillCap = 2;
		}else if(placedPieces > totalArea/3){
			extraSkillCap = 1;
		}
	}
	
	
	//Move to Skill-class
	private void PlacePiece(FieldIndex index){ //placing piece in a normal turn
		if (playerDone == false && playField[index] == Route.empty){
			if(currPlayer == 0){
				playField[index] = Route.red;
			}else{
				playField[index] = Route.blue;
			}
			if(CheckCluster(index)){
				IncPieceCount();
				playerDone = true;
			}else{
				playField[index] = Route.empty;
			}
				sound.PlaySound(SoundType.onClick);
		}else{
			Debug.Log("invalid move");
			sound.PlaySound(SoundType.error);
			// **DEBUG** write this out somehow
		}
	}
	//Move to Skill-class
	private void ExtraBuild(FieldIndex index){ //placing an extra piece with the build-skill
		if (playField[index] == Route.empty){
		
			playField[index] = Field<int>.GetPlayerColor(currPlayer);
			playerSkill[currPlayer].build--;
			skillsUsed.build++;
			
			CheckCluster(index);
			IncPieceCount();
			skillInUse = 0;
			sound.PlaySound(SoundType.build);
		}else{
			Debug.Log("invalid move");
			sound.PlaySound(SoundType.error);
			// **DEBUG** write this out somehow
		}		
		//do not change first player
	}
	//Move to Skill-class
	private void Shoot(FieldIndex index){ //select an enemy piece to destroy it
	
		if (playField[index] == Field<int>.GetPlayerColor( (currPlayer+1)%2 ) ){
			playField[index] = Route.destroyed;
			playerSkill[currPlayer].shoot--;
			skillsUsed.shoot++;
			skillInUse = 0;
			BroadcastMessage("UpdateField");
			sound.PlaySound(SoundType.shoot);
		}else{
			Debug.Log("invalid move");
			sound.PlaySound(SoundType.error);
			//write this out somehow
		}
		
	}
	//Move to Skill-class
	private void EMP(){
		Debug.Log("player "+currPlayer+" has used EMP");
		skillsUsed.emp++;
		playerSkill[currPlayer].emp--;
		player[(currPlayer+1)%2].silenced = true;
		sound.PlaySound(SoundType.emp);
		}
	
	private void ReportTower(Tower t){
		switch(t.towerType){
			case TowerType.shoot:
				playerSkill[currPlayer].shoot++;
				break;
			case TowerType.build:
				playerSkill[currPlayer].build++;
				break;
			case TowerType.emp:
				playerSkill[currPlayer].emp++;
				break;
			case TowerType.square:
				playerSkill[currPlayer].square++;
				break;
		}
	}
	
	public void ChangeCurrPlayer(){
		player[currPlayer].EndTurn(playerSkill[currPlayer].square);
		if(currPlayer == 1){
			turn++;
		}
		currPlayer = (currPlayer+1)%2;
		skillInUse = 0;
		skillsUsed.Reset();
		playerDone = false;
	}
	
	public void StartNewGame(){
		GameState tmp = Stats.startState;
		if (tmp == null){
			tmp = new GameState();
		}
		
		totalArea = Stats.fieldSize*Stats.fieldSize;
		//currPlayer = tmp.startingPlayer;
		currPlayer = 0;
		playField = new Field<Route>(tmp.field);
		playerSkill[0] = tmp.player0Skills;
		playerSkill[1] = tmp.player1Skills;
		
		player[0] = tmp.player0Score;
		player[1] = tmp.player1Score;

		extraSkillCap = tmp.globalSkillCap;
		placedPieces = tmp.placedPieces;
		
		skillInUse = 0;
		skillsUsed = new SkillContainer();
		playerDone = false;
		sound.PlaySound(SoundType.background);

		BroadcastMessage("InitField");
	}
		
		
	//merge findCluster-functions (as in android)
	private Field<bool> FindClusterRecurse( FieldIndex ind, Field<bool> taken){
		taken[ind] = true;
		//Debug.Log("FindCluster, NB's: "+ind.LogStraightNeighbours());
		foreach( FieldIndex i in ind.GetStraightNeighbours() ){
			if( playField[i] == playField[ind] && taken[i] == false ){
				//Debug.Log("calling FCR from "+i.x+", "+i.y+"...");
				taken = FindClusterRecurse(i, taken);
			}
		}
		return taken;
	}
	
	private Field<bool> FindDiagClusterRecurse( FieldIndex ind, Field<bool> takenDiag){
		takenDiag[ind] = true;
		foreach( FieldIndex i in ind.GetDiagNeighbours() ){
			if( playField[i] == playField[ind] && takenDiag[i] == false ){
				takenDiag = FindDiagClusterRecurse(i, takenDiag);
			}
		}
		return takenDiag;
	}

	
}