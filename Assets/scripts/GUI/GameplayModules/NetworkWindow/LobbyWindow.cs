using UnityEngine;
using System.Collections;

public class LobbyWindow : MenuContent {

	private string password = "";
	private int selectedGame = -1;
	private bool joiningGame = false;
	private bool failedToJoin = false;
	
	private NetworkInterface networkInterface;
	private NetworkWindow networkWindow; 
	
	public LobbyWindow(NetworkInterface nif, NetworkWindow networkWin){
		networkInterface = nif;
		networkWindow = networkWin;
	}
	
	
	public override void PrintGUI ()
	{
		GUILayout.Space(20);
		if(!joiningGame){
			if(failedToJoin){
				GUILayout.Label("Failed to join game");
			}
			GUILayout.BeginHorizontal();
				GUILayout.Label("Lobby");
				if(GUILayout.Button("Refresh")){
					MasterServer.ClearHostList();
					MasterServer.RequestHostList(Stats.uniqueGameID);
				}
			GUILayout.EndHorizontal();
			LobbyList();
			GUILayout.FlexibleSpace();
			HostData[] pollList = Poll();
			if( selectedGame >= 0 && pollList[selectedGame].passwordProtected){
				GUILayout.BeginHorizontal();
				GUILayout.Box("Password:",GUILayout.ExpandWidth(false));
				password = GUILayout.TextField(password);
				GUILayout.EndHorizontal();
			}
			GUILayout.BeginHorizontal();
				if(selectedGame >= 0){
	
				if( GUILayout.Button("Join")){
					JoinGame(selectedGame,password);
				}
				}else if(selectedGame < 0){
					GUILayout.Box("Join");
				}
				if(GUILayout.Button("Create")){
					CreateGame();
				}
			GUILayout.EndHorizontal();
		}else{
			GUILayout.Box("Joining game","darkBoxCentered");
			if(GUILayout.Button("Abort")){
				joiningGame = false;
			}
		}
	}
	
	private void LobbyList(){
		int passwordWidth = 40;
		
		GUILayout.BeginHorizontal();
		GUILayout.Box("Game name");
		GUILayout.Box("PW", GUILayout.Width(passwordWidth));
		GUILayout.EndHorizontal();
		if(Poll().Length == 0){
			GUILayout.Label("No games found..."); 
		}else{
			HostData[] pollList = Poll();
			for(int i=0;i<pollList.Length;i++){
				GUILayout.BeginHorizontal();
				if(selectedGame == i){
					GUILayout.Box(pollList[i].gameName);
				}else{
					if(GUILayout.Button(pollList[i].gameName))
						selectedGame = i;
				}
				GUILayout.Box(pollList[i].passwordProtected ? "yes" : "no", GUILayout.Width(passwordWidth));
				GUILayout.EndHorizontal();
			}
		}
	}
	
	private void JoinGame(int gameNumber, string password){
		joiningGame = true;
		HostData[] hostArray = MasterServer.PollHostList();
		HostData game = hostArray[gameNumber];
		networkInterface.ConnectToServer(game,password);
	}
	
	private void EnterGame(){
		networkWindow.Back();
	}
	
	private void CreateGame(){
		CreateWindow create = new CreateWindow(networkInterface,networkWindow);
		networkWindow.AddMenu(create);
	}
	
	
	//network messages
	
	public void ChatMessage (string msg){}
	
	public void StartGameMessage (){}
	
	public void ConnectionStatus (ConnectionMessage msg)
	{
		switch(msg){
			
		case ConnectionMessage.connected:
			EnterGame();
			joiningGame = false;
			break;
		case ConnectionMessage.connectFailed:
			joiningGame = false;
			failedToJoin = true;
			break;
		}
			
	}
	private HostData[] pollResult = new HostData[0];
	private HostData[] Poll(){
		if(Event.current.type == EventType.Layout){
			pollResult = MasterServer.PollHostList();
			return pollResult;
		}else{
			return pollResult;
		}
	}
}
