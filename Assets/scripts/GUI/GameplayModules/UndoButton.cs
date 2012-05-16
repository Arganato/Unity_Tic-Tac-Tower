using UnityEngine;
using System.Collections;

public class UndoButton{
	
	public bool enable = true;
	public Rect position = new Rect(5,300,100,50);
	
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
		if( enable ){
			GUI.BeginGroup(position);
			GUI.Box(new Rect(0,25,position.width,25),"P1: "+usedUndoCounter[0]+"  P2: "+usedUndoCounter[1]);
			if( CanUndo() ){
				if(GUI.Button(new Rect(0,0,position.width,25),"Undo")){
				usedUndoCounter[Control.cState.activePlayer]++;
				control.UndoTurn();
				}
			}else{
				GUI.Box(new Rect(0,0,position.width,25),"Undo's used:");
			}
			GUI.EndGroup();
		}
	}
	
	private bool CanUndo(){
		return (!Skill.skillsUsed.Empty() || control.playerDone) && Stats.gameRunning;
	}
}
