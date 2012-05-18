using UnityEngine;
using System.Collections;

public class ClockGUI{

	public bool enable = true;
	public Rect p1Position = new Rect(0,150,90,60);
	public Rect p2Position = new Rect(Screen.width-90,150,90,60);
	
	public void PrintGUI(){
		GameTime p1Timer = Control.cState.player[0].gameTime;
		GameTime p2Timer = Control.cState.player[1].gameTime;
		if (enable && p1Timer.enabled){
			int tpt = (int)p1Timer.timePrTurn;
			GUI.BeginGroup(p1Position);
			GUI.Box(new Rect(0,0,p1Position.width,p1Position.height),"Time:");
			GUI.Box(new Rect(25,30,40,25),ClockFormat((int)p1Timer.totalTime), "invisBox");
			GUI.Box(new Rect(60,15,20,20),tpt.ToString(),"invisBox");
			GUI.EndGroup();
		}
		if (enable && p2Timer.enabled){
			int tpt = (int)p2Timer.timePrTurn;
			GUI.BeginGroup(p2Position);
			GUI.Box(new Rect(0,0,p2Position.width,p2Position.height),"Time:");
			GUI.Box(new Rect(25,30,40,25),ClockFormat((int)p2Timer.totalTime), "invisBox");
			GUI.Box(new Rect(60,15,20,20),tpt.ToString(),"invisBox");
			GUI.EndGroup();
		}
	}
	
	private string ClockFormat(int time){
		return (time/60)+":"+(time%60); 
	}
	

}
