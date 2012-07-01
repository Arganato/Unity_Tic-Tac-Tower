using UnityEngine;
using System.Collections;

public class HeaderBar{

	public bool enable = true;
	public Rect position = new Rect(0,0,Screen.width,40);
	
	private DropdownMenu dropdownMenu;
	
	public HeaderBar(Control c, NetworkInterface nif){
		# if UNITY_WEBPLAYER
			dropdownMenu = DropdownMenu.Create(c,nif);
		# elif UNITY_ANDROID
			dropdownMenu = DropdownMenu.CreateAndroid(c,nif);
		# else
			dropdownMenu = DropdownMenu.Create(c,nif);
		# endif
	}
	
	public void PrintGUI(){
		HeaderText();
		dropdownMenu.PrintGUI();
	}
	
	private void HeaderText(){
		GUI.Box(position,"");
		GUI.Box(new Rect(0,0,position.width-80,position.height/2),"Welcome to Tic-Tac-Tower!"); 
		//this text could be swapped with player x vs player y, or something...
		GUI.Box(new Rect(0,position.height/2-2,position.width-80,position.height/2+2),"Player " + (Control.cState.activePlayer+1) + "'s turn. Turn "+(Control.cState.turn+1));
	}

}
