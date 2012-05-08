using UnityEngine;
using System.Collections;

public class GUI_script : MonoBehaviour {

	private Control control;
	private Grid grid;
	//private string[] skillNames =new string[4] {"cancel", "shoot", "build", "emp"};
	
	public Texture[] tSkills;
	public Texture smallArrowUp;
	public Texture smallArrowDown;
	private int showSkillInfo = 0; //0 = Reveals no info.
	public GUIStyle darkTextBoxes;
	public GUIStyle toggleTowerDisplay;
	
	public bool enable;
	public bool lockGUI;
	
	private string buildText = "Build Tower: \nAllows the player to place one more piece on the board.\nThis will not, however, reset the amount of skills used, as if starting a new round.";
	private string shootText = "Shoot Tower: \nThe player may destroy another unused, hostile piece on the board.\nThe piece is ruined, and the tile cannot be built upon.";
	private string empText = "EMP Tower: \nThe opponent is rendered unable to place a piece where he/she would normally be able to build a tower. Also, the opponent will not benefit from any abilities next turn.";
	private string squareText = "Square Tower: \nIncreases the skill cap by one for the player who builds it, allowing\n the player to use the same skill one more time during the same round.\nIn addition, the player will gain five score points at the end of each turn.";
	
	private Rect consoleWindowRect = new Rect(20,Screen.height/2,400,150);
	private string consoleString = "";
	private string consoleEditable = "";
	
	private bool toggleConsole = false;
	public Vector2 scrollPosition = Vector2.zero;
	private int consoleTextHeight = 20;
	
	private bool towerRow; // whether the straight or diagonal towers shall be shown
	
	private bool confirmNewGame = false;
	
	SkillSelectError skillError;		//Case number for the specific error to be displayed on screen.
	float errorStartTime;				//Time stamp for error display start.
	static float errorDisplayTime = 3; 	//How many seconds an error is displayed on screen.
	
	void Start () {
		control = (Control)FindObjectOfType(typeof(Control));
		grid = (Grid)FindObjectOfType(typeof(Grid));
		//enable = true;
		//lockGUI = false;
	}
	
	void OnGUI() {
		if(!enable){
			return;
		}
		GUI.enabled = !lockGUI;
		
		toggleConsole = GUI.Toggle(new Rect(10, 180, 100, 30), toggleConsole, "Toggle Console", "box");
		
		TextInfo();
		
		EndTurn();
		
		SkillOverview();
		
		SkillDescrDropdown();
		
		NewGameMenu();
		
		if(toggleConsole){
			consoleWindowRect = GUI.Window(0,consoleWindowRect,ConsoleWindow,"Console");
		}
		
		
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
		if(control.playerDone && GUI.Button( new Rect(8, Screen.height-40,100, 40), "End Turn")){
			control.UserEndTurn();
		}	
	}
	
	private void TextInfo(){
		string p1SkillInfo = "Player 1 skills\nShoot: "+control.player[0].playerSkill.shoot+
				"\nBuild: "+control.player[0].playerSkill.build+"\nEMP: "+control.player[0].playerSkill.emp+
				"\nSkill cap: "+(int)(1+control.player[0].playerSkill.square+Skill.extraSkillCap);
		string p2SkillInfo = "Player 2 skills\nShoot: "+control.player[1].playerSkill.shoot+
				"\nBuild: "+control.player[1].playerSkill.build+"\nEMP: "+control.player[1].playerSkill.emp+
				"\nSkill cap: "+(int)(1+control.player[1].playerSkill.square+Skill.extraSkillCap);
		
		GUI.Box(new Rect(0,0,90,100), p1SkillInfo);
		GUI.Box(new Rect(Screen.width - 90,0,90,100), p2SkillInfo);
		
		GUI.Box(new Rect(0,110,90,45), "Player 1 \n score: " +control.player[0].score);
		GUI.Box(new Rect(Screen.width-90,110,90,45), "Player 2 \nscore: " +control.player[1].score);
		
		//Turns until skill cap increase.
		if(control.placedPieces <= control.totalArea/3){
			GUI.Box(new Rect(Screen.width/2-200, 0, 400, 25), "Player " + (control.activePlayer+1) + "'s turn.  " 
					+ "Skill cap: " + (int)(1+Skill.extraSkillCap) + ".   Skill cap increase after: " 
					+ (int)(control.totalArea/3+1-control.placedPieces) + " tiles.");
		}else if(control.placedPieces <= 2*control.totalArea/3){
			GUI.Box(new Rect(Screen.width/2-200, 0, 400, 25), "Player " + (control.activePlayer+1) + "'s turn.  " 
					+ "Skill cap: " + (int)(1+Skill.extraSkillCap) + ".   Skill cap increase after: " 
					+ (int)(control.totalArea*2/3+1-control.placedPieces) + " tiles.");
		}else{
			GUI.Box(new Rect(Screen.width/2-200, 0, 400, 25), "Player " + (control.activePlayer+1) + "'s turn.  " 
					+ "Skill cap: " + (int)(1+Skill.extraSkillCap) + ".   Total skill cap increases reached");
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
				skillError = Skill.UseSkill(i+1);
				if(skillError != SkillSelectError.NO_ERROR){
					errorStartTime = Time.time;
				}
			}else if( Skill.skillInUse == i+1 ){
				GUI.Box(new Rect(Screen.width/2-200+i*100, 30, 100, 100),tSkills[i+Bool2Int(towerRow)*5]);
			}
		}
		
		UseSkillError(skillError,new Rect(Screen.width/2-100,Screen.height/2,250,25));
		
		if(Skill.skillInUse == 0){
			GUI.Box(new Rect(Screen.width/2+100, 30, 100, 100), tSkills[3+Bool2Int(towerRow)*5]);
		}else{
			if(GUI.Button(new Rect(Screen.width/2+100, 30, 100, 100), tSkills[4])){
				Skill.UseSkill(0);
			}
		}
		towerRow = GUI.Toggle(new Rect(Screen.width/2-225,105,25,25),towerRow, "",toggleTowerDisplay);
		
		switch(showSkillInfo){
			case 0:
				break;
			case 1:
				GUI.Box(new Rect(Screen.width/2-225, 145, 450, 70),shootText, darkTextBoxes);
				break;
			case 2:
				GUI.Box(new Rect(Screen.width/2-225, 145, 450, 70),buildText, darkTextBoxes);
				break;
			case 3:
				GUI.Box(new Rect(Screen.width/2-225, 145, 450, 70),empText, darkTextBoxes);
				break;
			case 4:
				GUI.Box(new Rect(Screen.width/2-225, 145, 450, 70),squareText, darkTextBoxes);
				break;
		}		
		
	}
	
	private void SkillDescrDropdown(){
		for(int i = 1;i<5;i++){
			if(showSkillInfo != i){ 
				if(GUI.Button(new Rect(Screen.width/2-270+i*100, 130, 40, 15),smallArrowDown)){
					showSkillInfo = i;
				}
			}else{ 
				if(GUI.Button(new Rect(Screen.width/2-270+i*100, 130, 40, 15),smallArrowUp)){
				showSkillInfo = 0;
				}
			}
		}		
	}
	
	private void NewGameMenu(){
		if( !confirmNewGame && GUI.Button(new Rect(Screen.width - 100, Screen.height - 280, 100, 40), "NEW GAME")){
			confirmNewGame = true;
		}else if(confirmNewGame){
			GUI.Box(new Rect(Screen.width - 100, Screen.height - 280, 100, 40), "NEW GAME");
			if(GUI.Button(new Rect(Screen.width - 110, Screen.height - 240, 55, 25), "confirm")){
				control.StartNewGame();
				confirmNewGame = false;
			}else if(GUI.Button(new Rect(Screen.width - 55, Screen.height - 240, 55, 25), "cancel")){
				confirmNewGame = false;
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
	
	private void UseSkillError(SkillSelectError errorCode, Rect pos){
		switch(errorCode){
			case SkillSelectError.NO_ERROR:
				//no error
				errorStartTime = 0;
				return;
			case SkillSelectError.SKILL_AMMO_ERROR: 
				//no more skills
				if(Time.time < errorStartTime + errorDisplayTime){
					GUI.Box(pos, "You dont have towers for that skill", darkTextBoxes);
				}
				return;
			case SkillSelectError.SKILL_CAP_ERROR: //not enough skillCap
				if(Time.time < errorStartTime + errorDisplayTime){
					GUI.Box(pos, "Cannot use that skill that many times", darkTextBoxes);
				}
				return;
			case SkillSelectError.UNKNOWN_ERROR:
				if(Time.time < errorStartTime + errorDisplayTime){
					GUI.Box(pos, "Unknown error occured", darkTextBoxes);
				}
				return;
		}
		return;
	}
	
	private void ConsoleWindow(int windowID){
		scrollPosition = GUI.BeginScrollView(new Rect(0, 15, consoleWindowRect.width, consoleWindowRect.height-60), scrollPosition, new Rect(0, 0, consoleWindowRect.width,consoleTextHeight));
		GUI.TextArea(new Rect(0,0,consoleWindowRect.width,consoleTextHeight),consoleString);
		GUI.EndScrollView();
		consoleEditable = GUI.TextField(new Rect(20,consoleWindowRect.height-20,consoleWindowRect.width-40,20),consoleEditable);
	
		if(GUI.Button(new Rect(consoleWindowRect.width-20,consoleWindowRect.height-20,20,20),"Send")){
			Debug.Log("string recieved: "+consoleEditable);
			control.ExecuteTurn(Turn.StringToTurn(consoleEditable));
		}
		GUI.DragWindow(new Rect(0,0,consoleWindowRect.width,15));
	}
	
	public void PrintToConsole(string s){
		consoleTextHeight+=15;
		consoleString+= s+"\n";
	}
}
