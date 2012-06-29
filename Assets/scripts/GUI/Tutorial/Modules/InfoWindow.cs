using UnityEngine;
using System.Collections;

public class InfoWindow{
		
	public string textString;
	public Rect position = new Rect(0,0,300,360);
	
	private SkillDescription[] skillDescriptions = new SkillDescription[4];
	private int textureType;


	public InfoWindow(){
		skillDescriptions[0] = new SkillDescription(TowerType.shoot);
		skillDescriptions[0].position = new Rect(0,position.height/2,position.width,position.height/2);
		skillDescriptions[1] = new SkillDescription(TowerType.build);
		skillDescriptions[1].position = new Rect(0,position.height/2,position.width,position.height/2);
		skillDescriptions[2] = new SkillDescription(TowerType.silence);
		skillDescriptions[2].position = new Rect(0,position.height/2,position.width,position.height/2);
		skillDescriptions[3] = new SkillDescription(TowerType.skillCap);
		skillDescriptions[3].position = new Rect(0,position.height/2,position.width,position.height/2);
		
	}

	public void PrintTutorialText(){
		GUI.Box(position,"");
		GUI.BeginGroup(position);
		
		if(Tutorial.chapter == Tutorial.Chapter.textStr){
			GUI.Label(new Rect(10,5,position.width-20,position.height/2-10),TutorialAssets.GetTutorialMessage());
			skillDescriptions[(int)Tutorial.towerTut].PrintGUI();
		}else{
			GUI.Label(new Rect(10,5,position.width-20,position.height-10),TutorialAssets.GetTutorialMessage());
		}

		GUI.EndGroup();
	}
}
