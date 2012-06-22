using UnityEngine;
using System.Collections;

public class StagingAreaGUI {

	public Rect position = new Rect(0,0,300,480);
	
	public void PrintGUI(){
		GUILayout.BeginArea(position);
		GUILayout.Label("Player 1 vs Player 2");
		
		GUILayout.EndArea();
	}

}
