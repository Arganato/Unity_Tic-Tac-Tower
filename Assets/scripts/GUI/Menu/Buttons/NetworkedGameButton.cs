using UnityEngine;
using System.Collections;

//Multiplayer menus
public class NetworkedButton : MenuButton {
	
	private MainMenu mainMenu;
	
	public NetworkedButton(MainMenu m){
		mainMenu = m;
	}
	
	public override void ButtonDown(){
		mainMenu.StartGame(false);
	}
	
	public override string Name(){
		return "New Online Game";
	}

}