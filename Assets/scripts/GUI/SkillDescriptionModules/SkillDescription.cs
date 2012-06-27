using UnityEngine;
using System.Collections;

public class SkillDescription{
	
	private int width = 300;
	private int height = 250;
	
	public string descrString;
	public Rect position;
	//public Rect position = new Rect(0,0,Screen.width/2,150);

	private int textureSize = 75;
	private int textureType;


	public SkillDescription(TowerType tower){
		descrString = ResourceFactory.GetDescription(tower);
		//position = new Rect((Screen.width-width)/2,(Screen.height-height)/2,width,height);
		position = new Rect(0,0,width,height);
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
		GUI.Box(new Rect(width/2-textureSize,0,textureSize,textureSize),ResourceFactory.GetSkillTexture(textureType));
		GUI.Box(new Rect(width/2,0,textureSize,textureSize), ResourceFactory.GetSkillTexture(textureType+4));
		GUI.Box(new Rect(0,textureSize,position.width-textureSize,position.height), descrString,"invisBox");
		GUI.EndGroup();
	}
}
