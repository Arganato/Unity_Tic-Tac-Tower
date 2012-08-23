using UnityEngine;
using System.Collections;

//Option menus
public class OptionsButton : MenuButton {
	
	private MainMenu mainMenu;
	private OptionsMenu optionsMenu;
	public OptionsButton(MainMenu m){
		mainMenu = m;
		optionsMenu = OptionsMenu.Create(m);
	}
	public override void ButtonDown(){
		mainMenu.AddMenu(optionsMenu);
	}
	
	public override string Name(){
		return "Options";
	}
}
