using UnityEngine;
using System.Collections;

public class SkillDescription{
	
	private int width = 100;
	private int height = 150;
	
	public string descrString;
	public Rect position;
	//public Rect position = new Rect(0,0,Screen.width/2,150);

	private int textureSize = 75;
	private int textureType;


	public SkillDescription(TowerType tower){
		descrString = ResourceFactory.GetDescription(tower);
		position = new Rect((Screen.width-width)/2,(Screen.height-height)/2,(Screen.width+width)/2,(Screen.height+height)/2);
		switch(tower){
		case TowerType.shoot:
			textureType = 0;
			break;
		case TowerType.build:
			textureType = 1;
			break;
		case TowerType.silence:
			textureType = 2;
			break;
		case TowerType.skillCap:
			textureType = 3;
			break;
		default:
			Debug.LogWarning("TowerDescription not initialized with a valid tower");
			break;
		}
	}

	public void PrintGUI(){
		GUI.BeginGroup(position);
		GUI.Box(new Rect(0,0,position.width,position.height),"");
		GUI.Box(new Rect(0,0,textureSize,textureSize),ResourceFactory.GetSkillTexture(textureType));
		GUI.Box(new Rect(0,textureSize,textureSize,textureSize), ResourceFactory.GetSkillTexture(textureType+4));
		GUI.Box(new Rect(textureSize,0,position.width-textureSize,position.height), descrString,"invisBox");
		GUI.EndGroup();
	}
}
