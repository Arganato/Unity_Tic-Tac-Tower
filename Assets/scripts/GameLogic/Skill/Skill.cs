using UnityEngine;
using System.Collections;

public static class Skill {
	// Contains rules associated with the usage of skills, 
	// in terms of functions to check them 
	
	public static int skillInUse;
	
	public static void UseSkill(int skill){
		SkillSelectError error = CanUseSkill((SkillType)skill);
		if( error == SkillSelectError.NO_ERROR){
			skillInUse = skill;
		}else{
			UseSkillError(error);
		}
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
	
	private static void UseSkillError(SkillSelectError errorCode){
		switch(errorCode){
			case SkillSelectError.NO_ERROR:
				return;
			case SkillSelectError.SKILL_AMMO_ERROR:
				PopupMessage.DisplayMessage("You dont have towers for that skill");
				return;
			case SkillSelectError.SKILL_CAP_ERROR:
				PopupMessage.DisplayMessage("Cannot use that skill that many times");
				return;
			case SkillSelectError.UNKNOWN_ERROR:
				PopupMessage.DisplayMessage("Unknown error occured");
				return;
		}
	}

}
