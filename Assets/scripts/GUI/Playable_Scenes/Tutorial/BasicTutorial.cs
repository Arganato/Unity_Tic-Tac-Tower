using UnityEngine;
using System.Collections;

public class BasicTutorial : IScenarioDescription{
	
	private ScenarioDescriptionGUI tutorialWindow;
	private int tutorialProgress = 0;
	
	public BasicTutorial(){
	}
	
	public void SetGUI(ScenarioDescriptionGUI guiwindow){
		tutorialWindow = guiwindow;
	}
	
	public void Start(){
		Debug.Log("starting tutorialWindow");
		PropagateTutorial(tutorialProgress);		
	}
	
	public void OnContinue ()
	{
		Debug.Log("Continue called");
		PropagateTutorial(tutorialProgress);
	}
	
	public void OnFinished ()
	{
		throw new System.NotImplementedException ();
	}
	
//	IEnumerable<int> PropagateTutorial(){
//		tutorialWindow.AddMission("This is a mission","This is a mission that should be carried out like this");
//		tutorialWindow.ShowContinue(true);
//		tutorialWindow.ShowFinish(false);
////		yield break;
//		yield return 0;
//		tutorialWindow.AddNote("This is a note");
//	}
	
	public void PropagateTutorial(int step){
		switch(step){
		case 0:
			tutorialWindow.AddMission("This is a mission","This is a mission that should be carried out like this");
			tutorialWindow.ShowContinue(true);
			tutorialWindow.ShowFinish(false);
			break;
		case 1:
			tutorialWindow.AddNote("This is a note");
			break;
		case 2:
			tutorialWindow.AddNote("This is another note");
			break;
		case 3:
			Debug.Log("Tutorial is finished");
			break;
		}
		
		tutorialProgress++;
	}	
}
