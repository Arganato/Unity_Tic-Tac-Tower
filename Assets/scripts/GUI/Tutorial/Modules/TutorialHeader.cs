using UnityEngine;
using System.Collections;

public class TutorialHeader{

	public bool enable = true;
	public Rect position = new Rect(0,0,Screen.width,40);
	
	private DropdownMenu dropdownMenu;
	
	public TutorialHeader(){
	}
	
	public void PrintGUI(TowerType tower){
		HeaderText(tower);
	}
	
	private void HeaderText(TowerType tower){
		string s = "";
		GUI.Box(position,"");
		switch(tower){
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
			s = "Skillcap";
			break;
		default:
			Debug.LogError("Tried to access invalid skill tutorial");
			break;
		}
		GUI.Box(new Rect(0,0,position.width-50,position.height/2),"Tutorial - " + s,"InvisBox");
	}
}
