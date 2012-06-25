using UnityEngine;
using System.Collections;

public class NetworkGameGUI : MenuContent, INetworkMessage{
	
	public Rect position = new Rect(0,0,Screen.width,Screen.height-40);
	
	private MainMenu mainMenu; //callback-link to main menu
	private NetworkInterface networkInterface;
	private LobbyGUI lobbyGUI = new LobbyGUI();
	private bool joiningGame = false;
	private bool failedToJoin = false;
	
	public NetworkGameGUI(MainMenu m){
		mainMenu = m;
		networkInterface = mainMenu.FindNetworkInterface();
		networkInterface.AddMessageRecipient((INetworkMessage) this);
	}
	
	public override void PrintGUI (){
		if(joiningGame){
			GUI.Box(new Rect(position.width/2-150,position.height/2-25,300,50),"Joining Game...","darkBoxCentered");
			if(GUI.Button(new Rect(position.width-100,position.height-25,100,25), "Abort")){
				joiningGame = false;
				failedToJoin = true;
			}
		}else{
			LobbyGUI.SelectedAction action = lobbyGUI.PrintGUI();
			if(failedToJoin){
				GUI.Box(new Rect(position.width-200,0,200,25),"Connection failed");
			}
			if(!action.NoAction()){
				if(action.option == LobbyGUI.Option.Create){
					CreateGame();
				}else if(action.option == LobbyGUI.Option.Join){
					JoinGame(action.hostID, action.password);
				}
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
		StartGameScreen startGame = new StartGameScreen(networkInterface,true);
		mainMenu.AddMenu(startGame);
	}
	
	private void CreateGame(){
		CreateGameGUI createGame = new CreateGameGUI(networkInterface, mainMenu);
		mainMenu.AddMenu(createGame);
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
}
