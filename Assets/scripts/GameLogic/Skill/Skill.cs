using UnityEngine;
using System.Collections;

public static class Skill {
	
	public static int skillInUse;
	public static SkillContainer skillsUsed; //skill used this turn by activePlayer
	
	
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
		case SkillType.silence:
			return CanUseSilence();
		}
		return SkillSelectError.UNKNOWN_ERROR;
	}
	public static SkillSelectError CanUseShoot(){
		if ( !(skillsUsed.shoot <= Control.cState.player[Control.cState.activePlayer].playerSkill.skillCap + Control.cState.globalSkillCap) ) 
			return SkillSelectError.SKILL_CAP_ERROR;
		if(!(Control.cState.player[Control.cState.activePlayer].playerSkill.shoot > 0))
			return SkillSelectError.SKILL_AMMO_ERROR;
		return SkillSelectError.NO_ERROR;
	}
	
	public static SkillSelectError CanUseBuild(){
		if ( !(skillsUsed.build <= Control.cState.player[Control.cState.activePlayer].playerSkill.skillCap + Control.cState.globalSkillCap)) 
			return SkillSelectError.SKILL_CAP_ERROR;
		if( !(Control.cState.player[Control.cState.activePlayer].playerSkill.build > 0) )
			return SkillSelectError.SKILL_AMMO_ERROR;
		return SkillSelectError.NO_ERROR;
	}
	
	public static SkillSelectError CanUseSilence(){
		if( !(skillsUsed.silence < 1) )
			return SkillSelectError.SKILL_CAP_ERROR;
		if( !(Control.cState.player[Control.cState.activePlayer].playerSkill.silence > 0) )
			return SkillSelectError.SKILL_AMMO_ERROR;
		return SkillSelectError.NO_ERROR;
	}
	
}
