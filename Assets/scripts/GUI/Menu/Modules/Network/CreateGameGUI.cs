using UnityEngine;
using System.Collections;

public class CreateGameGUI : MenuContent, INetworkMessage{
	
	private enum State {DefaultState, LaunchingServer, FailedToLaunchServer}
	
	public Rect position =  new Rect(0,0,300,440);
	
	private string gameName = "new game";
	private string password = "";
	private NetworkInterface networkInterface;
	private MainMenu mainMenu; //callback
	private State state = State.DefaultState;
	
	public CreateGameGUI(NetworkInterface nif, MainMenu m){
		networkInterface = nif;
		networkInterface.AddMessageRecipient((INetworkMessage)this);
		mainMenu = m;
		SetUpScreen();
	}
	
	private void  SetUpScreen(){
		if( Screen.width >= 300){
			position.x = Screen.width/2 - position.width/2;
		}else{
			position.x = 0; 
			position.width = Screen.width;
		}if(Screen.height >= 480){
			position.y = Screen.height/2+20 - position.height/2;
		}else{
			position.y = 0;
			position.height = Screen.height-40;
		}
	}
	public override void Close (){
		networkInterface.RemoveMessageRecipient((INetworkMessage)this);
		networkInterface.Disconnect();
	}


	public override void PrintGUI(){
		GUILayout.BeginArea(position);
		switch(state){
		case State.DefaultState:
			DefaultGUI();
			break;
		case State.LaunchingServer:
			LaunchingGUI();
			break;
		case State.FailedToLaunchServer:
			FailedToConnectGUI();
			break;
		}
		GUILayout.EndArea();
	}
	private void DefaultGUI(){
		GUILayout.FlexibleSpace();
		GUILayout.BeginHorizontal();
		GUILayout.Label("Game name: ",GUILayout.ExpandWidth(false));
		gameName = GUILayout.TextField(gameName);
		GUILayout.EndHorizontal();
		
		GUILayout.FlexibleSpace();	
		GUILayout.BeginHorizontal();
		GUILayout.Label("Password: ",GUILayout.ExpandWidth(false));
		password = GUILayout.TextField(password);
		GUILayout.EndHorizontal();
		GUILayout.Label("Leave empty for no password");

		GUILayout.FlexibleSpace();
		GUILayout.BeginHorizontal();
		if(GUILayout.Button("Create Game")){
			CreateGame();
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		
	}
	
	private void LaunchingGUI(){
		string connectLabel;
		if(Network.peerType == NetworkPeerType.Disconnected){
			connectLabel = "Connection failed";
		}else if(Network.peerType == NetworkPeerType.Connecting){
			connectLabel = "Connecting...";
		}else{
			connectLabel = "Connected!";
		}
//		GUILayout.Space(20);
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.Label(connectLabel);
		GUILayout.EndHorizontal();
		GUILayout.BeginArea(new Rect(position.width/2-150,position.height/2-25,300,50),"","darkBoxCentered");
		GUILayout.Label("Launching server...");
		GUILayout.EndArea();
		GUILayout.FlexibleSpace();
		if(GUILayout.Button("Abort",GUILayout.ExpandWidth(false))){
			state = State.FailedToLaunchServer;
			networkInterface.Disconnect();
			//fancy abort-action here?
		}
	}
	
	private void FailedToConnectGUI(){
		GUILayout.Label("Failed to create server!");
		DefaultGUI();
	}

	private void CreateGame(){
		if(password != ""){
			Network.incomingPassword = password;
		}
		networkInterface.LaunchServer(Network.HavePublicAddress(),gameName);
		state = State.LaunchingServer;
	}
	
	private void FinishCreateGame(){
		StartGameScreen startGame = new StartGameScreen(networkInterface,false);
		mainMenu.AddMenu(startGame);
		state = State.DefaultState;
	}
	
	//interface functions
	
	public void StartGameMessage (){}
	
	public void ConnectionStatus (ConnectionMessage msg)
	{
		Debug.Log("network message to CreateGameGUI: "+msg);
		switch(msg){
		case ConnectionMessage.serverInit:
			FinishCreateGame();
			break;
		case ConnectionMessage.serverInitFailed:
			state = State.FailedToLaunchServer;
			break;
		}
	}
	public void ChatMessage (string msg){}
}
