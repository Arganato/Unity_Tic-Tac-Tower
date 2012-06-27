using UnityEngine;
using System.Collections;

public class TutorialHeader{

	public bool enable = true;
	public Rect position = new Rect(0,0,120,25);
	
	private DropdownMenu dropdownMenu;
	
	public TutorialHeader(Control c){
		dropdownMenu = new DropdownMenu(c,null);
	}
	
	public void PrintGUI(){
		HeaderText();
		dropdownMenu.PrintGUI();
	}
	
	private void HeaderText(){
		string s = "";
		GUI.Box(position,"");
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
		GUI.Box(new Rect((Screen.width-position.width)/2,0,position.width,position.height),"Tutorial - " + s,"InvisBox");
	}
}
