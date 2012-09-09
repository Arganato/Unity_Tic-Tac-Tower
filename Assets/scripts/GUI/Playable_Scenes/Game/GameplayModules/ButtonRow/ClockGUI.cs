using UnityEngine;
using System.Collections;

public class ClockGUI{

	public bool enable = true;
	public Rect position;
	public int player;
	
	public ClockGUI(int playerID){
		player = playerID;
		if(player == 0){
			position = new Rect(0,0,40,40);
		}else{
			position = new Rect(300-40,0,40,40);
		}
	}

	public void PrintGUI(){
		if (enable){
			Color old = GUI.contentColor;
			if(player == 0)
				GUI.contentColor = Color.red;
			else
				GUI.contentColor = Color.blue;
			switch(Control.cState.player[player].gameTime.state){
			case GameTime.State.off:
				OffGUI();
				break;
			case GameTime.State.not_counting:
				NotCountingGUI();
				break;
			case GameTime.State.countingPrTurn:
				PrTurnGUI();
				break;
			case GameTime.State.countingTotal:
				TotalGUI();
				break;
			case GameTime.State.finished:
				FinishedGUI();
				break;
			}
			GUI.contentColor = old;
		}
	}
		
	private void NotCountingGUI(){
		TotalGUI();
	}
	private void PrTurnGUI(){
		int tpt = (int)Control.cState.player[player].gameTime.timePrTurn;
		GUI.Box(position,tpt.ToString());
	}
	
	private void TotalGUI(){
			GUI.Box(position,ClockFormat((int)Control.cState.player[player].gameTime.totalTime));
	}
	
	private void FinishedGUI(){
		TotalGUI();
	}
	
	private void OffGUI(){
		//no clock here
	}
	
	private string ClockFormat(int time){
		return (time/60)+":"+(time%60); 
	}
	

}
