using UnityEngine;
using System.Collections;

public class TutorialButton : MenuButton {
	
	private MainMenu mainMenu;
	private Frame tutorialFrame;
	
	public TutorialButton(MainMenu m){
		mainMenu = m;
		tutorialFrame = Frame.Create("Tutorial");
		tutorialFrame.AddButton(new TutorialBasicButton());
		tutorialFrame.AddButton(new TutorialSkillButton());
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
