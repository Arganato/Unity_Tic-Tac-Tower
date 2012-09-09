using UnityEngine;
using System.Collections;

public class MainContent : MenuContent{
	
	private LobbyWindow lobby;
	private TestWindow test;
	private NetworkWindow networkWindow;
	private IGUIMessages receiver;
	
	public MainContent(IGUIMessages receiver, NetworkWindow networkWin){
		lobby = new LobbyWindow(receiver,networkWin);
		test = new TestWindow();
		networkWindow = networkWin;
		this.receiver = receiver;
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
				receiver.Disconnect();
			}
		}
		GUILayout.FlexibleSpace();
		if(GUILayout.Button("Test Connection")){
			networkWindow.AddMenu(test);
		}
		GUILayout.FlexibleSpace();
		

	}

}
