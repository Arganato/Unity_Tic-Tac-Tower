using UnityEngine;
using System.Collections;

public class NetworkGUI {

	public bool enable;
	public Rect togglePos = new Rect(0,0,80,55);
	private Rect position = new Rect(100,100,200,400);
	private NetworkInterface networkInterface;
	private string messages = "";
	
	private ConnectGUI connectGUI;
	private Chat chat;

	public NetworkGUI(NetworkInterface nif){
		connectGUI = new ConnectGUI(nif);
		chat = new Chat(nif);
		chat.togglePos = new Rect(0,30,togglePos.width,25);
		nif.RegisterGUI(this);
		networkInterface = nif;
	}
		
	public void ToggleGUI(){
		GUI.BeginGroup(togglePos);
		enable = GUI.Toggle(new Rect(0,0,togglePos.width,25),enable,"Network","button");
		chat.ToggleGUI();
		GUI.EndGroup();
	}
	
	public void WindowGUI(){
		if(enable){
			position = GUI.Window(2,position,NetworkWindow,"Network");
		}
			chat.PrintGUI();
	}
	
	private void NetworkWindow(int windowID){
		GUI.BeginGroup(new Rect(0,20,position.width,position.height-20));
		if(!(Network.isClient || Network.isServer)){
			connectGUI.PrintGUI();
		}else{
			GUI.Box(new Rect(0,0,position.width,20),"Connected! IP: "+Network.player.ipAddress);
			if(GUI.Button(new Rect(0,20,position.width,20),"Disconnect")){
				networkInterface.Disconnect();
			}
		}
		GUI.TextArea(new Rect(0,40,position.width,position.height-60),messages);
		GUI.EndGroup();
		GUI.DragWindow();
	}
	
	public void AddLogEntry(string str){
		messages += str+"\n";
	}
	
	public void AddChatMessage(string entry,string name){
		chat.AddEntry(entry,name);
	}
}
