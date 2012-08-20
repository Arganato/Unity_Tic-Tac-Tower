using UnityEngine;
using System.Collections;

public class EndTurnButton{

	public Rect position = new Rect(40,0,60,40);
	public bool enable = true;
	
	private Control control;
	
	public EndTurnButton(Control c){
		control = c;
	}
	
	public void PrintGUI(){
		if(enable){
			if(Control.cState.playerDone && Stats.gameRunning && GUI.Button( position, "End\nTurn")){
				control.UserEndTurn();
			}else if(!Control.cState.playerDone){
				Color old = GUI.contentColor;
				if(Control.cState.activePlayer == 0)
					GUI.contentColor = Color.red;
				else
					GUI.contentColor = Color.blue;
				GUI.Box(position,"Player "+(Control.cState.activePlayer+1));
				GUI.contentColor=old;
			}
		}
	}
}
