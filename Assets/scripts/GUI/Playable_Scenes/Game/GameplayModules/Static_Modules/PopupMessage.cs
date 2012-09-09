using UnityEngine;
using System.Collections;

public static class PopupMessage{
	public static bool enable = true;
	public static Rect position = new Rect(Screen.width/2-90,Screen.height/2-100,180,50);
	
	public static float duration = 3f;
	
	private static float dispStopTime = 0f;
	private static string displayingString = "";
	
	public static void PrintGUI(){
		if(enable && Time.time < dispStopTime){
			GUI.Box(position,displayingString,"darkBoxCentered");
		}
	}
	
	public static void DisplayMessage(string str){
		DisplayMessage(str, duration);
	}
	
	public static void DisplayMessage(string str, float duration){
		displayingString = str;
		dispStopTime = Time.time+duration;
		
	}
	
}
