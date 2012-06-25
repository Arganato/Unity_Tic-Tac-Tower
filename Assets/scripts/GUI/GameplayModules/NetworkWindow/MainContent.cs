using UnityEngine;
using System.Collections;

public class MainContent : MenuContent{
	
	private LobbyWindow lobby;
	private TestWindow test;
	private NetworkWindow networkWindow;
	private NetworkInterface networkInterface;
	
	public MainContent(NetworkInterface nif, NetworkWindow networkWin){
		lobby = new LobbyWindow(nif,networkWin);
		test = new TestWindow();
		networkWindow = networkWin;
		networkInterface = nif;
	}

	public override void Close (){}

	public override void PrintGUI ()
	{
		GUILayout.Space(20);
		
		if(Network.peerType == NetworkPeerType.Disconnected){
			if(GUILayout.Button("Connect")){
				networkWindow.AddMenu(lobby);
			}
		}else{
			if(GUILayout.Button("Disconnect")){
				networkInterface.Disconnect();
			}
		}
		GUILayout.FlexibleSpace();
		if(GUILayout.Button("Test Connection")){
			networkWindow.AddMenu(test);
		}
		GUILayout.FlexibleSpace();
		

	}

}
