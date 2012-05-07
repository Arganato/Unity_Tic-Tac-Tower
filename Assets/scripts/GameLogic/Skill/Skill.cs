using UnityEngine;
using System.Collections;

public static class Skill {
	
	public static int skillInUse;
	public static SkillContainer skillsUsed; //skill used this turn by activePlayer
	public static int extraSkillCap = 0;
	
	private static Control control;
	
	
	
	public static void Init(Control c){
		control = c;
	}
	
	public static SkillSelectError UseSkill(int skill){
		SkillSelectError ret = CanUseSkill((SkillType)skill);
		if( ret == SkillSelectError.NO_ERROR){
			skillInUse = skill;
		}
		return ret;
	}

	
	public static SkillSelectError CanUseSkill(SkillType skill){
		switch(skill){
		case SkillType.noSkill:
			return SkillSelectError.NO_ERROR;
		case SkillType.place:
			return SkillSelectError.NO_ERROR;
		case SkillType.shoot:
			return CanUseShoot();
		case SkillType.build:
			return CanUseBuild();
		case SkillType.emp:
			return CanUseSilence();
		}
		return SkillSelectError.UNKNOWN_ERROR;
	}
	public static SkillSelectError CanUseShoot(){
		if ( !(skillsUsed.shoot <= control.player[control.activePlayer].playerSkill.square + extraSkillCap) ) 
			return SkillSelectError.SKILL_CAP_ERROR;
		if(!(control.player[control.activePlayer].playerSkill.shoot > 0))
			return SkillSelectError.SKILL_AMMO_ERROR;
		return SkillSelectError.NO_ERROR;
	}
	
	public static SkillSelectError CanUseBuild(){
		if ( !(skillsUsed.build <= control.player[control.activePlayer].playerSkill.square + extraSkillCap)) 
			return SkillSelectError.SKILL_CAP_ERROR;
		if( !(control.player[control.activePlayer].playerSkill.build > 0) )
			return SkillSelectError.SKILL_AMMO_ERROR;
		return SkillSelectError.NO_ERROR;
	}
	
	public static SkillSelectError CanUseSilence(){
		if( !(skillsUsed.emp < 1) )
			return SkillSelectError.SKILL_CAP_ERROR;
		if( !(control.player[control.activePlayer].playerSkill.emp > 0) )
			return SkillSelectError.SKILL_AMMO_ERROR;
		return SkillSelectError.NO_ERROR;
	}
	
}
