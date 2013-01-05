using UnityEngine;
using System.Collections;

public class TutorialSkillCondition : TutorialCondition{

	private IScenarioDescription receiver;
	private TutorialScene tutorialScene;
	
	public TutorialSkillCondition(IScenarioDescription receiver, TutorialScene scene){
		this.receiver = receiver;
		tutorialScene = scene;
	}
	
	public override void Calculate (ConditionEvent e){
		//called when there is an event
		if(CheckCondition(tutorialScene.tutorialStep,e)){
			receiver.OnContinue();
		}
	}

	private bool CheckCondition(int step, ConditionEvent e){
		//should return false when there is no condition to check
		switch(step){
		case 2:
			return e == TutorialCondition.ConditionEvent.endTurn;
		case 4: //detect that the shoot-help is open somehow
			return e == TutorialCondition.ConditionEvent.endTurn;
		case 8: //detect shoot-tower
			return (Control.cState.player[0].playerSkill.build >= 1);
		case 10:
			return (Control.cState.player[0].playerSkill.build == 0);
		case 14:
			return (Control.cState.player[0].playerSkill.silence >= 1);
		case 16:
			return e == TutorialCondition.ConditionEvent.endTurn;
		case 20:
			return e == TutorialCondition.ConditionEvent.endTurn;
		case 22:
			return e == TutorialCondition.ConditionEvent.endTurn;
		default:
			return false;
		}
	}
}
