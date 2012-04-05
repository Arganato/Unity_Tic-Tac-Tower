using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Control: MonoBehaviour {

	public int placedPieces = 0;
	public int totalArea;
	public int extraSkillCap = 0;
	
	public Field<Route> playField;
	public bool playerDone = false; //Is true when player has placed a piece. Allows user to "End Turn".
	
	public int turn = 1; //1-indexed
	public int firstPlayer = 0; //player 1 starting; changed to 0-indexing
	public int skillInUse; // skil used by firstPlayer. 0 = no skill, 1 = shoot, 2 = build, etc
	
	//Skills:
	public SkillContainer[] playerSkill = new SkillContainer[2];

	public Score[] playerScore = new Score[2];
	
	public SkillContainer skillsUsed; //skill used this turn by firstPlayer
	
	private Sound sound;
	
	// Use this for initialization
	void Start () {
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
	
	private void CheckCluster(FieldIndex index){
		//Finds a cluster from a field index recursively
		//calls appropriate FindTower-functions on this cluster
		//reports found towers
		
		// **DEBUG** tilpasse funksjonalitet til silence
		if(playField[index] != Route.empty){
			Field<bool> cluster = new Field<bool>(9,false); 
			Field<bool> clusterDiag = new Field<bool>(9,false); 
			cluster = FindClusterRecurse(index,cluster);
			clusterDiag = FindDiagClusterRecurse(index,clusterDiag);
			List<Tower> tower = Tower.FindTower(cluster, clusterDiag);
			playerScore[firstPlayer].Add(tower.Count);
			foreach( Tower t in tower){
				//coloring the towers:
				if(t.towerType == TowerType.five){
					playerScore[firstPlayer].score += 1000;
					// *DEBUG* lage game-over screen her
					sound.PlaySound(SoundType.victory);
				}
				foreach(FieldIndex i in t.GetList()){ 
					
					// **DEBUG** lage GetDarkColor-funksjon
					if( firstPlayer == 0){
						playField[i] = Route.redBuilt;
					}else{
						playField[i] = Route.blueBuilt;
					}
				}
				//reporting the towers:
				ReportTower(t);
			}
		}
		BroadcastMessage("UpdateField");
	}
	
	public int UseSkill(int skill){
		//Returns an error message 
		switch (skill){
			case 0: //no skill
				skillInUse = 0;
				return 0;
			case 1: //shoot
				Debug.Log("shoot selected");
				if (playerSkill[firstPlayer].shoot > 0){
					if( skillsUsed.shoot <= playerSkill[firstPlayer].square + extraSkillCap){
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
				
				if ( playerSkill[firstPlayer].build > 0){
					if ( skillsUsed.build <= playerSkill[firstPlayer].square + extraSkillCap){
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
					if (playerSkill[firstPlayer].emp > 0){
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
	
	private void PlacePiece(FieldIndex index){ //placing piece in a normal turn
		if (playerDone == false && playField.At(index) == Route.empty){
			if(firstPlayer == 0){
				playField[index] = Route.red;
			}else{
				playField[index] = Route.blue;
			}
			CheckCluster(index);
			IncPieceCount();
			playerDone = true;
			sound.PlaySound(SoundType.onClick);
		}else{
			Debug.Log("invalid move");
			sound.PlaySound(SoundType.error);
			// **DEBUG** write this out somehow
		}
	}
	
	private void ExtraBuild(FieldIndex index){ //placing an extra piece with the build-skill
		if (playField.At(index) == Route.empty){
		
			playField[index] = Field<int>.GetPlayerColor(firstPlayer);
			playerSkill[firstPlayer].build--;
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
	
	private void Shoot(FieldIndex index){ //select an enemy piece to destroy it
	
		if (playField[index] == Field<int>.GetPlayerColor( (firstPlayer+1)%2 ) ){
			playField[index] = Route.destroyed;
			playerSkill[firstPlayer].shoot--;
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
	
	private void EMP(){
		Debug.Log("player "+firstPlayer+" has used EMP");
		skillsUsed.emp++;
		playerSkill[firstPlayer].emp--;
		sound.PlaySound(SoundType.emp);
		}
	
	
	private void DebugRemovePiece(FieldIndex index){ //removes a piece. should only be availible in debug mode
		if (playField.At(index) == Route.empty){		
			playField[index] = Field<int>.GetPlayerColor(firstPlayer);
		}else{
			//Debug.Log("changing field "+index.x+","+index.y+"to "+Route.empty);
			playField[index] = Route.empty;
		}
	}
	
	private void ReportTower(Tower t){
		switch(t.towerType){
			case TowerType.shoot:
				playerSkill[firstPlayer].shoot++;
				break;
			case TowerType.build:
				playerSkill[firstPlayer].build++;
				break;
			case TowerType.emp:
				playerSkill[firstPlayer].emp++;
				break;
			case TowerType.square:
				playerSkill[firstPlayer].square++;
				break;
		}
	}
	
	public void ChangeFirstPlayer(){
		playerScore[firstPlayer].EndTurn(playerSkill[firstPlayer].square);
		if(firstPlayer == 1){
			turn++;
		}
		firstPlayer = (firstPlayer+1)%2;
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
		//firstPlayer = tmp.startingPlayer;
		firstPlayer = 0;
		playField = new Field<Route>(tmp.field);
		playerSkill[0] = tmp.player0Skills;
		playerSkill[1] = tmp.player1Skills;
		
		playerScore[0] = tmp.player0Score;
		playerScore[1] = tmp.player1Score;

		extraSkillCap = tmp.globalSkillCap;
		placedPieces = tmp.placedPieces;
		
		skillInUse = 0;
		skillsUsed = new SkillContainer();
		playerDone = false;
		sound.PlaySound(SoundType.background);

		BroadcastMessage("InitField");
	}
		
		
	
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





