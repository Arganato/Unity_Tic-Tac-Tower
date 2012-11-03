using UnityEngine;
using System.Collections;

public class TutorialConditionChecker{

	private IScenarioDescription receiver;
	private TutorialScene tutorialScene;
	
	public TutorialConditionChecker(IScenarioDescription receiver, TutorialScene scene){
		this.receiver = receiver;
		tutorialScene = scene;
	}
	
	public void Calculate(){
		//called when there is an event
		if(CheckCondition(tutorialScene.tutorialStep)){
			receiver.OnContinue();
		}
	}
	
	private bool CheckCondition(int step){
		//should return false when there is no condition to check
		switch(step){
		case 1:
			//check something
			return true;
		case 2:
			return false;
		default:
			return false;
		}
	}
}
