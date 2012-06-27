using UnityEngine;
using System.Collections;

public static class SolutionChecker {

	public static bool CheckSolution(Tutorial.Chapter chapter, TowerType tower){
		switch(tower){
		case TowerType.shoot:
			return CheckShoot(chapter);
		case TowerType.build:
			return CheckBuild(chapter);
		case TowerType.silence:
			return CheckSilence(chapter);
		case TowerType.skillCap:
			return CheckSkillCap(chapter);
		default:
			Debug.LogError("Invalid tutorial");
			return false;
		}
	}
	
	
	private static bool CheckShoot(Tutorial.Chapter chapter){
		if(chapter == Tutorial.Chapter.tutStr){
			return Control.cState.field[5,3] == Route.destroyed ||
				Control.cState.field[5,4] == Route.destroyed ||
				Control.cState.field[5,5] == Route.destroyed ||
				Control.cState.field[5,6] == Route.destroyed;
		}else if(chapter == Tutorial.Chapter.tutDiag){
			return true;
		}
		Debug.LogError("invalid chapter: "+chapter.ToString());
		return true;
	}
	
	private static bool CheckBuild(Tutorial.Chapter chapter){
		return true;
	}
	
	private static bool CheckSilence(Tutorial.Chapter chapter){
		return true;
	}
	
	private static bool CheckSkillCap(Tutorial.Chapter chapter){
		return true	;
	}
	
}
