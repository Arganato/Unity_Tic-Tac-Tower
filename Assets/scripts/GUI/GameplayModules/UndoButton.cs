using UnityEngine;
using System.Collections;

public class UndoButton{
	
	public bool enable = true;
	public Rect position = new Rect(5,Screen.height-300,100,25);
	
	public int[] usedUndoCounter = new int[2];
	
	private Control control;
	
	
	public UndoButton(Control c){
		control = c;
	}
	
	public void ResetCounter(){
		usedUndoCounter[0] = 0;
		usedUndoCounter[1] = 0;
	}
	
	public void PrintGUI(){
		if( enable && CanUndo() && GUI.Button(position,"Undo")){
			usedUndoCounter[Control.cState.activePlayer]++;
			control.UndoTurn();
		}
	}
	
	private bool CanUndo(){
		return !Skill.skillsUsed.Empty() || control.playerDone;
	}
}
