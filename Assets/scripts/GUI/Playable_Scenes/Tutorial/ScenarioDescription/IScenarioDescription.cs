using UnityEngine;
using System.Collections;

public interface IScenarioDescription {
	
	GameGUIOptions GetGUIOptions();
	
	void Start();
	
	void OnContinue();
	
	void OnFinished();
}
