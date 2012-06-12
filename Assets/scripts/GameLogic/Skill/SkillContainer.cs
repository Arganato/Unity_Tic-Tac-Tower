using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Struct SkillContainer:
// -stores the availible skills for each player
// square (skillcap) should be 0 by default (an extra is added in the logic)

public struct SkillContainer{
	
	public int shoot;
	public int build;
	public int silence;
	public int skillCap;
		
	public void Reset(){
		shoot = 0;
		build = 0;
		silence = 0;
		skillCap = 0;
	}
	
	public bool Empty(){
		return shoot == 0 && build == 0 && silence == 0 && skillCap == 0;
	}
	
	public int GetSkillAmount(TowerType s){
		switch(s){
		case TowerType.shoot:
			return shoot;
		case TowerType.build:
			return build;
		case TowerType.silence:
			return silence;
		case TowerType.skillCap:
			return skillCap;
		}
		return -1;
	}
	
}