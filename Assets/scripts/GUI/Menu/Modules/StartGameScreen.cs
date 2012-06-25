using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StartGameScreen : MenuContent, INetworkMessage {


	public Rect position = new Rect(0,40,Screen.width,Screen.height-80);
	public bool localGame;
	public bool readOnly;
	private bool lostConnection = false;
	
	private SelectGameTime selectGameTime = new SelectGameTime();
	private GUIList selectRules;
	private GUIList selectTowers;
	private NetworkInterface networkInterface = null;
	
	private void SetUp(){
		selectRules = new GUIList(new Rect(100,50,180,25));
		selectRules.AddElement("Disappearing towers");
		selectRules.AddElement("Solid towers");
		selectTowers = new GUIList(new Rect(100,100,180,25));
		selectTowers.AddElement("All towers");
		selectTowers.AddElement("Straight only");
		selectGameTime.position.y = 180;
		selectGameTime.position.x = 0;
		selectGameTime.enable = false; //spill uten tid er default
		Stats.SetDefaultSettings();
	}
	public StartGameScreen(){
		SetUp();
		readOnly = false;
		localGame = true;
	}
	public StartGameScreen(NetworkInterface netif, bool readOnly){
		SetUp();
		networkInterface = netif;
		this.readOnly = readOnly;
		networkInterface.AddMessageRecipient((INetworkMessage) this);
	}
	
	public override void Close (){
		if(!localGame){
			networkInterface.RemoveMessageRecipient((INetworkMessage)this);
			networkInterface.Disconnect();
		}
	}


	public override void PrintGUI(){
		GUI.BeginGroup(position);
		if(!lostConnection){
			if(localGame)
				GUI.Box(new Rect(0,0,position.width,25),"Start Local Game");
			else
				GUI.Box(new Rect(0,0,position.width,25),"Networked play (beta)");
				
			if(!localGame && readOnly){
				string rules = "Rules: "+Stats.rules.ToString();
				GUI.Label(selectRules.position,rules);
				
				//Hacka sammen utskrift for towers, siden det bare er to muligheter:
				string towers;
				if(Stats.skillEnabled.build && Stats.skillEnabled.diagBuild)
					towers = "Towers: all towers";
				else
					towers = "Towers: Straight only";
				GUI.Label(selectTowers.position,towers);
			}else{
				if(selectRules.PrintGUI())
					UpdateStats();
		
				if(selectTowers.PrintGUI())
					UpdateStats();
			}
			if(localGame){
				selectGameTime.enable = GUI.Toggle(new Rect(20,160,100,25),selectGameTime.enable, "Time Cap");
				selectGameTime.PrintGUI();
			}else{
				ChatGUI();
			}
			if(localGame || !readOnly){
				if(GUI.Button(new Rect(20,position.height-25,100,25),"Start Game")){
					if(!localGame)
						networkInterface.SendStartGame();
					StartGame();
				}
			}
		}else{ //if lost connection
			GUI.Box(new Rect(position.width/2-150,position.height/2-25,300,50),"Connection lost...","darkBoxCentered");
		}
		GUI.EndGroup();
	}
		
	// The chat
	
	private Vector2 chatScrollPos = Vector2.zero;
	private string chatField = "";
	private string chatMessage = "";
	private void ChatGUI(){
		GUILayout.BeginArea(new Rect(10,180,position.width-20,position.height-205),"","darkBox");
		for(int i=0; i < networkInterface.GetConnectedPlayers(); i++){
			GUILayout.Label(networkInterface.GetPlayerName(i),GUILayout.ExpandHeight(false));
		}
		chatScrollPos = GUILayout.BeginScrollView(chatScrollPos);
		GUILayout.TextArea(chatField,GUILayout.ExpandHeight(true),GUILayout.ExpandWidth(true));
		GUILayout.EndScrollView();
		GUILayout.BeginHorizontal();
		chatMessage = GUILayout.TextField(chatMessage,GUILayout.Width(position.width-80));
		if(GUILayout.Button("send",GUILayout.ExpandWidth(true))){
			networkInterface.SendChatMessage(chatMessage);
			chatField += "Me: "+chatMessage+"\n";
			chatMessage = "";
		}
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}
	
	private void UpdateStats(){
		switch(selectRules.choice){
		case 0:
			Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
			break;
		case 1:
			Stats.rules = Stats.Rules.SOLID_TOWERS;
			break;
		}
		
		switch(selectTowers.choice){
		case 0:
			Stats.skillEnabled.SetAll(true);
			break;
		case 1:
			Stats.skillEnabled.SetDiag(false);
			Stats.skillEnabled.SetStraight(true);
			break;
		}
		if(!localGame && networkInterface != null){
			networkInterface.SendGameStats();
		}
	}
	
	private void StartGame(){
		UpdateStats();
//		if(!readOnly)
//			networkInterface.SendStartGame();
		
		if(selectGameTime.enable){
			selectGameTime.SetGameTime();
		}
		Application.LoadLevel("game");
	}
	
	//Network messages
	
	public void ChatMessage (string msg)
	{
		chatField += msg+"\n";
		Debug.Log("chat message received!!");
	}
	
	public void ConnectionStatus (ConnectionMessage msg)
	{
		switch(msg){
		case ConnectionMessage.disconnected:
			lostConnection = true;
			break;
		case ConnectionMessage.playerDisconnected:
			
			break;
		}
			
	}
	
	public void StartGameMessage (){
		StartGame();
	}
}
