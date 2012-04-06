using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct Player {
	public int score;
	public int currPlayer;
	
	public bool silenced;
	
	
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