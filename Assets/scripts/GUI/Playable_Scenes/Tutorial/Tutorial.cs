using UnityEngine;
using System.Collections;

public static class Tutorial { //Outdated??

								 // Str = Straight
	public enum Chapter {intro, textStr, tutStr, textDiag, tutDiag, end};

	public static Chapter chapter;
	public static GameState tutorialState;
	public static GameGUIOptions guiOptions;

	public static TowerType tutorialType;//Used to know which tutorial is run. TowerType.five is used when none is run.
	
	public static void StartTutorial(){
		Stats.startState = new GameState();
		chapter = Chapter.intro;
		SetTutorial();
	}

	public static void SetTutorialPower2(){
		TutorialAssets.SetTutorialPower2(Stats.startState);

	}
	public static void SetTutorial(){
		Stats.startState = new GameState();
		SkillEnabled guiButtonsEnabled = new SkillEnabled();
		guiButtonsEnabled.SetAll(false);
		switch(tutorialType){
		case TowerType.build:
			guiButtonsEnabled.build = true;
			guiButtonsEnabled.diagBuild = true;
			guiOptions = GameGUIOptions.Create(guiButtonsEnabled,false);
			if(chapter == Chapter.tutStr) TutorialAssets.SetTutorialBuild1(Stats.startState);
			else if(chapter == Chapter.tutDiag) TutorialAssets.SetTutorialBuild2(Stats.startState);
			break;
		case TowerType.shoot:
			guiButtonsEnabled.shoot = true;
			guiButtonsEnabled.diagShoot = true;
			if(chapter == Chapter.tutStr) TutorialAssets.SetTutorialShoot1(Stats.startState);
			else if(chapter == Chapter.tutDiag) TutorialAssets.SetTutorialShoot2(Stats.startState);
			break;
		case TowerType.silence:
			guiButtonsEnabled.build = true;
			guiButtonsEnabled.diagBuild = true;
			guiButtonsEnabled.silence = true;
			guiButtonsEnabled.diagSilence = true;
			if(chapter == Chapter.tutStr) TutorialAssets.SetTutorialSilence1(Stats.startState);
			else if(chapter == Chapter.tutDiag) TutorialAssets.SetTutorialSilence2(Stats.startState);
			break;	
		case TowerType.skillCap:
			guiButtonsEnabled.SetAll(true);
			if(chapter == Chapter.tutStr) TutorialAssets.SetTutorialPower1(Stats.startState);
			else if(chapter == Chapter.tutDiag) TutorialAssets.SetTutorialPower2(Stats.startState);
			break;
		}
		guiOptions.skillsEnabled = guiButtonsEnabled;
		guiOptions.makeNetworkGUI = false;
		Debug.Log("SetTutorial: stats.skillsEnabled set to: "+guiOptions.skillsEnabled.ToString());
	}

	public static bool CheckSolution(){
		if(SolutionChecker.CheckSolution(chapter,tutorialType)){
//			solutionAccepted = true;
			return true;
		}else{
			PopupMessage.DisplayMessage("Wrong solution!\nUndo to try again");
			return false;
		}
	}
	
	
}
