using UnityEngine;
using System.Collections;

public class NetworkGameGUI : MenuContent{
	
	public Rect position = new Rect(0,0,Screen.width,Screen.height-40);
	
	private MainMenu mainMenu; //callback-link to main menu
	private NetworkInterface networkInterface;
	private LobbyGUI lobbyGUI = new LobbyGUI();
	
	public NetworkGameGUI(MainMenu m){
		mainMenu = m;
		networkInterface = mainMenu.FindNetworkInterface();
	}
	
	public override void PrintGUI (){
		LobbyGUI.SelectedAction action = lobbyGUI.PrintGUI();
		if(!action.NoAction()){
			if(action.option == LobbyGUI.Option.Create){
				CreateGame();
			}else if(action.option == LobbyGUI.Option.Join){
				JoinGame(action.hostID);
			}
		}
	}
	
	private void JoinGame(int gameNumber){
		HostData[] hostArray = MasterServer.PollHostList();
		HostData game = hostArray[gameNumber];
		networkInterface.ConnectToServer(game);
		StartGameScreen startGame = new StartGameScreen(networkInterface,true);
		mainMenu.AddMenu(startGame);
	}
	
	private void CreateGame(){
		networkInterface.LaunchServer(Network.HavePublicAddress(),true);
		StartGameScreen startGame = new StartGameScreen(networkInterface,false);
		mainMenu.AddMenu(startGame);
	}
	
}
