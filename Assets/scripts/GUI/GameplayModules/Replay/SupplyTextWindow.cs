using UnityEngine;
using System.Collections;

public class SupplyTextWindow{
	
	public bool enable = true;
	private string text = "";
	
	public bool PrintGUI(){
		if (enable){
			GUI.BeginGroup(new Rect(Screen.width/2-200,Screen.height/2-100,400,200));
			GUI.Box(new Rect(0,0,400,200),"Copy a match replay to the window below","darkBoxCentered");
			//GUI.Box(new Rect(0,0,400,22),);
			text = GUI.TextArea(new Rect(0,20,400,160),text);
			bool ret = GUI.Button(new Rect(300,180,80,20),"OK");	
			GUI.EndGroup();
			return ret;
		}
		return false;
	}
	
	public string GetText(){
		return text;
	}
}
