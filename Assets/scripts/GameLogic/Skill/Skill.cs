using UnityEngine;
using System.Collections;

public static class Skill {
	// Contains rules associated with the usage of skills, 
	// in terms of functions to check them 
	
	public static int skillInUse;
	
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
	public static SkillSelectError CanUseShoot(GameState state){
		if ( !(state.skillsUsed.shoot <= state.player[state.activePlayer].playerSkill.skillCap + state.globalSkillCap) ) 
			return SkillSelectError.SKILL_CAP_ERROR;
		if(!(state.player[state.activePlayer].playerSkill.shoot > 0))
			return SkillSelectError.SKILL_AMMO_ERROR;
		return SkillSelectError.NO_ERROR;
	}
	
	public static SkillSelectError CanUseBuild(GameState state){
		if ( !(state.skillsUsed.build <= state.player[state.activePlayer].playerSkill.skillCap + state.globalSkillCap)) 
			return SkillSelectError.SKILL_CAP_ERROR;
		if( !(state.player[state.activePlayer].playerSkill.build > 0) )
			return SkillSelectError.SKILL_AMMO_ERROR;
		return SkillSelectError.NO_ERROR;
	}
	
	public static SkillSelectError CanUseSilence(GameState state){
		if( !(state.skillsUsed.silence < 1) )
			return SkillSelectError.SKILL_CAP_ERROR;
		if( !(state.player[state.activePlayer].playerSkill.silence > 0) )
			return SkillSelectError.SKILL_AMMO_ERROR;
		return SkillSelectError.NO_ERROR;
	}
	
	public static SkillSelectError CanUseShoot(){
		return CanUseShoot(Control.cState);
	}
	public static SkillSelectError CanUseBuild(){
		return CanUseBuild(Control.cState);
	}
	public static SkillSelectError CanUseSilence(){
		return CanUseSilence(Control.cState);
	}


}
