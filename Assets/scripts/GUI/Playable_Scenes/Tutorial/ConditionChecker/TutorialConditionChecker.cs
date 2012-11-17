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
		case 4: //etter første brikke er lagt ut (flash endTurn)
			return Control.cState.playerDone;
		case 5: //detect shoot-tower
			return (Control.cState.player[0].playerSkill.shoot >= 1);
		case 8: //use shoot-skill at (4,4)
			return Control.cState.field[4,4] == Route.destroyed;
		case 10: //detect that "?" is pressed somehow
			return false;
		case 11: //detect that the shoot-help is open somehow
			return false;			
		default:
			return false;
		}
	}
}
