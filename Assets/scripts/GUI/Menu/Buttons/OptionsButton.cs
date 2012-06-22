using UnityEngine;
using System.Collections;

//Option menus
public class OptionsButton : MenuButton {
	
	private MainMenu mainMenu;
	private Frame optionsMenu;
	public OptionsButton(MainMenu m){
		mainMenu = m;
		optionsMenu = Frame.Create("Options");
		//TODO: Add buttons to the menu!
	}
	public override void ButtonDown(){
		mainMenu.AddMenu(optionsMenu);
	}
	
	public override string Name(){
		return "Options";
	}
}
