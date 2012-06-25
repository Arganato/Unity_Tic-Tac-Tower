using UnityEngine;
using System.Collections;

public class TutorialSkillCapButton : MenuButton {
	
	public TutorialSkillCapButton(){
	}
	
	public override void ButtonDown(){
		Tutorial.tutorialType = TowerType.skillCap;
		Tutorial.SetupTutorial();
		Application.LoadLevel("Tutorial");
	}
	
	public override string Name(){
		return "Power Tutorial";
	}
	
}