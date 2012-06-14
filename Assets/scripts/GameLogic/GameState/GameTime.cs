using UnityEngine;
using System.Collections;

public struct GameTime{
	
	public enum State {off, not_counting, countingPrTurn, countingTotal, finished};
	
	public float timePrTurn;
	public float startTimePrTurn;
	public float totalTime;
	public float startTotalTime;
	
	public State state;
	
//	public bool enabled;
//	public bool isFinished;
	
	public GameTime(float tpt, float tt){
		bool enabled = tpt>0 || tt>0;
		timePrTurn = tpt;
		startTimePrTurn = tpt;
		totalTime = tt;
		startTotalTime = tt;
		if(enabled){
			state = State.not_counting;
		}else{
			state = State.off;
		}
	}
	public void SetCounting(bool b){
		if(b){
			if(timePrTurn>0){
				state = State.countingPrTurn;
			}else if(totalTime>0){
				state = State.countingTotal;
			}else{
				state = State.finished;
			}
		}else{
			state = State.not_counting;
		}
	}
	
	public bool ProgressTime(float time){
		if( timePrTurn > 0){
			timePrTurn -= time;
			state = State.countingPrTurn;
			return false;
		}else if(totalTime > 0){
			totalTime -= time;
			timePrTurn = 0;
			state = State.countingTotal;
			return false;
		}else{
			//time out
			totalTime = 0;
			state = State.finished;
			return true;
		}
	}
	
	public bool IsActive(){
		return state != State.off && state != State.finished;
	}
}
