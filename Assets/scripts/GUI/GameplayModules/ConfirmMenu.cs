using UnityEngine;
using System.Collections;

public class ConfirmMenu{

	public bool enable = true;
	public Rect position = new Rect(Screen.width - 110, Screen.height - 280, 110, 65);
	public string name = "";
	
	private bool confirm = false;
	
	public ConfirmMenu(string buttonName){
		name = buttonName;
	}
	public ConfirmMenu(string buttonName, Rect pos){
		position.x = pos.x;
		position.y = pos.y;
		name = buttonName;
	}
	
	public ConfirmMenu(string buttonName, int x, int y){
		position.x = x;
		position.y = y;
		name = buttonName;
	}
	
	public bool PrintGUI(){
		if(enable){
			GUI.BeginGroup(position);
			bool b = Print();
			GUI.EndGroup();
			return b;
		}else{
			return false;
		}
	}
	
	private bool Print(){
		Rect buttonPos = new Rect(10,0, 100, 40);
		if( !confirm && GUI.Button(buttonPos, name)){
			confirm = true;
		}else if(confirm){
			GUI.Box(buttonPos, name);
			if(GUI.Button(new Rect(0, 40, 55, 25), "confirm")){
				confirm = false;
				return true;
			}else if(GUI.Button(new Rect(55,40, 55, 25), "cancel")){
				confirm = false;
			}
		}
		return false;
	}
}
