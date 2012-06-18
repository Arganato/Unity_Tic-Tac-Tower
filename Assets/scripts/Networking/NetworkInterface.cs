using UnityEngine;
using System.Collections;

public class NetworkInterface : MonoBehaviour {
	
	private NetworkGUI networkGUI;
	private Control control;
	
	void Start(){
		Console.Init(this);
		control = (Control)FindObjectOfType(typeof(Control));
	}

	public void TestConnection(){
		
	}

	public void RegisterGUI(NetworkGUI ngui){
		networkGUI = ngui;
	}
	
	public void ConnectToServer(string ip){
		Network.Connect(ip,25000);
	}
	
	public void LaunchServer(){
		Network.InitializeServer(1,25000,false);
	}
	
	public void Disconnect(){
		Network.Disconnect();
	}
	
	public void SendChatMessage(string pck){
		networkView.RPC("ReceiveChatMessage",RPCMode.Others, networkView.viewID, pck);
	}
	
	public void SendTurn(string pck){
		networkView.RPC("ReceiveTurn",RPCMode.Others, networkView.viewID, pck);
		Debug.Log("Turn sent: "+pck);
	}
	
	private void SendHeartbeat(){
		if(Network.isServer){
			networkView.RPC("ReceiveHeartbeat",RPCMode.Others,networkView.viewID, Network.time);
		}else{
			networkView.RPC("ReceiveHeartbeat",RPCMode.Server,networkView.viewID, Network.time);			
		}
	}
	
	[RPC]
	public void ReceiveChatMessage(NetworkViewID id, string pck){
		Debug.Log("Package received: "+pck);
		if(networkGUI != null){
			networkGUI.AddChatMessage(pck,networkView.name);
		}
	}
	[RPC]
	public void ReceiveHeartbeat(NetworkViewID id, float timestamp){
		if(!Network.isServer){
			SendHeartbeat();
		}
		//receive heartbeat somehow
	}
	[RPC]
	public void ReceiveTurn(NetworkViewID id, string pck){
		Turn turn = Turn.StringToTurn(pck);
		control.ExecuteTurn(turn);
	}
	
	void OnServerInitialized(){
		if(networkGUI != null)
			networkGUI.AddLogEntry("Created server at IP "+Network.player.ipAddress+"/"+Network.player.port+".");
	}
	
	void OnConnectedToServer(){
		if(networkGUI != null){
			networkGUI.AddLogEntry("Successfully connected to server @"+Network.connections[0].ipAddress+"/"+Network.connections[0].port);
		}
		Stats.hasConnection = true;
		Stats.playerController[0] = Stats.PlayerController.remotePlayer;
	}
	
	void OnDisconnectedFromServer(){
		if(networkGUI != null)
			networkGUI.AddLogEntry("Disconnected from server");
	}
	
	void OnPlayerDisconnected(NetworkPlayer player){
		if(networkGUI != null)
			networkGUI.AddLogEntry("Player "+player+" has disconnected");
		Stats.hasConnection = false;
		Stats.playerController[1] = Stats.PlayerController.localPlayer;
	}
	
	void OnPlayerConnected(NetworkPlayer player){
		if(networkGUI != null)
			networkGUI.AddLogEntry("player "+player+" connected to the server!");
		Stats.hasConnection = true;
		Stats.playerController[1] = Stats.PlayerController.remotePlayer;
	}
	
	
}
