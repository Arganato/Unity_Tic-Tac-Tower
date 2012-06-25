using UnityEngine;
using System.Collections;

public class TutorialButtonRow{

	public bool enable = true;
	public Rect position;
	private CheckSolutionButton checkSolution;
	
	public TutorialButtonRow(Control c){
		checkSolution = new CheckSolutionButton(c);
		float width = 300f;
		position = new Rect(Screen.width/2-width/2,Screen.height-110,width,40);
	}
	
	public void PrintGUI(){
		if(enable){
			if(Stats.playerController[Control.cState.activePlayer] != Stats.PlayerController.localPlayer){
				GUI.enabled = false;
			}
			GUI.BeginGroup(position);
			checkSolution.PrintGUI();
			GUI.EndGroup();
			GUI.enabled = true;
		}
	}
	
	
}
