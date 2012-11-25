using UnityEngine;
using System.Collections;

public class UserInputs : MonoBehaviour {
	private SceneTemplate gui;

	// Use this for initialization
	void Start () {
		gui = (SceneTemplate)FindObjectOfType(typeof(SceneTemplate));
	}
	
	void Update () {
		//keyboard input
		if(Stats.gameRunning){
			if(Input.GetButton("End Turn") && Control.cState.playerDone){
				gui.UserEndTurn();
			}
			if(Input.GetButton("shoot")){
				gui.UseSkill(1);
			}if(Input.GetButton("build")){
				gui.UseSkill(2);
			}if(Input.GetButton("emp")){
				gui.UseSkill(3);
			}
		}	
	}
}
