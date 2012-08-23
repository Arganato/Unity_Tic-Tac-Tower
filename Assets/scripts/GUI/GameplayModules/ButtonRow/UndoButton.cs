using UnityEngine;
using System.Collections;

public class UndoButton{
	
	public bool enable = true;
	public Rect position = new Rect(200,0,60,40);
	
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
			ColoredBox();
			if( CanUndo() ){
				if(GUI.Button(new Rect(0,0,position.width,position.height/2),"Undo")){
				usedUndoCounter[Control.cState.activePlayer]++;
				control.UndoTurn();
				}
			}else{
				GUI.Box(new Rect(0,0,position.width,position.height/2),"Undo");
			}
			GUI.EndGroup();
		}
	}
	private void ColoredBox(){
		Color old = GUI.contentColor;
		GUI.contentColor = Color.red;
		GUI.Box(new Rect(0,position.height/2,position.width/2,position.height/2),""+usedUndoCounter[0]);
		GUI.contentColor = Color.blue;
		GUI.Box(new Rect(position.width/2,position.height/2,position.width/2,position.height/2),""+usedUndoCounter[1]);
		GUI.contentColor = old;
	}
	
	private bool CanUndo(){
		return (!Control.cState.skillsUsed.Empty() || Control.cState.playerDone) && Stats.gameRunning;
	}
}
