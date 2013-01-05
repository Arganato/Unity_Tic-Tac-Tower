using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Control: MonoBehaviour, EffectInterface {

//	public bool playerDone = false; //Is true when player has placed a piece. Allows user to "End Turn".	
	public static GameState cState; //the current gamestate of the progressing game
	private static GameState startOfTurn; //the undo-point
	
	private Turn activeTurn;
	private Sound sound;
	private GraphicalEffectFactory graphicalEffectFactory;
	private static NetworkInterface networkInterface;
	private bool networkSendMessageFlag = true;
//	private AI ai = new AI(); //for debug
		
	void Awake () {
		sound = (Sound)FindObjectOfType(typeof(Sound));
		graphicalEffectFactory = (GraphicalEffectFactory)FindObjectOfType(typeof(GraphicalEffectFactory));
		networkInterface = (NetworkInterface)FindObjectOfType(typeof(NetworkInterface));

		if (sound == null){
			Debug.LogError("sound-object not found");
		}
		if (graphicalEffectFactory == null){
			Debug.LogError("graphicalEffectFactory-object not found");
		}
		Console.Init(this);
	}
	
	void Start(){
		StartNewGame();
	}

	public bool UserFieldSelect(FieldIndex index){
		// Called when the user clicks on the field
		// Returns true if a the click resulted in the execution of a (valid) move
		bool validmove = false;
		if(Stats.playerController[cState.activePlayer] == Stats.PlayerController.localPlayer){
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
					o.skill = SkillType.silence;
					break;
			}
			validmove = ExecuteOrder(o);
			if(cState.victory != VictoryType.noVictory)
				Victory();
		}
		return validmove;
	}
	
	public void UserEndTurn(){
		Order o = new Order();
		o.skill = SkillType.noSkill;
		o.endTurn = true;
		ExecuteOrder(o);
	}
	
	public void UserResign(){
		Victory((cState.activePlayer+1)%2);
	}
	
	public void TimeOut(){
		Victory((cState.activePlayer+1)%2);
	}
	
	
	private void Victory(int player){
		sound.PlayEffect(SoundType.victory);
		EndTurn();
		Console.PrintToConsole("Player "+(player+1)+" has won!",Console.MessageType.INFO);
		Stats.gameRunning = false;
		PopupMessage.DisplayMessage("Player "+(player+1)+" has won!",10f);
		BroadcastMessage("OnVictory",SendMessageOptions.DontRequireReceiver);
	}
	
	private void Victory(){
		if (cState.victory == VictoryType.playerOne){
			Victory(0);
		}else if (cState.victory == VictoryType.playerTwo){
			Victory(1);		
		}
	}
	
	public bool ExecuteOrder(Order o){
		// Executes an order from the order-format
		if(cState.EvaluateOrder(o)){
			activeTurn.Add(o);
			if(o.endTurn && cState.playerDone){
				EndTurn();
			}else if(o.endTurn){
				Console.PrintToConsole("You are trying to end the turn without placing your piece",Console.MessageType.ERROR);	
			}
			if (cState.victory != VictoryType.noVictory){
				Victory();
			}
			BroadcastMessage("UpdateField");
			Skill.skillInUse = 0;
			return true;
		}
		return false;
	}
	
	public void ExecuteTurn(Turn t){
		if(t.IsValid()){
			foreach( Order o in t.GetOrderList()){
				if(!ExecuteOrder(o)){
					Debug.LogError("invalid order-code: "+o.ToString());
					break;
				}
			}
		}else{
			Debug.LogError("invalid turn-code: "+t.ToString());
		}
	}
	
	public void ExecuteNetworkTurn(Turn t){
		networkSendMessageFlag = false;
		ExecuteTurn(t);
	}
	
	
	private void EndTurn(){
		cState.ChangeActivePlayer();
		Console.PrintToConsole(activeTurn.ToString(),Console.MessageType.TURN);
		if(Stats.hasConnection){
			if(networkSendMessageFlag){
				networkInterface.SendTurn(activeTurn.ToString());
			}else{
				networkSendMessageFlag = true;
			}
		}
		activeTurn = new Turn();
		cState.playerDone = false;
		Skill.skillInUse = 0;
		startOfTurn = new GameState(cState);
		
//		//TEST:
//		Debug.Log("Calling AI");
//		ai.Calculate(); 
	}
	
	public void StartNewGame(){
		GameState tmp = Stats.startState;
		Stats.gameRunning = true;
		Console.Clear();
		if (tmp == null){
			tmp = new GameState();
			tmp.field = GameBoardFactory.SquareBoard();
			Stats.SetDefaultSettings();
			Stats.StartGame();
			Debug.Log("No settings found. Using default settings");
		}
		
		cState = new GameState(tmp);
		cState.effectInterface = (EffectInterface)this;		
		startOfTurn = new GameState(tmp);
		
		Skill.skillInUse = 0;
		cState.playerDone = false;
		
		activeTurn = new Turn();
		BroadcastMessage("InitField");
	}
	
	public void UndoTurn(){
		GameTime gt = cState.player[cState.activePlayer].gameTime;
		cState = new GameState(startOfTurn);
		cState.effectInterface = (EffectInterface)this;	
		cState.player[cState.activePlayer].gameTime = gt; // so that you cant undo the time
		cState.playerDone = false;
		activeTurn = new Turn();
		Skill.skillInUse = 0;
		cState.skillsUsed.Reset();
		BroadcastMessage("UpdateField");
	}
	
	public static void QuitGame(){
		//cleans up stuff, and makes ready to quit the game
//		NetworkInterface nif = (NetworkInterface)FindObjectOfType(typeof(NetworkInterface));
//		if(nif != null){
//			nif.DestroySelf();
//		}
		Application.LoadLevel("mainMenu");
	}
	
	public static void SetUndoPoint(GameState state){
		//changes game state and sets undo-point
		startOfTurn = new GameState(state);
	}

	public void PlaySilenceEffect ()
	{
		graphicalEffectFactory.SilenceEffect();
	}
	
	public void PlaySound (SoundType type)
	{
		sound.PlayEffect(type);
	}
	
	public void PlayBuildingConstructionEffect (List<Tower> towersCreated)
	{
		graphicalEffectFactory.BuildingConstructionEffect(towersCreated);
	}
}