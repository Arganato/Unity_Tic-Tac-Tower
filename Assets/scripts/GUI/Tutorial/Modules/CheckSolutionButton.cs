using UnityEngine;
using System.Collections;

public class CheckSolutionButton{

	public Rect position = new Rect(120,0,60,40);
	public bool enable = true;
	
	//private Control control;
	
	public CheckSolutionButton(Control c){
		//control = c;
	}
	
	public void PrintGUI(){
		if(enable){
			if(GUI.Button( position, "Check\nSolution")){
				//Run checkSolution();
			}
		}
	}
	
}
