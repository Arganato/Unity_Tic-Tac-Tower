using UnityEngine;
using System.Collections;

public class LobbyGUI {
	
	public enum Option {NoAction, Create, Join}
	
	public struct SelectedAction{
		public Option option;
		public int hostID;
		public string password;
		public SelectedAction(Option o){ option = o;hostID = -1;password = "";}
		public SelectedAction(Option o,int i){option = o; hostID = i;password = "";}
		public bool NoAction(){return option == LobbyGUI.Option.NoAction;}
	}
	
	public Rect position = new Rect(0,0,300,420);
	private int selectedGame = -1;
	private string password = "";
	
	public LobbyGUI(){
		if( Screen.width >= 300){
			position.x = Screen.width/2 -position.width/2;
		}else{
			position.x = 0; 
			position.width = Screen.width;
		}if(Screen.height >= 480){
			position.y = Screen.height/2+30 - position.height/2;
		}else{
			position.y = 0;
			position.height = Screen.height-60;
		}

	}
	
	//returns int: -1 for create game, x for join game x 
	public SelectedAction PrintGUI(){
		SelectedAction act = new SelectedAction(Option.NoAction);
		GUILayout.BeginArea(position);
			GUILayout.Space(20);
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

				if( GUILayout.Button("Join"))
					act = new SelectedAction(Option.Join,selectedGame);
					act.password = password;
				}else if(selectedGame < 0){
					GUILayout.Box("Join");
				}
				if(GUILayout.Button("Create"))
					act = new SelectedAction(Option.Create);
			GUILayout.EndHorizontal();	
		GUILayout.EndArea();
		return act;
	}
	
	private void LobbyList(){
		GUILayout.BeginHorizontal();
		GUILayout.Box("Game name");
		GUILayout.Box("Password", GUILayout.Width(70));
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
				GUILayout.Box(pollList[i].passwordProtected ? "yes" : "no", GUILayout.Width(70));
				GUILayout.EndHorizontal();
			}
		}
	}
	
	//neccesary to avoid the poll-list to change mid-frame
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
