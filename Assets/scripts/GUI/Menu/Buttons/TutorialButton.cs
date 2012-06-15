using UnityEngine;
using System.Collections;

public class TutorialButton : MenuButton {
	
	private MainMenu mainMenu;
	
	public TutorialButton(MainMenu m){
		mainMenu = m;
	}
	
	public override void ButtonDown ()
	{
		mainMenu.TutorialMenu();
	}
	public override string Name ()
	{
		return "Tutorial";
	}
}
