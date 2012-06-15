using UnityEngine;
using System.Collections;

//New Game menus
public class LocalGameButton : MenuButton {
	
	private MainMenu mainMenu;
	
	public LocalGameButton(MainMenu m){
		mainMenu = m;
	}

	public override void ButtonDown(){
		mainMenu.StartGame(true);
	}
	
	public override string Name(){
		return "New Local Game";
	}
}