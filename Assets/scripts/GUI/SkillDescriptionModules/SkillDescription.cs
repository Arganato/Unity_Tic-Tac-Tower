using UnityEngine;
using System.Collections;

public class SkillDescription{

	public string descrString;
	public Rect position = new Rect(0,0,Screen.width/2,150);

	private int textureSize = 75;
	private int textureType;
	


	public SkillDescription(TowerType tower){
		descrString = ResourceFactory.GetDescription(tower);
		switch(tower){
		case TowerType.shoot:
			textureType = 0;
			break;
		case TowerType.build:
			textureType = 1;
			position.x = position.width;
			break;
		case TowerType.silence:
			textureType = 2;
			position.y = position.height;
			break;
		case TowerType.skillCap:
			textureType = 3;
			position.x = position.width;
			position.y = position.height;
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
