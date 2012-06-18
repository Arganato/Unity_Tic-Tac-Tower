using UnityEngine;
using System.Collections;

public class ConnectGUI {
	
	public bool enable = true;
	public Rect position = new Rect(0,00,200,40);
	private string ipAdress = "192.168.8.2";
	private NetworkInterface networkIf;
	
	public ConnectGUI(NetworkInterface netIf){
		networkIf = netIf;
	}

	public void PrintGUI(){
		if(enable){
			NetworkGUI();
		}
	}
	
	private void NetworkGUI(){
		GUI.BeginGroup(position);
		GUI.Box(new Rect(0,0,100,20),"IP Adress:");
		ipAdress = GUI.TextField(new Rect(100,0,100,20),ipAdress);
		if(GUI.Button(new Rect(0,20,120,20),"Create Server")){
			networkIf.LaunchServer();
		}if(GUI.Button(new Rect(120,20,80,20),"Connect")){
//			BroadcastMessage("ConnectToServer",ipAdress);
			networkIf.ConnectToServer(ipAdress);
		}
		GUI.EndGroup();
	}
}
