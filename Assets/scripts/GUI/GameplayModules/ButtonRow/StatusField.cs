using UnityEngine;
using System.Collections;

public class StatusField {

	public Rect position = new Rect(100,0,100,40);
	
	private Control control;

	public StatusField(Control c){
		control = c;
	}
	
	public void PrintGUI(){
		GUI.BeginGroup(position);
		float w = position.width;
		GUI.Box(new Rect(0.10f*w,0,0.2f*w,position.height/2),GetSkillInUseContent());
		GUI.Box(new Rect(0.4f*w,0,0.20f*w,position.height/2),GetSilenceContent());
		if(Skill.skillInUse > 0){
			if(GUI.Button(new Rect(0.7f*w,0,0.2f*w,position.height/2),new GUIContent(ResourceFactory.GetCancelTexture(),"Cancel"))){
				Skill.UseSkill(0);
			}
		}else{
			GUI.Box(new Rect(0.7f*w,0,0.2f*w,position.height/2),new GUIContent("","Cancel"));
		}
			
		
		GUI.Label(new Rect(0,position.height/2,position.width,position.height/2),GUI.tooltip,"labelCentered");
		GUI.EndGroup();
	}
	
	private GUIContent GetSkillInUseContent(){
		switch(Skill.skillInUse){
		case 0:
			if(!control.playerDone)
				return new GUIContent("p","Place");
			else
				return new GUIContent("","Skill in use");
		case 1:
			return new GUIContent("s","Shoot");
		case 2: 
			return new GUIContent("b","Build");
		case 3:
			return new GUIContent("i", "Silence");
		default:
			return new GUIContent("","Skill in use");
		}
	}
	
	private bool GetSilencedStatus(){
		if(Stats.hasConnection){
			if(Stats.playerController[0] == Stats.PlayerController.localPlayer){
				return Control.cState.player[0].silenced;
			}else if(Stats.playerController[1] == Stats.PlayerController.localPlayer){
				return Control.cState.player[1].silenced;
			}//if not: no controlled player. show firstplayer 
		}
		return Control.cState.player[Control.cState.activePlayer].silenced;
	}
	
	private GUIContent GetSilenceContent(){
		if(GetSilencedStatus())
			return new GUIContent("x","silenced");
		else
			return new GUIContent("","not silenced");
	}
	
}
