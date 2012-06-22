using UnityEngine;
using System.Collections;

public class LobbyGUI {
	
	public enum Option {NoAction, Create, Join}
	
	public struct SelectedAction{
		public Option option;
		public int hostID;
		public SelectedAction(Option o){ option = o;hostID = -1;}
		public SelectedAction(Option o,int i){option = o; hostID = i;}
		public bool NoAction(){return option == LobbyGUI.Option.NoAction;}
	}
	
	public Rect position = new Rect(0,0,300,480);
	private int selectedGame = -1;
	
	//returns int: -1 for create game, x for join game x 
	public SelectedAction PrintGUI(){
		SelectedAction act = new SelectedAction(Option.NoAction);
		GUILayout.BeginArea(position);
			GUILayout.BeginHorizontal();
				GUILayout.Label("Lobby");
				if(GUILayout.Button("Refresh")){
					MasterServer.ClearHostList();
					MasterServer.RequestHostList(Stats.uniqueGameID);
				}
			GUILayout.EndHorizontal();
			LobbyList();
			GUILayout.BeginHorizontal();
				if(GUILayout.Button("Join"))
					act = new SelectedAction(Option.Join,selectedGame);
				if(GUILayout.Button("Create"))
					act = new SelectedAction(Option.Create);
			GUILayout.EndHorizontal();	
		GUILayout.EndArea();
		return act;
	}
	
	private void LobbyList(){
		if(Poll().Length == 0){
			GUILayout.Label("No games found..."); 
		}else{
			HostData[] pollList = Poll();
			for(int i=0;i<pollList.Length;i++){
				if(selectedGame == i){
					GUILayout.Box(pollList[i].gameName);
				}else{
					if(GUILayout.Button(pollList[i].gameName))
						selectedGame = i;
				}
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
