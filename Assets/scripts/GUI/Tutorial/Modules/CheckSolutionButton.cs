using UnityEngine;
using System.Collections;

public class CheckSolutionButton{

	public Rect position = new Rect(120,0,60,40);
	public bool enable = true;
	
	private Control control;
	
	public CheckSolutionButton(Control c){
		control = c;
	}
	
	public bool PrintGUI(){
		bool buttonDown = false;
		if(enable){
			if(control.playerDone && GUI.Button( position, "Check\nSolution")){
				buttonDown = true;
			}else if(!control.playerDone){
				Color old = GUI.contentColor;
				if(Control.cState.activePlayer == 0)
					GUI.contentColor = Color.red;
				else
					GUI.contentColor = Color.blue;
				GUI.Box(position,"Player "+(Control.cState.activePlayer+1));
				GUI.contentColor=old;
			}
		}
		return buttonDown;
	}
	
}
