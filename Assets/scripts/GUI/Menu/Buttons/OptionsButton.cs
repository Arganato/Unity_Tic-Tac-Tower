using UnityEngine;
using System.Collections;

//Option menus
public class OptionsButton : MenuButton {
	
	private MainMenu mainMenu;
	public OptionsButton(MainMenu m){
		mainMenu = m;
	}
	public override void ButtonDown(){
		mainMenu.OptionsMenu();
	}
	
	public override string Name(){
		return "Options";
	}
}
