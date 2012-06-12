using UnityEngine;
using System.Collections;

public class SkillGUI{

	public bool enable;
	public Rect position = new Rect(10,Screen.height-70,300,70);
	
	private SkillButtonGUI[] buttonRow = new SkillButtonGUI[4];
	private SkillAmountGUI[] textRow = new SkillAmountGUI[4];

	public void PrintGUI(){
		GUI.BeginGroup(position);
		for( int i=0;i<buttonRow.Length;i++){
			buttonRow[i].PrintGUI();
			textRow[i].PrintGUI();
		}
		GUI.EndGroup();
	}
	
	private SkillGUI(){
		buttonRow[0] = SkillButtonGUI.CreateShoot();
		buttonRow[1] = SkillButtonGUI.CreateBuild();
		buttonRow[2] = SkillButtonGUI.CreateSilence();
		buttonRow[3] = SkillButtonGUI.CreateSkillCap();
		
		textRow[0] = SkillAmountGUI.CreateShoot();
		textRow[1] = SkillAmountGUI.CreateBuild();
		textRow[2] = SkillAmountGUI.CreateSilence();
		textRow[3] = SkillAmountGUI.CreateSkillCap();
		
	}
	
	public static SkillGUI Create(){
		//Platform-Specific code...
		return new SkillGUI();
	}
}
