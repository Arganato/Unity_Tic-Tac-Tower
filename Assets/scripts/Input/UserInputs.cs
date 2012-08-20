using UnityEngine;
using System.Collections;

public class UserInputs : MonoBehaviour {
	private Control control;

	// Use this for initialization
	void Start () {
		control = (Control)FindObjectOfType(typeof(Control));
	
	}
	
	void Update () {
		//keyboard input
		if(Stats.gameRunning){
			if(Input.GetButton("End Turn") && Control.cState.playerDone){
				control.UserEndTurn();
			}
			if(Input.GetButton("shoot")){
				Skill.UseSkill(1);
			}if(Input.GetButton("build")){
				Skill.UseSkill(2);
			}if(Input.GetButton("emp")){
				Skill.UseSkill(3);
			}
		}	
	}
}
