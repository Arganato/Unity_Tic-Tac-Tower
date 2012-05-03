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
		if(Input.GetButton("End Turn") && control.playerDone){
			control.ChangeCurrPlayer();
		}
		if(Input.GetButton("shoot")){
			control.UseSkill(1);
		}if(Input.GetButton("build")){
			control.UseSkill(2);
		}if(Input.GetButton("emp")){
			control.UseSkill(3);
		}
	
	}
}
