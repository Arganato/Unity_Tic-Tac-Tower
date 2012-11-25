using UnityEngine;
using System.Collections;

public class TutorialShootButton : MenuButton {
	
	public TutorialShootButton(){
	}
	
	public override void ButtonDown(){
		Stats.SetDefaultSettings();
		Tutorial.tutorialType = 1;
		Application.LoadLevel("Tutorial");
	}
	
	public override string Name(){
		return "Start Tutorial";
	}
}
