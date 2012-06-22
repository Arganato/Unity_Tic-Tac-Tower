using UnityEngine;
using System.Collections;

//New Game menus
public class LocalGameButton : MenuButton {
	
	private MainMenu mainMenu;
	private StartGameScreen startGame = new StartGameScreen();
	
	public LocalGameButton(MainMenu m){
		mainMenu = m;
	}

	public override void ButtonDown(){
		mainMenu.AddMenu(startGame);
		Stats.networkEnabled = false;
	}
	
	public override string Name(){
		return "New Local Game";
	}
}