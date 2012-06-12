using UnityEngine;
using System.Collections;

public class ConfirmMenu{

	public bool enable = true;
	public Rect positionClosed = new Rect(60,0, 80, 25);
	public Rect positionOpen;
	public string name = "";
	
	private bool confirm = false;
	
	public ConfirmMenu(string buttonName){
		Init(buttonName);
	}
	private void Init(string buttonName){
		name = buttonName;
		if(positionClosed.x>=60){
			positionOpen = new Rect(positionClosed.x-60,positionClosed.y,60,50);
		}else{
			positionOpen = new Rect(positionClosed.x+positionClosed.width,positionClosed.y,60,50);
		}		
	}
	public ConfirmMenu(string buttonName, Rect pos){
		positionClosed = pos;
		Init (buttonName);
	}
	
	public ConfirmMenu(string buttonName, int x, int y){
		positionClosed.x = x;
		positionClosed.y = y;
		Init(buttonName);
	}
	
	public bool PrintGUI(){
		if(enable){
			bool b = Print();
			return b;
		}else{
			return false;
		}
	}
	
	private bool Print(){
		if( !confirm && GUI.Button(positionClosed, name)){
			confirm = true;
		}else if(confirm){
			GUI.Box(positionClosed, name);
			GUI.Box(positionOpen,"");
			GUI.BeginGroup(positionOpen);
			if(GUI.Button(new Rect(0, 0, 60, 25), "confirm")){
				confirm = false;
				return true;
			}else if(GUI.Button(new Rect(0,25, 60, 25), "cancel")){
				confirm = false;
			}
			GUI.EndGroup();
		}
		return false;
	}
}
