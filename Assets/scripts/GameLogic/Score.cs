using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct Score {
	public int score;
	
	public void Add(int numbOfTowers){
		score += 100*numbOfTowers;
		if(numbOfTowers > 1){
			score += 20*(numbOfTowers-1);
		}
	}
	
	public void EndTurn(int numbOfSquares){
		score += numbOfSquares * 5;
	}
}