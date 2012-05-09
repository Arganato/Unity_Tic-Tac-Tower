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
	
	private Turn activeTurn;
	private Sound sound;
	
	void Awake () {
		sound = (Sound)FindObjectOfType(typeof(Sound));
		if (sound == null){
			Debug.LogError("sound-object not found");
		}
		Skill.Init(this);
		Console.Init(this);
		player[0] = new Player();
		player[1] = new Player();

	}
	
	void Start(){
		StartNewGame();
	}
	
	public void UserFieldSelect(FieldIndex index){
		//Called when the user clicks on the field
		Order o = new Order();
		o.endTurn = false;
		switch(Skill.skillInUse){
			case 0:
				o.skill = SkillType.place;
				o.position = index;
				break;
			case 1:
				o.skill = SkillType.shoot;
				o.position = index;
				break;
			case 2:
				o.skill = SkillType.build;
				o.position = index;
				break;
			case 3:
				o.skill = SkillType.emp;
				break;
		}
		ExecuteOrder(o);
	}
	
	public void UserEndTurn(){
		Order o = new Order();
		o.skill = SkillType.noSkill;
		o.endTurn = true;
		ExecuteOrder(o);
	}
	
	private bool CheckCluster(FieldIndex index){ //rename?
		//Finds a cluster from a field index recursively
		//calls appropriate FindTower-functions on this cluster
		//reports found towers
		//returns true if a piece was placed
		
		// **DEBUG** tilpasse funksjonalitet til silence
		Field<bool> cluster = new Field<bool>(false); 

		cluster = Tower.FindAllClusterRecurse(index,cluster);
		List<Tower> tower = Tower.FindTower(cluster);
		

//			Debug.Log(activePlayer + " silenced: " + player[activePlayer].silenced);
		if(!player[activePlayer].silenced){
			player[activePlayer].AddScore(tower.Count);
			foreach( Tower t in tower){
				//Checking for victory
				if(t.type == TowerType.five){
					Victory();
				}
				//Coloring the towers:
				foreach(FieldIndex i in t.GetList()){ 
					
					// **DEBUG** lage GetDarkColor-funksjon
					playField[i] = Field<int>.GetDarkRoute(playField[i]);						
				}
				//reporting the towers:
				ReportTower(t);
			}
		}else if(tower.Count > 0){ //if a tower was found that was blocked by Silence
			return false;
		}
		BroadcastMessage("UpdateField");
		return true;

	}
	
	private void Victory(){
		sound.PlaySound(SoundType.victory);
		player[activePlayer].score += 1000;
		Console.PrintToConsole("Player "+(activePlayer+1)+" has won!",Console.MessageType.INFO);
		//active player has won!
		//TODO: stuff
	}
	
	public bool ExecuteOrder(Order o){
		// Executes an order from the order-format
		// TODO: make all orders go through this by having a wrapper function
		bool validMove = false;
		switch(o.skill){
		case SkillType.noSkill:
			validMove = true;
			break;
		case SkillType.place:
			validMove = PlacePiece(o.position);
			break;
		case SkillType.shoot:
			if(Skill.CanUseShoot() == SkillSelectError.NO_ERROR){
				validMove = Shoot(o.position);
			}else{
				validMove = false;
			}
			break;
		case SkillType.build:
			if(Skill.CanUseBuild() == SkillSelectError.NO_ERROR){
				validMove = ExtraBuild(o.position);
			}else{
				validMove = false;
			}
			break;
		case SkillType.emp:
			if(Skill.CanUseSilence() == SkillSelectError.NO_ERROR){
				validMove = EMP();
			}else{
				validMove = false;
			}
			break;
		}
		if(validMove){
			activeTurn.Add(o);
			if(o.endTurn && playerDone){
				EndTurn();
			}else if(!playerDone){
				Console.PrintToConsole("You are trying to end the turn without placing your piece",Console.MessageType.ERROR);	
			}
		}
		return validMove;
	}
	
	public void ExecuteTurn(Turn t){
		if(t.IsValid()){
			foreach( Order o in t.GetOrderList()){
				if(!ExecuteOrder(o))
					break;
			}
		}else{
			Debug.LogError("invalid turn-code");
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
	
	
	//Move to Skill-class?
	private bool PlacePiece(FieldIndex index){ //placing piece in a normal turn
		if (playerDone == false && playField[index] == Route.empty){
//			Debug.Log("Index: " + index);
			sound.PlaySound(SoundType.onClick);
			if(activePlayer == 0){
				playField[index] = Route.red;
			}else{
				playField[index] = Route.blue;
			}
			if(CheckCluster(index)){
				IncPieceCount();
				playerDone = true;
			}else{ //the move is illegal due to silence
				playField[index] = Route.empty;
				Console.PrintToConsole("You are silenced; You can't build towers",Console.MessageType.ERROR);
				return false;
			}
			return true;
		}else{
			if(playerDone){
				Console.PrintToConsole("Can only place one piece each turn",Console.MessageType.ERROR);
			}else{
				Console.PrintToConsole("Cannot place there",Console.MessageType.ERROR);
			}
			sound.PlaySound(SoundType.error);
			// **DEBUG** write this out somehow
			return false;
		}
	}
	//Move to Skill-class?
	private bool ExtraBuild(FieldIndex index){ //placing an extra piece with the build-skill
		if (playField[index] == Route.empty){
		
			playField[index] = Field<int>.GetPlayerColor(activePlayer);
			player[activePlayer].playerSkill.build--;
			Skill.skillsUsed.build++;
			
			CheckCluster(index);
			IncPieceCount();
			Skill.skillInUse = 0;
			sound.PlaySound(SoundType.build);
			return true;
		}else{
			Debug.Log("invalid move");
			Console.PrintToConsole("Cannot place there",Console.MessageType.ERROR);
			sound.PlaySound(SoundType.error);
			return false;
			// **DEBUG** write this out somehow
		}
		//do not change first player
	}
	//Move to Skill-class?
	private bool Shoot(FieldIndex index){ //select an enemy piece to destroy it
	
		if (playField[index] == Field<int>.GetPlayerColor( (activePlayer+1)%2 ) ){
			playField[index] = Route.destroyed;
			player[activePlayer].playerSkill.shoot--;
			Skill.skillsUsed.shoot++;
			Skill.skillInUse = 0;
			BroadcastMessage("UpdateField");
			sound.PlaySound(SoundType.shoot);
			return true;
		}else{
			Console.PrintToConsole("Can only shoot active enemy pieces",Console.MessageType.ERROR);
			Debug.Log("invalid move");
			sound.PlaySound(SoundType.error);
			return false;
			//write this out somehow
		}
	}	
	//Move to Skill-class?
	public bool EMP(){
		Debug.Log("player "+(activePlayer+1)+" has used EMP");
		Console.PrintToConsole("player "+(activePlayer+1)+" has used EMP!",Console.MessageType.INFO);
		Skill.skillsUsed.emp++;
		player[activePlayer].playerSkill.emp--;
		player[(activePlayer+1)%2].silenced = true;
		sound.PlaySound(SoundType.emp);
		return true;
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
	
	private void EndTurn(){
		ChangeActivePlayer();
		Console.PrintToConsole(activeTurn.ToString(),Console.MessageType.TURN);
		activeTurn = new Turn();
		//ADD:
		// set undo-point
	}
	
	private void ChangeActivePlayer(){
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
			Stats.SetDefaultSettings();
			Debug.Log("No settings found. Using default settings");
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
		activeTurn = new Turn();
		BroadcastMessage("InitField");
	}
}