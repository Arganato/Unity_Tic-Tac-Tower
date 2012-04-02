using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
//@MenuItem("Example/Save Scene while on play mode")
	void OnGUI () { 	
	
		if(GUI.Button(new Rect(Screen.width/2-100, Screen.height/2-100, 200, 50), "Start simple Game")){
			Stats.SetDefaultSettings();
			Stats.skillEnabled.SetDiag(false);
			Application.LoadLevel("game");
		}
		if(GUI.Button(new Rect(Screen.width/2-100, Screen.height/2+100, 200, 50), "Start full Game")){
			Stats.SetDefaultSettings();
			Application.LoadLevel("game");
		}
	}
}
