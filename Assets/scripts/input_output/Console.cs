using UnityEngine;
using System.Collections;

public class Console{
	
	public enum MessageType{ TURN, ORDER, ERROR, INFO};
	
	public string diplayedText = "";
	public string editableText = "";
	public int windowHeight = 6;
	public Vector2 windowScrollPos = Vector2.zero;
	public bool show = false;
	
	private Rect windowRect = new Rect(20,Screen.height/2,400,150);
	private Control control;
	
	public Console(Control c){
		control = c;
	}
	
	public void PrintToConsole(string s, MessageType m){
		string code = "";
		switch(m){
		case MessageType.TURN:
			code = "-t ";
			break;
		case MessageType.ORDER:
			code = "-o ";
			break;
		case MessageType.ERROR:
			code = "-e ";
			break;
		case MessageType.INFO:
			code = "-i ";
			break;
		default:
			code = "-i ";
			break;
		}
		windowHeight+=15;
		windowScrollPos.y +=15;
		diplayedText += (code+s+"\n");
	}
	
	public void PrintGUI(){
		if(show){
			windowRect = GUI.Window(0,windowRect,TheWindow,"Console");
		}
		
	}
	
	private void TheWindow(int windowID){
		windowScrollPos = GUI.BeginScrollView(new Rect(2, 15, windowRect.width-2, windowRect.height-35), windowScrollPos, new Rect(0, 0, windowRect.width-20,windowHeight));
		GUI.TextArea(new Rect(0,0,windowRect.width,windowHeight),diplayedText);
		GUI.EndScrollView();
		editableText = GUI.TextField(new Rect(2,windowRect.height-20,windowRect.width-52,20),editableText);
	
		if(GUI.Button(new Rect(windowRect.width-50,windowRect.height-20,50,20),"Send")){
//			Debug.Log("string recieved: "+editableText);
			HandleString(editableText);
		}
		GUI.DragWindow(new Rect(0,0,windowRect.width,15));
	}
	
	private void HandleString(string s){
		if(s.StartsWith("-")){
			int endTypeCode = s.IndexOf(' ');
			string code = s.Substring(1,endTypeCode-1);
			string theRest = s.Substring(endTypeCode+1);
//			Debug.Log("code: "+code+". The rest: "+theRest);
			switch(code){
			case "t":
				control.ExecuteTurn(Turn.StringToTurn(theRest));
				break;
			case "o":
				control.ExecuteOrder(Order.StringToOrder(theRest));
				break;
			default: 
				break;
			}
		}
	}
}
