using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkWindow : INetworkMessage{

	public bool enable;
	public Rect togglePos = new Rect(0,0,80,55);
	private Rect position = new Rect(100,100,200,300);
//	private NetworkInterface networkInterface;
	private string messages = "";
	
	private Chat chat;
	private List<MenuContent> menus = new List<MenuContent>();

	public NetworkWindow(IGUIMessages receiver){
		chat = new Chat(receiver);
		chat.togglePos = new Rect(0,30,togglePos.width,25);
		receiver.AddNetworkMessageRecipient((INetworkMessage)this);
		menus.Add(new MainContent(receiver,this));
	}
		
	public void ToggleGUI(){
		GUI.BeginGroup(togglePos);
		enable = GUI.Toggle(new Rect(0,0,togglePos.width,25),enable,"Network","button");
		chat.ToggleGUI();
		GUI.EndGroup();
	}
	
	public void WindowGUI(){
		if(enable){
			position = GUI.Window(2,position,TheWindow,"Network");
		}
			chat.PrintGUI();
	}
	
	private void TheWindow(int windowID){
		GUILayout.BeginArea(new Rect(0,0,position.width,position.width-30));
			menus[menus.Count-1].PrintGUI();
		GUILayout.EndArea();
		if(GUI.Button(new Rect(position.width-100,position.height-25,100,25),"Back")){
			Back();
		}
		GUI.DragWindow();
	}
	
	private void BackGUI(){
		if(menus.Count > 1){
			if(GUI.Button(new Rect(position.width-100,position.height-25,100,25),"Back")){
				Back();
			}
		}else{
			if(GUI.Button(new Rect(position.width-100,position.height-25,100,25),"Quit")){
				Back();
			}			
		}
	}
	
	public void Back(){
		if(menus.Count > 1){
			menus.RemoveAt(menus.Count-1);
		}else{
			enable = false;
		}
	}
	
	public void AddMenu(MenuContent m){
		menus.Add(m);
	}
	
	public void AddLogEntry(string str){
		messages += str+"\n";
	}
	
	//Network messages
	
	public void ChatMessage (string msg)
	{
		chat.AddEntry(msg);
	}
	
	public void ConnectionStatus (ConnectionMessage msg)
	{
		AddLogEntry("Network update: "+msg);
	}
	
	public void StartGameMessage (){}
	
}
