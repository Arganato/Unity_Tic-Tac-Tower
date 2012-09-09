using UnityEngine;
using System.Collections;

public class ReplayButtons{
	
	public enum UserAction {NO_ACTION, BACK, FORWARD, PLAY, PAUSE} 

	public Rect position = new Rect(Screen.width/2-200, 30, 400, 100);
	public bool enabled = true;
	
	public bool isPlaying = false;
	
	public UserAction PrintGUI(){
		UserAction ret = UserAction.NO_ACTION;
		if(enabled){
			GUI.BeginGroup(position);
			ret = ButtonRow();
			GUI.EndGroup();
		}
		return ret;
	}
	
	private UserAction ButtonRow(){
		if(GUI.Button(new Rect(50,20,100,30),"Previous")){
			return UserAction.BACK;
		}if(isPlaying){
			if(GUI.Button(new Rect(150,20,100,30),"Pause")){
				isPlaying = false;
				return UserAction.PAUSE;
			}
		}else{				
			if(GUI.Button(new Rect(150,20,100,30),"Play")){
				isPlaying = true;
				return UserAction.PLAY;
			}
		}if( GUI.Button(new Rect(250,20,100,30),"Next")){
			return UserAction.FORWARD;
		}
		return UserAction.NO_ACTION;
	}
}
