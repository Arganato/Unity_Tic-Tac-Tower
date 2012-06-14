using UnityEngine;
using System.Collections;

public class GameClock : MonoBehaviour {
	
	
	private Control control;
	private int activePlayer;
	// Use this for initialization
	void Awake () {
		control = (Control)FindObjectOfType(typeof(Control));
	}
	
	// Update is called once per frame
	void Update () {
		if(Control.cState.activePlayer == activePlayer){
			GameTime gameTime = Control.cState.player[activePlayer].gameTime;
			if(gameTime.IsActive() && Stats.gameRunning){
				DecTimer(gameTime);
			}
		}else{
			ChangePlayer();
		}
	
	}
	
	private void DecTimer(GameTime gt){
		if( gt.ProgressTime(Time.deltaTime)){
			control.TimeOut();
		}
		Control.cState.player[activePlayer].gameTime = gt;
	}
	
	private void ChangePlayer(){
		Control.cState.player[activePlayer].gameTime.SetCounting(false);
		activePlayer = Control.cState.activePlayer;
		Control.cState.player[activePlayer].gameTime.SetCounting(true);
	}
}
