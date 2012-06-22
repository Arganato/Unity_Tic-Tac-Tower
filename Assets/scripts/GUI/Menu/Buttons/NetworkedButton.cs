using UnityEngine;
using System.Collections;

//Multiplayer menus
public class NetworkedButton : MenuButton {
	
	private MainMenu mainMenu;
	private NetworkGameGUI networkGUI;
	
	public NetworkedButton(MainMenu m){
		mainMenu = m;
		networkGUI = new NetworkGameGUI(mainMenu);
	}
	
	public override void ButtonDown(){
		mainMenu.AddMenu(networkGUI);
		Stats.networkEnabled = true;
	}
	
	public override string Name(){
		return "New Online Game";
	}

}