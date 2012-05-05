using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Control: MonoBehaviour {

	public int placedPieces = 0;
	public int totalArea;
	
	
	public static Field<Route> playField;
	public bool playerDone = false; //Is true when player has placed a piece. Allows user to "End Turn".
	
	public int turn = 1; //1-indexed
	public int activePlayer = 0; //player 1 starting; changed to 0-indexing
	
	public Player[] player = new Player[2];
	
	private Sound sound;
	
	void Awake () {
		sound = (Sound)FindObjectOfType(typeof(Sound));
		if (sound == null){
			Debug.LogError("sound-object not found");
		}
		Skill.Init(this);
		player[0] = new Player();
		player[1] = new Player();

	}
	
	void Start(){
		StartNewGame();
	}
	
	public void UserFieldSelect(FieldIndex index){
		//Called when the user clicks on the field
		switch(Skill.skillInUse){
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
			//Debug.Log(activePlayer + " silenced: " + player[activePlayer].silenced);
			
			
			//Coloring towers, and adding skills and score to players
			if(!player[activePlayer].silenced || tower.Count == 0){
				player[activePlayer].AddScore(tower.Count);
			}
//			Debug.Log(activePlayer + " silenced: " + player[activePlayer].silenced);
			if(!player[activePlayer].silenced || tower.Count == 0){
				player[activePlayer].AddScore(tower.Count);
				foreach( Tower t in tower){
					//Checking for victory
					if(t.type == TowerType.five){
						player[activePlayer].score += 1000;
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
	
	
	public void ExecuteOrder(Order o){
		// Executes an order from the order-format
		// TODO: make all orders go through this by having a wrapper function
		if (o.player == activePlayer){
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
			Skill.extraSkillCap = 2;
		}else if(placedPieces > totalArea/3){
			Skill.extraSkillCap = 1;
		}
	}
	
	
	//Move to Skill-class
	private void PlacePiece(FieldIndex index){ //placing piece in a normal turn
		if (playerDone == false && playField[index] == Route.empty){
			Debug.Log("Index: " + index);
			if(activePlayer == 0){
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
		
			playField[index] = Field<int>.GetPlayerColor(activePlayer);
			player[activePlayer].playerSkill.build--;
			Skill.skillsUsed.build++;
			
			CheckCluster(index);
			IncPieceCount();
			Skill.skillInUse = 0;
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
	
		if (playField[index] == Field<int>.GetPlayerColor( (activePlayer+1)%2 ) ){
			playField[index] = Route.destroyed;
			player[activePlayer].playerSkill.shoot--;
			Skill.skillsUsed.shoot++;
			Skill.skillInUse = 0;
			BroadcastMessage("UpdateField");
			sound.PlaySound(SoundType.shoot);
		}else{
			Debug.Log("invalid move");
			sound.PlaySound(SoundType.error);
			//write this out somehow
		}
	}
		
	//Move to Skill-class
	public void EMP(){
		Debug.Log("player "+activePlayer+" has used EMP");
		Skill.skillsUsed.emp++;
		player[activePlayer].playerSkill.emp--;
		player[(activePlayer+1)%2].silenced = true;
		sound.PlaySound(SoundType.emp);
	}
	
	private void ReportTower(Tower t){
		switch(t.type){
			case TowerType.shoot:
				player[activePlayer].playerSkill.shoot++;
				break;
			case TowerType.build:
				player[activePlayer].playerSkill.build++;
				break;
			case TowerType.emp:
				player[activePlayer].playerSkill.emp++;
				break;
			case TowerType.square:
				player[activePlayer].playerSkill.square++;
				break;
		}
	}
	
	public void ChangeCurrPlayer(){
		player[activePlayer].EndTurn(player[activePlayer].playerSkill.square);
		if(activePlayer == 1){
			turn++;
		}
		activePlayer = (activePlayer+1)%2;
		Skill.skillInUse = 0;
		Skill.skillsUsed.Reset();
		playerDone = false;
	}
	
	public void StartNewGame(){
		GameState tmp = Stats.startState;
		if (tmp == null){
			tmp = new GameState();
		}
		
		totalArea = Stats.fieldSize*Stats.fieldSize;
		//activePlayer = tmp.startingPlayer;
		activePlayer = 0;
		playField = new Field<Route>(tmp.field);
		player[0].playerSkill = tmp.player0Skills;
		player[1].playerSkill = tmp.player1Skills;
		
		player[0] = tmp.player0Score;
		player[1] = tmp.player1Score;

		Skill.extraSkillCap = tmp.globalSkillCap;
		placedPieces = tmp.placedPieces;
		
		Skill.skillInUse = 0;
		Skill.skillsUsed = new SkillContainer();
		playerDone = false;
		sound.PlaySound(SoundType.background);

		BroadcastMessage("InitField");
	}
}