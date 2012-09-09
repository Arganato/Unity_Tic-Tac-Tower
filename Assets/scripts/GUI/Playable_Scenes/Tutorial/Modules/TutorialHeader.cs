using UnityEngine;
using System.Collections;

public class TutorialHeader{

	public bool enable = true;
	
	private DropdownMenu dropdownMenu;
	
	public TutorialHeader(IGUIMessages receiver){
		# if UNITY_WEBPLAYER
			dropdownMenu = DropdownMenu.Create(receiver,false);
		# elif UNITY_ANDROID
			dropdownMenu = DropdownMenu.CreateAndroid(receiver,false);
		# else
			dropdownMenu = DropdownMenu.Create(receiver,false);
		# endif
	}
	
	public void PrintGUI(){
		HeaderText();
		dropdownMenu.PrintGUI();
	}
	
	private void HeaderText(){
		string s = "";
		switch(Tutorial.towerTut){
		case TowerType.build:
			s = "Build";
			break;
		case TowerType.shoot:
			s = "Shoot";
			break;
		case TowerType.silence:
			s = "Silence";
			break;
		case TowerType.skillCap:
			s = "Power";
			break;
		default:
			Debug.LogError("Tried to access invalid skill tutorial");
			break;
		}
		GUI.Label(new Rect((Screen.width-125)/2,0,125,25),"Tutorial - " + s);
	}
}
