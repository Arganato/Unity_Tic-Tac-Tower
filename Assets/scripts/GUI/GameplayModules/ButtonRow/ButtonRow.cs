using UnityEngine;
using System.Collections;

public class ButtonRow{
	
	public bool enable = true;
	public Rect position;
	private EndTurnButton endturn;
	private UndoButton undo;
	private ClockGUI p1Clock = new ClockGUI(0);
	private ClockGUI p2Clock = new ClockGUI(1);
	
	public ButtonRow(Control c){
		endturn = new EndTurnButton(c);
		undo = new UndoButton(c);
		float width = 300f;
		position = new Rect(Screen.width/2-width/2,Screen.height-110,width,40);
	}
	
	public void PrintGUI(){
		if(enable){
			if(Stats.playerController[Control.cState.activePlayer] != Stats.PlayerController.localPlayer){
				GUI.enabled = false;
			}
			GUI.BeginGroup(position);
			endturn.PrintGUI();
			undo.PrintGUI();
			p1Clock.PrintGUI();
			p2Clock.PrintGUI();
			GUI.EndGroup();
			GUI.enabled = true;
		}
	}
	
	
}