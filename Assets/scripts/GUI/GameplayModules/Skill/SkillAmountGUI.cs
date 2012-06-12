using UnityEngine;
using System.Collections;

public class SkillAmountGUI{

	public Rect position;
	public bool enable;
	public TowerType type;
	
	public void PrintGUI(){
		string descr;
		if(type == TowerType.skillCap){
			descr = (Control.cState.player[0].playerSkill.skillCap+1)+"/"+(Control.cState.player[1].playerSkill.skillCap+1);			
		}else{
			descr = Control.cState.player[0].playerSkill.GetSkillAmount(type)+"/"+Control.cState.player[1].playerSkill.GetSkillAmount(type);
		}
		GUI.Box(position,descr,"invisBox");
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
