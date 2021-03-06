using UnityEngine;
using System.Collections;

public class Chat{

	public bool enable = false;
	public Rect position = new Rect(100,100,150,250);
	public Rect togglePos = new Rect(0,0,100,25);
	private string chatField = "";
	private string newMessage = "";
	private IGUIMessages receiver;
	
	public Chat(IGUIMessages receiver){
		this.receiver = receiver;
	}
	
	public void PrintGUI(){
		if(enable)
			position = GUI.Window(1,position,ChatWindow,"Chat");
	}
	
	public void ToggleGUI(){
		enable = GUI.Toggle(togglePos,enable,"Chat", "button");
	}
	
	private void ChatWindow(int windowID){
		GUI.TextArea(new Rect(0,20,position.width,position.height-40),chatField);
		newMessage = GUI.TextField(new Rect(0,position.height-20,position.width-40,20),newMessage);
		if(GUI.Button(new Rect(position.width-40,position.height-20,40,20),"Send")){
			AddString("Me: "+newMessage);
			receiver.SendChatMessage(newMessage);
			newMessage = "";
		}
		GUI.DragWindow();
	}
	
	private void AddString(string str){
		chatField += str + "\n";
	}
	
	
	public void AddEntry(string entry){
		AddString(entry);
	}
	
}
