using UnityEngine;
using System.Collections;

public class TutorialBuildButton : MenuButton {
	
	public TutorialBuildButton(){
	}
	
	public override void ButtonDown(){
//		Application.LoadLevel("tutorialBuild");
	}
	
	public override string Name(){
		return "Build Tutorial";
	}
}
