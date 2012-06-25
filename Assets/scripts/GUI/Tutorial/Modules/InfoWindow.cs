using UnityEngine;
using System.Collections;

public class InfoWindow{
	
	private int width = 300;
	private int height = 400;
	
	public string textString;
	public Rect position;
	//public Rect position = new Rect(0,0,Screen.width/2,150);

	private int textureSize = 75;
	private int textureType;


	public InfoWindow(){
		position = new Rect((Screen.width-width)/2,(Screen.height-height)/2,width,height);
	}

	public void PrintTutorialText(){
		switch(Tutorial.towerTut){
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
		GUI.BeginGroup(position);
		GUI.Box(new Rect(0,0,position.width,position.height),"");
		if(Tutorial.chapter == Tutorial.Chapter.textStr || Tutorial.chapter == Tutorial.Chapter.tutStr){
			GUI.Box(new Rect((width-textureSize)/2,0,textureSize,textureSize),ResourceFactory.GetSkillTexture(textureType));
		}else if(Tutorial.chapter == Tutorial.Chapter.textDiag || Tutorial.chapter == Tutorial.Chapter.tutDiag){
			GUI.Box(new Rect((width-textureSize)/2,0,textureSize,textureSize), ResourceFactory.GetSkillTexture(textureType+4));
		}else if(Tutorial.chapter == Tutorial.Chapter.end){
			GUI.Box(new Rect(width/2-textureSize,0,textureSize,textureSize),ResourceFactory.GetSkillTexture(textureType));
			GUI.Box(new Rect(width/2,0,textureSize,textureSize), ResourceFactory.GetSkillTexture(textureType+4));
		}
		GUI.Box(new Rect(0,textureSize,position.width-textureSize,position.height), ResourceFactory.GetTutorialMessage(),"invisBox");
		GUI.EndGroup();
	}
}
