using UnityEngine;
using System.Collections;

public class ConnectGUI : MenuContent {
	
	public bool enable = true;
	public Rect position = new Rect(0,00,200,60);
	private string ipAdress = "192.168.0.0";
	private NetworkInterface networkIf;
	
	private bool connectionStarted = false;
	private bool useNat = false;
	
	public ConnectGUI(NetworkInterface netIf){
		networkIf = netIf;
	}
	public override void Close (){}


	public override void PrintGUI(){
		if(enable){
			if(Network.isClient || Network.isServer)
				Connected();
			else
				NotConnected();
		}
	}
	
	private void NotConnected(){
		bool tmpNat = useNat;
		useNat = GUILayout.Toggle(useNat,"Use NAT");
		if(tmpNat != useNat && tmpNat){
			ipAdress = "192.168.0.0";
		}else if(tmpNat != useNat){
			ipAdress = "";
		}
		if(useNat){
			GUILayout.Label("GUID:");
		}else{
			GUILayout.Label("IP Adress:"); //new Rect(0,0,100,20),
		}
		ipAdress = GUILayout.TextField(ipAdress); //new Rect(100,0,100,20),
		GUILayout.BeginHorizontal();
		if(connectionStarted){
			GUILayout.Box("Connecting...");
			if(GUILayout.Button("Cancel"))
				connectionStarted = false;
		}else{
			if(GUILayout.Button("Create Server")){ //new Rect(0,20,120,20),
				networkIf.LaunchServer(useNat);
				connectionStarted = true;
			}if(GUILayout.Button("Connect")){ //new Rect(120,20,80,20),
				networkIf.ConnectToServer(ipAdress,useNat);
				connectionStarted = true;
			}
		}
		GUILayout.EndHorizontal();
	}
	
	private void Connected(){
		if(useNat)
			GUILayout.Label("Connected! GUID: "+Network.player.guid);
		else
			GUILayout.Label("Connected! IP: "+Network.player.ipAddress); //new Rect(0,0,position.width,20),
		if(GUILayout.Button("Disconnect")){ //new Rect(0,20,position.width,20),
			networkIf.Disconnect();
			connectionStarted = false;
		}

	}
	
	
}
