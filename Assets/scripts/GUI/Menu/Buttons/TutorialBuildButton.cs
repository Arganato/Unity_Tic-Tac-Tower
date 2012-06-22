using UnityEngine;
using System.Collections;

public class TutorialBuildButton : MenuButton {

	public TutorialBuildButton(){
	}
	
	public override void ButtonDown(){
		Tutorial.tutorialType = TowerType.build;
		Tutorial.SetupTutorial();
		Application.LoadLevel("Tutorial");
	}
	
	public override string Name(){
		return "Build Tutorial";
	}
}
