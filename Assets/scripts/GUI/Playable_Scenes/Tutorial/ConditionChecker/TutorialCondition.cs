using UnityEngine;
using System.Collections;

public abstract class TutorialCondition{
	
	public abstract void Calculate();
	
	public static TutorialCondition Create(IScenarioDescription receiver, TutorialScene scene){
		return new TutorialBasicCondition(receiver, scene);
	}

}
