using UnityEngine;
using System.Collections;

public class TutorialSilenceButton : MenuButton {
	
	public TutorialSilenceButton(){
	}
	
	public override void ButtonDown(){
		Tutorial.tutorialType = TowerType.silence;
		Tutorial.SetupTutorial();
		Application.LoadLevel("Tutorial");
	}
	
	public override string Name(){
		return "Silence Tutorial";
	}
}
