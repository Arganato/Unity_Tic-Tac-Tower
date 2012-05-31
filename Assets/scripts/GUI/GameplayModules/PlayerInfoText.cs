using UnityEngine;
using System.Collections;

public class PlayerInfoText {

	
	public void PrintGUI(){
		TextInfo();
		
	}
	
	private void TextInfo(){
		string p1SkillInfo = "Player 1 skills\nShoot: "+Control.cState.player[0].playerSkill.shoot+
				"\nBuild: "+Control.cState.player[0].playerSkill.build+"\nSilence: "+Control.cState.player[0].playerSkill.emp+
				"\nSkill cap: "+(int)(1+Control.cState.player[0].playerSkill.square+Control.cState.globalSkillCap);
		string p2SkillInfo = "Player 2 skills\nShoot: "+Control.cState.player[1].playerSkill.shoot+
				"\nBuild: "+Control.cState.player[1].playerSkill.build+"\nSilence: "+Control.cState.player[1].playerSkill.emp+
				"\nSkill cap: "+(int)(1+Control.cState.player[1].playerSkill.square+Control.cState.globalSkillCap);
		
		GUI.Box(new Rect(0,0,90,100), p1SkillInfo);
		GUI.Box(new Rect(Screen.width - 90,0,90,100), p2SkillInfo);
		
		GUI.Box(new Rect(0,110,90,45), "Player 1 \n score: " +Control.cState.player[0].score);
		GUI.Box(new Rect(Screen.width-90,110,90,45), "Player 2 \nscore: " +Control.cState.player[1].score);
		
		//Turns until skill cap increase.
		if(Stats.rules == Stats.Rules.SOLID_TOWERS){
			if(Control.cState.placedPieces <= Stats.totalArea/3){
				GUI.Box(new Rect(Screen.width/2-200, 0, 400, 25), "Player " + (Control.cState.activePlayer+1) + "'s turn.  " 
						+ "Skill cap: " + (int)(1+Control.cState.globalSkillCap) + ".   Skill cap increase after: " 
						+ (int)(Stats.totalArea/3+1-Control.cState.placedPieces) + " tiles.");
			}else if(Control.cState.placedPieces <= 2*Stats.totalArea/3){
				GUI.Box(new Rect(Screen.width/2-200, 0, 400, 25), "Player " + (Control.cState.activePlayer+1) + "'s turn.  " 
						+ "Skill cap: " + (int)(1+Control.cState.globalSkillCap) + ".   Skill cap increase after: " 
						+ (int)(Stats.totalArea*2/3+1-Control.cState.placedPieces) + " tiles.");
			}else{
				GUI.Box(new Rect(Screen.width/2-200, 0, 400, 25), "Player " + (Control.cState.activePlayer+1) + "'s turn.  " 
						+ "Skill cap: " + (int)(1+Control.cState.globalSkillCap) + ".   Total skill cap increases reached");
			}	
		}else if( Stats.rules == Stats.Rules.INVISIBLE_TOWERS){
			GUI.Box(new Rect(Screen.width/2-200, 0, 400, 25), "Player " + (Control.cState.activePlayer+1) + "'s turn.  " 
						+ "Skill cap: " + (int)(1+Control.cState.globalSkillCap) + ".   Total skill cap increases reached");
		}
	}
}
