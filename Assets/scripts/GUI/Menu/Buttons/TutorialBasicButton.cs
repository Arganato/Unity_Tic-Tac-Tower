using UnityEngine;
using System.Collections;

public class TutorialBasicButton : MenuButton {
	
	public TutorialBasicButton(){
	}
	
	public override void ButtonDown(){
		Stats.SetDefaultSettings();
		Tutorial.tutorialType = 1;
		Application.LoadLevel("Tutorial");
	}
	
	public override string Name(){
		return "Basic Tutorial";
	}
}
