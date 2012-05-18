using UnityEngine;
using System.Collections;

public struct GameTime{

	public float timePrTurn;
	public float startTimePrTurn;
	public float totalTime;
	public float startTotalTime;
	
	public bool enabled;
	public bool isFinished;
	
	public GameTime(float tpt, float tt){
		enabled = tpt>0 || tt>0;
		timePrTurn = tpt;
		startTimePrTurn = tpt;
		totalTime = tt;
		startTotalTime = tt;
		isFinished = false;
	}
}
