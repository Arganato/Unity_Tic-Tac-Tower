using UnityEngine;
using System.Collections;

public class GameClock : MonoBehaviour {
	
	
	private Control control;
	// Use this for initialization
	void Awake () {
		control = (Control)FindObjectOfType(typeof(Control));
	}
	
	// Update is called once per frame
	void Update () {
		GameTime gameTime = Control.cState.player[Control.cState.activePlayer].gameTime;
		if(gameTime.enabled && !gameTime.isFinished && Stats.gameRunning){
			DecTimer(gameTime);
		}
	
	}
	
	private void DecTimer(GameTime gt){
		if( gt.timePrTurn > 0){
			gt.timePrTurn -= Time.deltaTime;
		}else if(gt.totalTime > 0){
			gt.totalTime -= Time.deltaTime;
			gt.timePrTurn = 0;
		}else{
			//time out
			gt.totalTime = 0;
			gt.isFinished = true;
			control.TimeOut();
		}
		Control.cState.player[Control.cState.activePlayer].gameTime = gt;
	}
}
