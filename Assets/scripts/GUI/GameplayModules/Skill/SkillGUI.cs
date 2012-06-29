using UnityEngine;
using System.Collections;

public class SkillGUI{

	public bool enable = true;
	public Rect position;
	
	public bool[] skillEn = new bool[4];
		
	private bool showHelp = false;
	private int helpSkill = -1;
	private SkillButtonGUI[] buttonRow = new SkillButtonGUI[4];
	private SkillAmountGUI[] textRow = new SkillAmountGUI[4];
	private SkillDescription[] descriptions = new SkillDescription[4];
	private SkillGUI(){
		buttonRow[0] = SkillButtonGUI.CreateShoot();
		buttonRow[1] = SkillButtonGUI.CreateBuild();
		buttonRow[2] = SkillButtonGUI.CreateSilence();
		buttonRow[3] = SkillButtonGUI.CreateSkillCap();
		
		textRow[0] = SkillAmountGUI.CreateShoot();
		textRow[1] = SkillAmountGUI.CreateBuild();
		textRow[2] = SkillAmountGUI.CreateSilence();
		textRow[3] = SkillAmountGUI.CreateSkillCap();
		
		descriptions[0] = new SkillDescription(TowerType.shoot);
		descriptions[0].position = new Rect(0,0,300,200);
		descriptions[1] = new SkillDescription(TowerType.build);
		descriptions[1].position = new Rect(0,0,300,200);
		descriptions[2] = new SkillDescription(TowerType.silence);
		descriptions[2].position = new Rect(0,0,300,200);
		descriptions[3] = new SkillDescription(TowerType.skillCap);
		descriptions[3].position = new Rect(0,0,300,200);
		
		
		for(int i=0;i<4;i++){
			skillEn[i] = true;
		}
				
	}
	
	public void PrintGUI(){
		if(enable){
			if(Stats.playerController[Control.cState.activePlayer] != Stats.PlayerController.localPlayer){
				GUI.enabled = false;
			}
			GUI.Box(position,"");
			GUI.BeginGroup(position);
			if(!showHelp){
				ButtonGUI();
			}else{
				NewHelpGUI();
			}
			GUI.EndGroup();
			if(showHelp && helpSkill >= 0){
				GUI.BeginGroup(new Rect(position.x,position.y-240,300,200),"");
				descriptions[helpSkill].PrintGUI();
				GUI.EndGroup();
			}
			GUI.enabled = true;
		}
	}
	
	private void ButtonGUI(){
		for( int i=0;i<buttonRow.Length;i++){
			if(skillEn[i]){
				if(buttonRow[i].PrintGUI() ){
					UseSkillError(Skill.UseSkill(i+1));
				}
				textRow[i].PrintGUI();
			}
		}
		if(GUI.Button(new Rect(265,10,25,25),"?")){
			showHelp = true;
			helpSkill = -1;
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
	
	private void NewHelpGUI(){
		for( int i=0;i<buttonRow.Length;i++){
			if(helpSkill != i && GUI.Button(new Rect(10 + i*60,0,50,50),ResourceFactory.GetSkillIcon(i))){
				helpSkill = i;
			}
		}
		GUI.Label(new Rect(0,50,300,20),"select one of the skills above for more info");
		showHelp = GUI.Toggle(new Rect(265,10,25,25),showHelp, "?","button");
		
	}
	
	public static SkillGUI Create(){
		//Platform-Specific code...
		SkillGUI ret = new SkillGUI();
		float width = 300f;
		ret.position = new Rect(Screen.width/2-width/2,Screen.height-70,width,70);
		return ret;
	}
		
	public static SkillGUI TutorialCreate(){
		//Platform-Specific code...
		SkillGUI ret = new SkillGUI();
		float width = 300f;
		if(Tutorial.towerTut != TowerType.shoot)ret.skillEn[0] = false;
		if(Tutorial.towerTut == TowerType.shoot)ret.skillEn[1] = false;
		if(Tutorial.towerTut != TowerType.silence)ret.skillEn[2] = false;
		if(Tutorial.towerTut != TowerType.skillCap)ret.skillEn[3] = false;
		ret.position = new Rect(Screen.width/2-width/2,Screen.height-70,width,70);
		return ret;
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
