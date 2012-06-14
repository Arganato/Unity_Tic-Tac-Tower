using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectGameTime{

	public bool enable = true;
	public Rect position = new Rect(Screen.width/2-200,100,400,200);
	
	private string p1tpt = "";
	private string p1tt = "";
	private string p2tpt = "";
	private string p2tt = "";
	private bool copyP2Toggle = true;
	
	public void PrintGUI(){
		if(enable){
			GUI.BeginGroup(position);
			GUI.Box(new Rect(0,0,position.width,position.height),"Select time cap");
			GUI.Box(new Rect(0,20,position.width/2,position.height-20),"Player 1");
			GUI.Box(new Rect(20,60,100,20),"Time per turn","invisBox");
			p1tpt = GUI.TextField(new Rect(120,60,40,20),p1tpt);
			GUI.Box(new Rect(20,80,100,20),"Total time","invisBox");
			p1tt = GUI.TextField(new Rect(120,80,40,20),p1tt);
						
			GUI.Box(new Rect(position.width/2,20,position.width/2,position.height-20),"Player 2");
			copyP2Toggle = GUI.Toggle(new Rect(220,40,160,20),copyP2Toggle,"Same as player 1");
			if(!copyP2Toggle){
				GUI.Box(new Rect(220,60,100,20),"Time per turn","invisBox");
				p2tpt = GUI.TextField(new Rect(320,60,40,20),p2tpt);
				GUI.Box(new Rect(220,80,80,20),"Total time","invisBox");
				p2tt = GUI.TextField(new Rect(320,80,40,20),p2tt);
			}
//			if(GUI.Button(new Rect(position.width-60,position.height-20,40,20),"Start Game")){
//				SetGameTime();
//			}
			GUI.EndGroup();
		}
	}
	
	public void SetGameTime(){
		float p1tptNr = 0;
		float p1ttNr = 0;
		if (p1tpt != ""){
			p1tptNr = (float)System.Convert.ToInt32(p1tpt);
		}if (p1tt != ""){
			p1ttNr = (float)System.Convert.ToInt32(p1tt);
		}
		GameTime tmp = new GameTime(p1tptNr,p1ttNr);
		Stats.startState.player[0].gameTime = tmp;
		if(copyP2Toggle){
			Stats.startState.player[1].gameTime = tmp;
		}else{
			float p2tptNr = 0;
			float p2ttNr = 0;
			if (p2tpt != ""){
				p2tptNr = (float)System.Convert.ToInt32(p2tpt);
			}if (p2tt != ""){
				p2ttNr = (float)System.Convert.ToInt32(p2tt);
			}
			Stats.startState.player[1].gameTime = new GameTime(p2tptNr,p2ttNr);
		}
	}
}
