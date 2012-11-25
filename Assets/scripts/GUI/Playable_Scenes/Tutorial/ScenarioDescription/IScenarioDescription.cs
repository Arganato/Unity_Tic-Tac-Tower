using UnityEngine;
using System.Collections;

public interface IScenarioDescription {
	
	GameGUIOptions GetGUIOptions();
	
	TutorialCondition GetCondition();
	
	void Start();
	
	void OnContinue();
	
	void OnFinished();
}
