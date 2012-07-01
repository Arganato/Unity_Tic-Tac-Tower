using UnityEngine;
using System.Collections;

public class SkillGUI{

	public bool enable = true;
	public Rect position;
	
	public bool[] skillEn = new bool[4];
		
	private bool showHelp = false;
	private int helpSkill = -1;
	private int buttonSize = 50;
	private int borderSize = 9;
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
	
	private void AdjustPositions(){
		buttonSize = (int)(position.width*50f/310f);
		borderSize = (int)(position.width*10f/310f);
		int textHeight = (int)(position.height-buttonSize);
		buttonRow[0].position = new Rect(borderSize,0,buttonSize,buttonSize);
		buttonRow[1].position = new Rect(borderSize*2+buttonSize,0,buttonSize,buttonSize);
		buttonRow[2].position = new Rect(borderSize*3+buttonSize*2,0,buttonSize,buttonSize);
		buttonRow[3].position = new Rect(borderSize*4+buttonSize*3,0,buttonSize,buttonSize);
		textRow[0].position = new Rect(borderSize,buttonSize,buttonSize,textHeight);
		textRow[1].position = new Rect(borderSize*2+buttonSize,buttonSize,buttonSize,textHeight);
		textRow[2].position = new Rect(borderSize*3+buttonSize*2,buttonSize,buttonSize,textHeight);
		textRow[3].position = new Rect(borderSize*4+buttonSize*3,buttonSize,buttonSize,textHeight);

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
				HelpGUI();
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
		int helpStart = (int)((buttonSize+borderSize)*4+borderSize);
		int helpSize = (int)(position.width - helpStart -borderSize);
		if(GUI.Button(new Rect(helpStart,0,helpSize,helpSize),new GUIContent("?","Help"))){
			showHelp = true;
			helpSkill = -1;
		}
		
	}
	
	
	private void HelpGUI(){
		for( int i=0;i<buttonRow.Length;i++){
			if(helpSkill != i && GUI.Button(buttonRow[i].position,new GUIContent(ResourceFactory.GetSkillIcon(i),ResourceFactory.GetSkillName(i)))){
				helpSkill = i;
			}else if(helpSkill == i){
				GUI.Box(buttonRow[i].position,new GUIContent(ResourceFactory.GetSkillIcon(i),ResourceFactory.GetSkillName(i)));
			}
		}
		GUI.Label(new Rect(0,buttonSize,position.width,position.height-buttonSize),"select one of the skills above for more info","labelCentered");
		
		int helpStart = (buttonSize+borderSize)*4+borderSize;
		int helpSize = (int)(position.width - helpStart -borderSize);
		showHelp = GUI.Toggle(new Rect(helpStart,0,helpSize,helpSize),showHelp, new GUIContent("?","Close help"),"button");
		
	}
	
	public static Rect GetGameGUIRect(){
		float guiRatio = 110f/300f;
		Rect guiPosition = new Rect(0,0,300,110);
		float pixUnderBoard = (float)(Screen.height-45-Screen.width);
		float spaceRatio = pixUnderBoard/(float)Screen.width;
		if(guiRatio > spaceRatio){
			guiPosition.height = pixUnderBoard;
			guiPosition.width = pixUnderBoard/guiRatio;
		}else{
			guiPosition.width = Screen.width;
			guiPosition.height = guiPosition.width*guiRatio;
		}
		guiPosition.x = (Screen.width-guiPosition.width)/2;
		guiPosition.y = Screen.height-guiPosition.height;
		
		return guiPosition;
	}
	
	public static SkillGUI Create(){
		//Platform-Specific code...
		SkillGUI ret = new SkillGUI();
		float width = 300f;
		ret.position = new Rect(Screen.width/2-width/2,Screen.height-70,width,70);
		return ret;
	}
	
	public static SkillGUI CreateAndroid(){
		Rect gameGUIPosition = GetGameGUIRect();
		SkillGUI skillgui = new SkillGUI();
		skillgui.position = new Rect(gameGUIPosition.x,gameGUIPosition.y+gameGUIPosition.height*(40f/110f),gameGUIPosition.width,gameGUIPosition.height*(70f/110f));
		skillgui.AdjustPositions();
//		Debug.Log("creating SkillGUI in rect: "+skillgui.position);
		return skillgui;
	}
		
	public static SkillGUI TutorialCreate(){
		//Platform-Specific code...
		SkillGUI ret = new SkillGUI();
		float width = 300f;
		if(Tutorial.towerTut == TowerType.shoot){
			for(int i=1;i<4;i++)
				ret.skillEn[i] = false;
		}else if(Tutorial.towerTut == TowerType.build){
			ret.skillEn[0] = false;
			ret.skillEn[2] = false;
			ret.skillEn[3] = false;
		}else if(Tutorial.towerTut == TowerType.silence){
			ret.skillEn[0] = false;
			ret.skillEn[3] = false;			
		}
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
