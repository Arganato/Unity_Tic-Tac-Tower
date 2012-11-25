using UnityEngine;
using System.Collections;

public class TutorialSkillButton : MenuButton {

	public TutorialSkillButton(){
	}
	
	public override void ButtonDown(){
		Stats.SetDefaultSettings();
		Tutorial.tutorialType = 2;
		Application.LoadLevel("Tutorial");
	}
	
	public override string Name(){
		return "Skill Tutorial";
	}
}
