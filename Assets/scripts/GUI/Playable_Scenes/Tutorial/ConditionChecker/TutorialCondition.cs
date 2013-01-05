using UnityEngine;
using System.Collections;

public abstract class TutorialCondition{
	
	public enum ConditionEvent {endTurn, placedPiece};
	
	public abstract void Calculate(ConditionEvent e);
	
	public static TutorialCondition Create(IScenarioDescription receiver, TutorialScene scene){
		return new TutorialBasicCondition(receiver, scene);
	}

}
