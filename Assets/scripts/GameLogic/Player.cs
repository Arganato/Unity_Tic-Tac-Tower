using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player {
	public int score;		//Player score.	
	public bool silenced;	//Status saying whether players is silenced or not.
	
	public SkillContainer playerSkill = new SkillContainer();
		
	public void AddScore(int numbOfTowers){
		score += 100*numbOfTowers;
		if(numbOfTowers > 1){
			score += 20*(numbOfTowers-1);
		}
	}
	
	public void EndTurn(int numbOfSquares){
		if(!silenced){
		score += numbOfSquares * 5;
		}
		silenced = false;
	}
}