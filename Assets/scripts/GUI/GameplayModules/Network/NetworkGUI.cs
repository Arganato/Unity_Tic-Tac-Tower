using UnityEngine;
using System.Collections;

public class NetworkGUI {

	public bool enable;
	public Rect togglePos = new Rect(0,0,80,55);
	private Rect position = new Rect(100,100,200,400);
//	private NetworkInterface networkInterface;
	private string messages = "";
	private bool showTest = false;
	
	private ConnectGUI connectGUI;
	private Chat chat;
	private TestWindow testWindow = new TestWindow();

	public NetworkGUI(NetworkInterface nif){
		connectGUI = new ConnectGUI(nif);
		chat = new Chat(nif);
		chat.togglePos = new Rect(0,30,togglePos.width,25);
		nif.RegisterGUI(this);
//		networkInterface = nif;
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
		if(showTest){
			TestWindow();
		}else{
			NormalWindow();
		}
		GUI.DragWindow();
	}
		
	private void NormalWindow(){
		if(GUI.Button(new Rect(2,20,position.width*0.6f,20),"Test Connection")){
			showTest = true;
		}
		GUILayout.BeginArea(new Rect(2,40,position.width-4,position.height-40));
		connectGUI.PrintGUI();
		
		GUILayout.TextArea(messages);
		GUILayout.EndArea();
		
	}	
	
	private void TestWindow(){
		if(GUI.Button(new Rect(position.width*0.6f,20,position.width*0.4f,20),"Back")){
			showTest = false;
		}
		GUI.BeginGroup(new Rect(0,40,position.width,position.height-40));
		testWindow.PrintGUI();
		GUI.EndGroup();
	}
	
	public void AddLogEntry(string str){
		messages += str+"\n";
	}
	
	public void AddChatMessage(string entry,string name){
		chat.AddEntry(entry,name);
	}
}
