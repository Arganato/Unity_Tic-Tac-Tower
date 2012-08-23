using UnityEngine;
using System.Collections;

public class SkillButtonGUI {

	
	public bool enable = true;
	public Rect position = new Rect(Screen.width/2,Screen.height/2,50,50);
	public TowerType type;
	
	public bool PrintGUI(){
		if(type == TowerType.skillCap){
			GUI.Box(position,new GUIContent(ResourceFactory.GetSkillIcon(3),ResourceFactory.GetSkillName(type)),"button");
			return false;
		}
		
		bool ret = GUI.Button(position,new GUIContent( ResourceFactory.GetSkillIcon((int)type),ResourceFactory.GetSkillName(type)));
		return ret;
	}
	
	private SkillButtonGUI(){}
	
	public static SkillButtonGUI CreateShoot(){
		SkillButtonGUI ret = new SkillButtonGUI();
		ret.position = new Rect(9,0,50,50);
		ret.type = TowerType.shoot;
		return ret;
	}
	public static SkillButtonGUI CreateBuild(){
		SkillButtonGUI ret = new SkillButtonGUI();
		ret.position = new Rect(68,0,50,50);
		ret.type = TowerType.build;
		return ret;
	}
	public static SkillButtonGUI CreateSilence(){
		SkillButtonGUI ret = new SkillButtonGUI();
		ret.position = new Rect(127,0,50,50);
		ret.type = TowerType.silence;
		return ret;
	}
	public static SkillButtonGUI CreateSkillCap(){
		SkillButtonGUI ret = new SkillButtonGUI();
		ret.position = new Rect(186,0,50,50);
		ret.type = TowerType.skillCap;
		return ret;
	}
	
}
