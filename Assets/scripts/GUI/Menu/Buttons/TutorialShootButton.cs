using UnityEngine;
using System.Collections;

public class TutorialShootButton : MenuButton {
	
	public TutorialShootButton(){
	}
	
	public override void ButtonDown(){
		Tutorial.tutorialType = TowerType.shoot;
		Tutorial.StartTutorial();
		Application.LoadLevel("Tutorial");
	}
	
	public override string Name(){
		return "Shoot Tutorial";
	}
}
