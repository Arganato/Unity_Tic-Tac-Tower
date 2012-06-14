using UnityEngine;
using System.Collections;

public class GUI_script : MonoBehaviour {

	private Control control;
	private Grid grid;
	
	public Texture[] tSkills;
	private int showSkillInfo = 0; //0 = Reveals no info.
	
	public GUISkin customSkin;
	
	public bool enable;
	public bool lockGUI;
		
	private bool towerRow; // whether the straight or diagonal towers shall be shown	
		
	private UndoButton undobutton;
	private ClockGUI clockGui = new ClockGUI(0);
	private ConfirmMenu newGameMenu = new ConfirmMenu("New Game");
	private ConfirmMenu resignMenu = new ConfirmMenu("Resign",Screen.width - 110, Screen.height - 215);
	private PlayerInfoText playerInfoText = new PlayerInfoText();
	private SkillGUI skillGUI = SkillGUI.Create();
	private HeaderBar header = new HeaderBar();
	private ButtonRow buttonRow;
	
	void Start () {
		control = (Control)FindObjectOfType(typeof(Control));
		grid = (Grid)FindObjectOfType(typeof(Grid));
		undobutton = new UndoButton(control);
		buttonRow =  new ButtonRow(control);
	}
	
	void OnGUI() {
		if(!enable){
			return;
		}
		GUI.enabled = !lockGUI;
		
		GUI.skin = customSkin;
		
		clockGui.PrintGUI();
		
		PopupMessage.PrintGUI();
		
		header.PrintGUI();

//		playerInfoText.PrintGUI();
		
//		EndTurn();
		
		skillGUI.PrintGUI();
		
		buttonRow.PrintGUI();
		
//		SkillOverview();
		
//		SkillDescrDropdown();
		
//		if(newGameMenu.PrintGUI()){
//			Application.LoadLevel("mainMenu");
//		}
//		if(resignMenu.PrintGUI()){
//			control.UserResign();
//		}
		
//		undobutton.PrintGUI();
				
		//Console.PrintGUI();
		
		//----Framework to handle mouse-input etc----//
		GUI.enabled = true;

		if(Event.current.type == EventType.MouseDown){
			if( Event.current.type != EventType.Used ){
				grid.MouseDown(Input.mousePosition);
			}
		}
	}
	
	private void EndTurn(){
		//End Turn.
		if(control.playerDone && Stats.gameRunning && GUI.Button( new Rect(8, Screen.height-40,100, 40), "End Turn")){
			control.UserEndTurn();
		}	
	}
	

	private void SkillOverview(){
		GUI.Box(new Rect(Screen.width/2-200, 30, 400, 100), "");

		//tanke for å switche bilder:
		//legge til 4 på telleren, og deretter håndtere skillInUse
		//kan caste boolen til en int og gange med 4 for å lage generell formel
		for(int i = 0; i<3; i++){
			if(Skill.skillInUse != (i+1) && GUI.Button(new Rect(Screen.width/2-200+i*100, 30, 100, 100),tSkills[i+Bool2Int(towerRow)*5]) ){
				//the user has pressed a skill-button
				UseSkillError(Skill.UseSkill(i+1));
			}else if( Skill.skillInUse == i+1 ){
				GUI.Box(new Rect(Screen.width/2-200+i*100, 30, 100, 100),tSkills[i+Bool2Int(towerRow)*5]);
			}
		}
		
		if(Skill.skillInUse == 0){
			GUI.Box(new Rect(Screen.width/2+100, 30, 100, 100), tSkills[3+Bool2Int(towerRow)*5]);
		}else{
			if(GUI.Button(new Rect(Screen.width/2+100, 30, 100, 100), tSkills[4])){
				Skill.UseSkill(0);
			}
		}
		towerRow = GUI.Toggle(new Rect(Screen.width/2-225,105,25,25),towerRow, "","towerDispToggle");
		
		switch(showSkillInfo){
			case 0:
				break;
			case 1:
				GUI.Box(new Rect(Screen.width/2-218, 144, 436, 70),ResourceFactory.GetDescription(TowerType.shoot), "darkBox");
				break;
			case 2:
				GUI.Box(new Rect(Screen.width/2-218, 144, 436, 70),ResourceFactory.GetDescription(TowerType.build), "darkBox");
				break;
			case 3:
				GUI.Box(new Rect(Screen.width/2-218, 144, 436, 70),ResourceFactory.GetDescription(TowerType.silence), "darkBox");
				break;
			case 4:
				GUI.Box(new Rect(Screen.width/2-218, 144, 436, 70),ResourceFactory.GetDescription(TowerType.skillCap), "darkBox");
				break;
		}		
		
	}
	
	private void SkillDescrDropdown(){
		for(int i = 1;i<5;i++){
			if(showSkillInfo != i){ 
				if(GUI.Button(new Rect(Screen.width/2-270+i*100, 130, 40, 15),ResourceFactory.GetArrowDown())){
					showSkillInfo = i;
				}
			}else{ 
				if(GUI.Button(new Rect(Screen.width/2-270+i*100, 130, 40, 15),ResourceFactory.GetArrowUp())){
				showSkillInfo = 0;
				}
			}
		}		
	}
	
	private int Bool2Int(bool b){
		int i;
		if(b){
			i = 1;
		}else{
			i = 0;
		}
		return i;
	}
	
	/*
	private void PlacePieceError(int errorCode, Rect pos){
		switch(errorCode){
			case 3:
				if(Time.time < errorStartTime + errorDisplayTime){
					GUI.Box(pos, "Cannot build towers while being silenced", darkTextBoxes);
				}
				return;	
		}
		return;
	*/
	
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
