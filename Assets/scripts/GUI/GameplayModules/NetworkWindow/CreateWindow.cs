using UnityEngine;
using System.Collections;

public class CreateWindow : MenuContent, INetworkMessage {

	private enum State {DefaultState, LaunchingServer, FailedToLaunchServer}
	
	
	private string gameName = "new game";
	private string password = "";
	private NetworkInterface networkInterface;
	private NetworkWindow mainMenu; //callback
	private State state = State.DefaultState;
	
	public CreateWindow(NetworkInterface nif, NetworkWindow m){
		networkInterface = nif;
		networkInterface.AddMessageRecipient((INetworkMessage)this);
		mainMenu = m;
	}
	
	
	public override void PrintGUI(){
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
		GUILayout.Label("Launching server...","darkBoxCentered");
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
		mainMenu.Back();
		mainMenu.Back();
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
	public void ChatMessage (string msg)
	{
		throw new System.NotImplementedException ();
	}
}


