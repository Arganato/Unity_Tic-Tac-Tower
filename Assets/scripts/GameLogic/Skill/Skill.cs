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
	
	// move to Skill-class
	public static int UseSkill(int skill){
		int activePlayer = control.activePlayer;
		//Returns an error code 
		switch (skill){
			case 0: //no skill
				skillInUse = 0;
				return 0;
			case 1: //shoot
				Debug.Log("shoot selected");
				if (control.player[activePlayer].playerSkill.shoot > 0){
					if( skillsUsed.shoot <= control.player[activePlayer].playerSkill.square + extraSkillCap){
						skillInUse = 1;
						return 0;
						//no error
					}else{
						return 2;
						//"not enough squares"-error
					}
				}else{
					return 1;
					//"not enough skill-ammo"-error
				}
			case 2: //build
				Debug.Log("build selected");
				
				if ( control.player[activePlayer].playerSkill.build > 0){
					if ( skillsUsed.build <= control.player[activePlayer].playerSkill.square + extraSkillCap){
						skillInUse = 2;
						return 0;
					}else{
						return 2;
						//"not enough squares"-error
					}
				}else{
					return 1;
					//"not enough skill-ammo"-error
				}

			case 3:
				Debug.Log("emp selected");
				if (skillsUsed.emp < 1){
					if (control.player[activePlayer].playerSkill.emp > 0){
						control.EMP();
						return 0;
					}else{
						return 1;
						//"not enough skill-ammo"-error
					}
				}else{
					return 2;
					//"not enough squares"-error
				}
		}
		return 3;
		//unknown error
	}
	
}
