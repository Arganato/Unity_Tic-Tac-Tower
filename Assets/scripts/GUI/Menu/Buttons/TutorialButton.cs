using UnityEngine;
using System.Collections;

public class TutorialButton : MenuButton {
	
	private MainMenu mainMenu;
	private Frame tutorialFrame;
	
	public TutorialButton(MainMenu m){
		mainMenu = m;
		tutorialFrame = Frame.Create("Tutorial");
		tutorialFrame.AddButton(new TutorialShootButton());
		tutorialFrame.AddButton(new TutorialBuildButton());
		tutorialFrame.AddButton(new TutorialSilenceButton());
		tutorialFrame.AddButton(new TutorialSkillCapButton());
	}
	
	public override void ButtonDown ()
	{
		mainMenu.AddMenu(tutorialFrame);
		Stats.networkEnabled = false;
	}
	public override string Name ()
	{
		return "Tutorial";
	}
}
