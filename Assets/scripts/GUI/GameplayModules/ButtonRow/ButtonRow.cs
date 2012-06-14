using UnityEngine;
using System.Collections;

public class ButtonRow{
	
	public bool enable = true;
	public Rect position = new Rect(0,Screen.height-110,Screen.width,40);
	private EndTurnButton endturn;
	private UndoButton undo;
	private ClockGUI p1Clock = new ClockGUI(0);
	private ClockGUI p2Clock = new ClockGUI(1);
	
	public ButtonRow(Control c){
		endturn = new EndTurnButton(c);
		undo = new UndoButton(c);
	}
	
	public void PrintGUI(){
		if(enable){
			GUI.BeginGroup(position);
			endturn.PrintGUI();
			undo.PrintGUI();
			p1Clock.PrintGUI();
			p2Clock.PrintGUI();
			GUI.EndGroup();
		}
	}
	
	
}