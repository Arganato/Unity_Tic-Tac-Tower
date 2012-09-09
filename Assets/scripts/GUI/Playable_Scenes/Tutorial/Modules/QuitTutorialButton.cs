using UnityEngine;
using System.Collections;

public class QuitTutorialButton{

	public Rect position = new Rect(240,0,60,40);
	public bool enable = true;
	
	//private Control control;
	
	public QuitTutorialButton(Control c){
		//control = c;
	}
	
	public void PrintGUI(){
		if(enable){
			if(GUI.Button( position, "Quit")){
				Application.LoadLevel("mainMenu");
			}
		}
	}
	
}
