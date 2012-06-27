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
			return (Control.cState.field[7,2] == Route.destroyed ||
				Control.cState.field[6,2] == Route.destroyed ||
				Control.cState.field[5,2] == Route.destroyed ||
				Control.cState.field[4,2] == Route.destroyed) 
				&&
				Control.cState.field[5,4] == Route.destroyed ||
				Control.cState.field[5,5] == Route.destroyed ||
				Control.cState.field[5,7] == Route.destroyed ||
				Control.cState.field[5,8] == Route.destroyed;
		}
		Debug.LogError("invalid chapter: "+chapter.ToString());
		return true;
	}
	
	private static bool CheckBuild(Tutorial.Chapter chapter){
		if(chapter == Tutorial.Chapter.tutStr){
			return Control.cState.field[6,6] == Route.red &&
				Control.cState.field[5,6] == Route.red;			
		}else if(chapter == Tutorial.Chapter.tutDiag){
			return Control.cState.field[1,2] == Route.red &&
				Control.cState.field[1,4] == Route.red;
		}
		Debug.LogError("invalid chapter: "+chapter.ToString());
		return true;
	}
	
	private static bool CheckSilence(Tutorial.Chapter chapter){
		if(chapter == Tutorial.Chapter.tutStr){
			return (Control.cState.field[5,2] == Route.red ||
				 Control.cState.field[5,7] == Route.red)
				&&
				Control.cState.player[1].silenced;		
		}else if(chapter == Tutorial.Chapter.tutDiag){
			return (Control.cState.field[0,2] == Route.red ||
				 Control.cState.field[5,7] == Route.red)
				&&
				Control.cState.player[1].silenced;
		}
		Debug.LogError("invalid chapter: "+chapter.ToString());
		return true;
	}
	
	private static bool CheckSkillCap(Tutorial.Chapter chapter){
		if(chapter == Tutorial.Chapter.tutStr){
			return Control.cState.field[8,2] == Route.red &&
				Control.cState.field[3,2] == Route.red &&
				Control.cState.field[3,7] == Route.red;	
		}else if(chapter == Tutorial.Chapter.tutDiag){
			return Control.cState.field[8,7] == Route.red &&
				Control.cState.field[6,7] == Route.red &&
				Control.cState.field[4,7] == Route.red;	
		}
		Debug.LogError("invalid chapter: "+chapter.ToString());
		return true;
	}
	
}
