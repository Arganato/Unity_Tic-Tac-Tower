using UnityEngine;
using System.Collections;

public class StagingAreaGUI {

	public Rect position = new Rect(0,0,300,480);
	
	public StagingAreaGUI(){
		if( Screen.width >= 300){
			position.x = Screen.width/2 -position.width/2;
		}else{
			position.x = 0; 
			position.width = Screen.width;
		}if(Screen.height >= 480){
			position.y = Screen.height/2 - position.height/2;
		}else{
			position.y = 0;
			position.height = Screen.height;
		}
	}
	
	public void PrintGUI(){
		GUILayout.BeginArea(position);
		GUILayout.Label("Player 1 vs Player 2");
		
		GUILayout.EndArea();
	}

}
