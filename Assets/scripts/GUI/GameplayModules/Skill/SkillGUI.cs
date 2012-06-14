using UnityEngine;
using System.Collections;

public class SkillGUI{

	public bool enable;
	public Rect position = new Rect(10,Screen.height-70,300,70);
	
	private bool showHelp = false;
	private int helpSkill = 0;
	private SkillButtonGUI[] buttonRow = new SkillButtonGUI[4];
	private SkillAmountGUI[] textRow = new SkillAmountGUI[4];

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
	
	public void PrintGUI(){
		GUI.Box(position,"");
		GUI.BeginGroup(position);
		if(!showHelp){
			ButtonGUI();
		}else{
			HelpGUI();
		}
		GUI.EndGroup();
	}
	
	private void ButtonGUI(){
		for( int i=0;i<buttonRow.Length;i++){
			if(	buttonRow[i].PrintGUI() ){
				UseSkillError(Skill.UseSkill(i+1));
			}
			textRow[i].PrintGUI();
		}
		
		if(GUI.Button(new Rect(250,0,50,50),"?")){
			showHelp = true;
		}
		
	}
	
	private void HelpGUI(){
		GUI.Box(new Rect(0,0,position.height,position.height),ResourceFactory.GetSkillTexture(helpSkill));
		GUI.Box(new Rect(position.height,25,position.width-position.height,position.height-25),ResourceFactory.GetDescription((TowerType)helpSkill));
		bool moveCloser = false;
		for (int i=0; i<4;i++){
			if(i == helpSkill){
				moveCloser = true;
			}else{
				if(moveCloser){
					if(GUI.Button(new Rect(position.height+30*(i-1),0,25,25),ResourceFactory.GetSkillIcon(i))){
						helpSkill = i;
					}
				}else{
					if(GUI.Button(new Rect(position.height+30*i,0,25,25),ResourceFactory.GetSkillIcon(i))){
						helpSkill = i;
					}
				}
			}
		}
		if(GUI.Button(new Rect(position.width-25,0,25,25),"x")){
			showHelp = false;
		}		
	}
	
	public static SkillGUI Create(){
		//Platform-Specific code...
		return new SkillGUI();
	}
		
	private void UseSkillError(SkillSelectError errorCode){
		switch(errorCode){
			case SkillSelectError.NO_ERROR:
				return;
			case SkillSelectError.SKILL_AMMO_ERROR:
				PopupMessage.DisplayMessage("You dont have towers for that skill");
				return;
			case SkillSelectError.SKILL_CAP_ERROR:
				PopupMessage.DisplayMessage("Cannot use that skill that many times");
				return;
			case SkillSelectError.UNKNOWN_ERROR:
				PopupMessage.DisplayMessage("Unknown error occured");
				return;
		}
	}
}
