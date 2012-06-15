using UnityEngine;
using System.Collections;

public class TutorialSilenceButton : MenuButton {
	
	public TutorialSilenceButton(){
	}
	
	public override void ButtonDown(){
//		Application.LoadLevel("tutorialSilence");
	}
	
	public override string Name(){
		return "Silence Tutorial";
	}
}
