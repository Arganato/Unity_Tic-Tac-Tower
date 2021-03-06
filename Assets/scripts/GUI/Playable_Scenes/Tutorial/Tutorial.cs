using UnityEngine;
using System.Collections;

public static class Tutorial {

	public static int tutorialType = 1;
	public static GameState tutorialState;
	public static GameGUIOptions guiOptions;

	public static IScenarioDescription GetTutorialDescription(TutorialScene scene, Control control){
		switch(tutorialType){
		case 1:
			return new BasicTutorial(scene, control);
		case 2:
			return new SkillTutorial(scene, control);
		default:
			return new BasicTutorial(scene, control);
		}
	}
	
	
}
