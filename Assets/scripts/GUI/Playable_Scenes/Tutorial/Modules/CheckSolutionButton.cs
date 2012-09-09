using UnityEngine;
using System.Collections;

public class CheckSolutionButton{

	public Rect position = new Rect(30,0,60,40);
	public bool enable = true;
	
	public bool PrintGUI(){
		bool buttonDown = false;
		if(enable){
			if(Control.cState.playerDone && GUI.Button( position, "Check\nSolution")){
				buttonDown = true;
				if(SolutionChecker.CheckSolution(Tutorial.chapter, Tutorial.tutorialType)) Debug.Log ("Solution is correct!"); //Added for debugging.
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
		return buttonDown;
	}
	
}
