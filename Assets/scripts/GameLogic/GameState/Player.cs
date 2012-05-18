using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player {
	public int score;		//Player score.	
	public bool silenced = false;	//Status saying whether players is silenced or not.
	
	public SkillContainer playerSkill = new SkillContainer();
	public GameTime gameTime = new GameTime(0,0);
	
	public Player(){}
	public Player(GameTime gt){
		gameTime = gt;
	}
	
	public Player(Player copy){
		this.playerSkill = copy.playerSkill;
		silenced = copy.silenced;
		score = copy.score;
		gameTime = copy.gameTime;
	}
	
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
		gameTime.timePrTurn = gameTime.startTimePrTurn;
	}
	
	public void Reset(){
		score = 0;
		silenced = false;
		playerSkill.Reset();
	}
}