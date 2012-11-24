using UnityEngine;
using System.Collections;

public class SkillAmountGUI{

	public Rect position;
	public bool enable;
	public TowerType type;
	
	public void PrintGUI(){
		Color old = GUI.contentColor;
		GUI.BeginGroup(position);
		if(type == TowerType.skillCap){
			GUI.contentColor = ResourceFactory.GetPlayer1Color();
			GUI.Box(new Rect(0,0,position.width/2,position.height), ""+(Control.cState.player[0].playerSkill.skillCap+1));
			GUI.contentColor = ResourceFactory.GetPlayer2Color();
			GUI.Box(new Rect(0+position.width/2,0,position.width/2,position.height), ""+(Control.cState.player[1].playerSkill.skillCap+1));
		}else{
			GUI.contentColor = ResourceFactory.GetPlayer1Color();
			GUI.Box(new Rect(0,0,position.width/2,position.height), ""+(Control.cState.player[0].playerSkill.GetSkillAmount(type)));
			GUI.contentColor = ResourceFactory.GetPlayer2Color();
			GUI.Box(new Rect(0+position.width/2,0,position.width/2,position.height), ""+(Control.cState.player[1].playerSkill.GetSkillAmount(type)));
		}
		GUI.EndGroup();
		GUI.contentColor = old;
	}
	
	private SkillAmountGUI(){}
	
	public static SkillAmountGUI CreateShoot(){
		SkillAmountGUI ret = new SkillAmountGUI();
		ret.position = new Rect(10,50,50,20);
		ret.type = TowerType.shoot;
		return ret;
	}
	public static SkillAmountGUI CreateBuild(){
		SkillAmountGUI ret = new SkillAmountGUI();
		ret.position = new Rect(70,50,50,20);
		ret.type = TowerType.build;
		return ret;
	}
	public static SkillAmountGUI CreateSilence(){
		SkillAmountGUI ret = new SkillAmountGUI();
		ret.position = new Rect(130,50,50,20);
		ret.type = TowerType.silence;
		return ret;
	}
	public static SkillAmountGUI CreateSkillCap(){
		SkillAmountGUI ret = new SkillAmountGUI();
		ret.position = new Rect(190,50,50,20);
		ret.type = TowerType.skillCap;
		return ret;
	}
	
}
