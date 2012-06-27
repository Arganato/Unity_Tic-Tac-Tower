using UnityEngine;
using System.Collections;

public class TutorialButtonRow{

	public bool enable = true;
	public Rect position;
	private CheckSolutionButton checkSolution;
	private UndoButton undo;
	
	public TutorialButtonRow(Control c){
		checkSolution = new CheckSolutionButton(c);
		undo = new UndoButton(c);
		float width = 300f;
		position = new Rect(Screen.width/2-width/2,Screen.height-110,width,40);
	}
	
	public bool PrintGUI(){
		if(enable){
			if(Stats.playerController[Control.cState.activePlayer] != Stats.PlayerController.localPlayer){
				GUI.enabled = false;
			}
			GUI.BeginGroup(position);
			bool check = checkSolution.PrintGUI();
			undo.PrintGUI();
			GUI.EndGroup();
			GUI.enabled = true;
			return check;
		}
		return false;
	}
	
	
}
