using UnityEngine;
using System.Collections;

public class TutorialSkillCondition : TutorialCondition{

	private IScenarioDescription receiver;
	private TutorialScene tutorialScene;
	
	public TutorialSkillCondition(IScenarioDescription receiver, TutorialScene scene){
		this.receiver = receiver;
		tutorialScene = scene;
	}
	
	public override void Calculate (){
		//called when there is an event
		if(CheckCondition(tutorialScene.tutorialStep)){
			receiver.OnContinue();
		}
	}

	private bool CheckCondition(int step){
		//should return false when there is no condition to check
		switch(step){
		case 10: //detect that "?" is pressed somehow
			return false;
		case 11: //detect that the shoot-help is open somehow
			return false;			
		default:
			return false;
		}
	}
}
