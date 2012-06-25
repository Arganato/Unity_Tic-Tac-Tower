using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkInterface : MonoBehaviour {
	
	private Control control;		//link to Control
	private List<INetworkMessage> messageRecipients = new List<INetworkMessage>();
		
	private const int port = 25000; 
	
	private bool useNat = false;
	private bool useMasterServer = true;
	private string gameName = "";
	
	void Start(){
		Console.Init(this);
		DontDestroyOnLoad(gameObject);
	}
	
	public void DestroySelf(){
		Disconnect();
		Destroy(gameObject);
	}
	
	private void FindControl(){
		control = (Control)FindObjectOfType(typeof(Control));
	}

	public void AddMessageRecipient(INetworkMessage inst){
		messageRecipients.Add(inst);
	}
	public void RemoveMessageRecipient(INetworkMessage inst){
		messageRecipients.Remove(inst);
	}

	public void ConnectToServer(string ip, bool nat, string password = ""){
		useNat = nat;
		if(useNat){
			Network.Connect(ip,password); //the string supplied should be the GUID
		}else{
			Network.Connect(ip,port,password);
		}
	}
	
	public void ConnectToServer(HostData game, string password = ""){
		Network.Connect(game,password);	
	}
		
	public void LaunchServer(bool nat){
		useNat = nat;
		useMasterServer = false;
		Network.InitializeServer(1,port,nat);
	}
	
	public void LaunchServer(bool nat, string gameName){
		useNat = nat;
		useMasterServer = true;
		this.gameName = gameName;
		Network.InitializeServer(1,port,nat);
		Debug.Log("gameName = "+gameName);
	}

	public void Disconnect(){
		Network.Disconnect();
		Stats.hasConnection = false;
		Stats.playerController[0] = Stats.PlayerController.localPlayer;
		Stats.playerController[1] = Stats.PlayerController.localPlayer;
	}
	
	public void SendChatMessage(string pck){
		networkView.RPC("ReceiveChatMessage",RPCMode.Others, networkView.viewID, pck);
	}
	
	public void SendTurn(string pck){
		networkView.RPC("ReceiveTurn",RPCMode.Others, networkView.viewID, pck);
		Debug.Log("Turn sent: "+pck);
	}
	
//	private void SendHeartbeat(){
//		if(Network.isServer){
//			networkView.RPC("ReceiveHeartbeat",RPCMode.Others,networkView.viewID, Network.time);
//		}else{
//			networkView.RPC("ReceiveHeartbeat",RPCMode.Server,networkView.viewID, Network.time);			
//		}
//	}
	
	public void SendGameStats(){
		string pck = Stats.MakeNetworkPackage();
		networkView.RPC ("ReceiveGameStats",RPCMode.Others,networkView.viewID,pck);
	}
	
	public void SendStartGame(){
		networkView.RPC ("ReceiveStartGame",RPCMode.Others,networkView.viewID,true);		
	}
	
	[RPC]
	public void ReceiveStartGame(NetworkViewID id, bool dummy){
		RelayStartMessage();
	}
	
	[RPC]
	public void ReceiveGameStats(NetworkViewID id, string pck){
		Stats.ReadNetworkPackage(pck);
	}
	
	[RPC]
	public void ReceiveChatMessage(NetworkViewID id, string pck){
		Debug.Log("Package received: "+pck);
		RelayChatMessage("Player: "+pck);
		//TODO: add player name
	}
//	[RPC]
//	public void ReceiveHeartbeat(NetworkViewID id, float timestamp){
//		if(!Network.isServer){
//			SendHeartbeat();
//		}
//		//receive heartbeat somehow
//	}
	[RPC]
	public void ReceiveTurn(NetworkViewID id, string pck){
		Turn turn = Turn.StringToTurn(pck);
		if(control == null)
			FindControl();
		if(control == null)
			Debug.LogError("script control not found!");
		else{
			control.ExecuteTurn(turn);
		}
	}

	//Messages	
	
	void OnServerInitialized(){
		if(useMasterServer){
			MasterServer.RegisterHost(Stats.uniqueGameID,gameName);
		}else{
			RelayConnectionStatus(ConnectionMessage.serverInit);
		}
//			if(useNat){
//				networkGUI.AddLogEntry("Created server with GUID "+Network.player.guid+".");
//			}else{
//				networkGUI.AddLogEntry("Created server with IP "+Network.player.ipAddress+"/"+Network.player.port+".");
//			}
	}
	
	
	void OnConnectedToServer(){
		RelayConnectionStatus(ConnectionMessage.connected);
		Debug.Log("Successfully connected to server @"+Network.connections[0].ipAddress+"/"+Network.connections[0].port);
		Stats.hasConnection = true;
		Stats.playerController[0] = Stats.PlayerController.remotePlayer;
	}
	
	void OnDisconnectedFromServer(){
		RelayConnectionStatus(ConnectionMessage.disconnected);
	}
		
	void OnPlayerDisconnected(NetworkPlayer player){
		RelayConnectionStatus(ConnectionMessage.playerDisconnected);
		Stats.hasConnection = false;
		Stats.playerController[1] = Stats.PlayerController.localPlayer;
	}
	
	void OnPlayerConnected(NetworkPlayer player){
		RelayConnectionStatus(ConnectionMessage.playerConnected);
		Stats.hasConnection = true;
		Stats.playerController[1] = Stats.PlayerController.remotePlayer;
	}

	void OnFailedToConnectToMasterServer(NetworkConnectionError info) {
		RelayConnectionStatus(ConnectionMessage.connectFailed);
		Debug.Log("Could not connect to master server: " + info);
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent) {
		switch(msEvent){
		case MasterServerEvent.RegistrationSucceeded:
			RelayConnectionStatus(ConnectionMessage.serverInit);
			break;
		}
    }
	
	//Event relay functions
	
	private void RelayStartMessage(){
		foreach( INetworkMessage i in messageRecipients){
			i.StartGameMessage();
		}
	}
	
	private void RelayConnectionStatus( ConnectionMessage msg){
//		foreach( INetworkMessage i in messageRecipients){
//			i.ConnectionStatus(msg);
//		}
//		
		for(int i=0; i<messageRecipients.Count;i++){
			messageRecipients[i].ConnectionStatus(msg);
		}
		
	}
	
	private void RelayChatMessage(string msg){
		foreach( INetworkMessage i in messageRecipients){
			i.ChatMessage(msg);
		}
	}
	
}
