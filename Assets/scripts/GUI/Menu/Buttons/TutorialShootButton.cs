using UnityEngine;
using System.Collections;

public class TutorialShootButton : MenuButton {
	
	public TutorialShootButton(){
	}
	public override void ButtonDown(){
//		Application.LoadLevel("tutorialShoot");
	}
	
	public override string Name(){
		return "Shoot Tutorial";
	}
}
