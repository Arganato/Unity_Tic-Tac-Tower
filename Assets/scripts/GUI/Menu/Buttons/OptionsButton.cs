using UnityEngine;
using System.Collections;

//Option menus
public class OptionsButton : MenuButton {
	
	private MainMenu mainMenu;
	private OptionsGUI optionsMenu;
	public OptionsButton(MainMenu m){
		mainMenu = m;
		optionsMenu = new OptionsGUI();
	}
	public override void ButtonDown(){
		mainMenu.AddMenu(optionsMenu);
	}
	
	public override string Name(){
		return "Options";
	}
}
