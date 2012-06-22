using UnityEngine;
using System.Collections;

public class NetworkInterface : MonoBehaviour {
	
	private bool pollHostList = false;
	
	private NetworkGUI networkGUI; 	//link to the GUI
	private Control control;		//link to Control
	
	private const int port = 25000; 
	
	private bool useNat = false;
	
	void Start(){
		Console.Init(this);
		control = (Control)FindObjectOfType(typeof(Control));
		DontDestroyOnLoad(gameObject);
	}

	public void RegisterGUI(NetworkGUI ngui){
		networkGUI = ngui;
	}
	
	public void ConnectToServer(string ip, bool nat){
		useNat = nat;
		if(useNat){
			Network.Connect(ip); //the string supplied should be the GUID
		}else{
			Network.Connect(ip,port);
		}
	}
	
	public void ConnectToServer(HostData game){
		MasterServer.ClearHostList();
		MasterServer.RequestHostList(Stats.uniqueGameID);
		pollHostList = true;
	}
	
	private void FinishMasterConnection(HostData game){ 
	//called when the host list is received when connecting through the master server
		Network.Connect(game);
	}
	
	public void LaunchServer(bool nat, bool connectToMaster){
		useNat = nat;
		Network.InitializeServer(1,port,nat);
		if(connectToMaster){
			MasterServer.RegisterHost(Stats.uniqueGameID,"New Game");
		}
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
	
	public void SendGameStats(){
		string pck = Stats.MakeNetworkPackage();
		networkView.RPC ("ReceiveGameStats",RPCMode.Others,networkView.viewID,pck);
	}
	
	[RPC]
	public void ReceiveGameStats(NetworkViewID id, string pck){
		Stats.ReadNetworkPackage(pck);
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
		if(networkGUI != null){
			Debug.Log("usenat: "+useNat);
			if(useNat){
				networkGUI.AddLogEntry("Created server with GUID "+Network.player.guid+".");
			}else{
				networkGUI.AddLogEntry("Created server with IP "+Network.player.ipAddress+"/"+Network.player.port+".");
			}
		}
	}
	
	//Messages
	
	void Update(){
		if(pollHostList){
			if(MasterServer.PollHostList().Length != 0){
				HostData[] hostlist = MasterServer.PollHostList();
				Debug.Log("game found: " +hostlist[0].gameName+", number of games: "+hostlist.Length+".");
				pollHostList = false;
				FinishMasterConnection(hostlist[0]);
			}
		}
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

	void OnFailedToConnectToMasterServer(NetworkConnectionError info) {
		Debug.Log("Could not connect to master server: " + info);
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent) {
        if (msEvent == MasterServerEvent.RegistrationSucceeded)
            Debug.Log("Server registered");
        else
			Debug.Log("unknown message: "+msEvent);
    }
	
}
