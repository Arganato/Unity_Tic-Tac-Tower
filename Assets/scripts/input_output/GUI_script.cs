using UnityEngine;
using System.Collections;

public class GUI_script : MonoBehaviour {

	
	private Control control;
	//private string[] skillNames =new string[4] {"cancel", "shoot", "build", "emp"};
	
	public Texture[] tSkills;
	public Texture smallArrowUp;
	public Texture smallArrowDown;
	private int showSkillInfo = 0; //0 = Reveals no info.
	public GUIStyle darkTextBoxes;
	public GUIStyle toggleTowerDisplay;
	
	private string buildText = "Build Tower: \nAllows the player to place one more piece on the board.\nThis will not, however, reset the amount of skills used, as if starting a new round.";
	private string shootText = "Shoot Tower: \nThe player may destroy another unused, hostile piece on the board.\nThe piece is ruined, and the tile cannot be built upon.";
	private string empText = "EMP Tower: \nThe opponent is rendered unable to place a piece where he/she would normally be able to build a tower. Also, the opponent will not benefit from any abilities next turn.";
	private string squareText = "Square Tower: \nIncreases the skill cap by one for the player who builds it, allowing\n the player to use the same skill one more time during the same round.\nIn addition, the player will gain five score points at the end of each turn.";
	
	private bool towerRow; // whether the straight or diagonal towers shall be shown
	
	private bool confirmNewGame = false;
	
	int skillError;		//Case number for the specific error to be displayed on screen.
	float errorStartTime;	//Time stamp for error display start.
	static float errorDisplayTime = 3; //How many seconds an error is displayed on screen.
	
	void Start () {
		control = (Control)FindObjectOfType(typeof(Control));
	}
	
	void OnGUI() {
		string p1SkillInfo = "Player 1 skills\nShoot: "+control.playerSkill[0].shoot+
				"\nBuild: "+control.playerSkill[0].build+"\nEMP: "+control.playerSkill[0].emp+
				"\nSkill cap: "+(int)(1+control.playerSkill[0].square+control.extraSkillCap);
		string p2SkillInfo = "Player 2 skills\nShoot: "+control.playerSkill[1].shoot+
				"\nBuild: "+control.playerSkill[1].build+"\nEMP: "+control.playerSkill[1].emp+
				"\nSkill cap: "+(int)(1+control.playerSkill[1].square+control.extraSkillCap);
		
		GUI.Box(new Rect(0,0,90,100), p1SkillInfo);
		GUI.Box(new Rect(Screen.width - 90,0,90,100), p2SkillInfo);
		
		GUI.Box(new Rect(0,110,90,45), "Player 1 \n score: " +control.playerScore[0].score);
		GUI.Box(new Rect(Screen.width-90,110,90,45), "Player 2 \nscore: " +control.playerScore[1].score);
		
		//Turns until skill cap increase.
		if(control.placedPieces <= control.totalArea/3){
			GUI.Box(new Rect(Screen.width/2-200, 0, 400, 25), "Player " + (control.firstPlayer+1) + "'s turn.  " 
					+ "Skill cap: " + (int)(1+control.extraSkillCap) + ".   Skill cap increase after: " 
					+ (int)(control.totalArea/3+1-control.placedPieces) + " tiles.");
		}else if(control.placedPieces <= 2*control.totalArea/3){
			GUI.Box(new Rect(Screen.width/2-200, 0, 400, 25), "Player " + (control.firstPlayer+1) + "'s turn.  " 
					+ "Skill cap: " + (int)(1+control.extraSkillCap) + ".   Skill cap increase after: " 
					+ (int)(control.totalArea*2/3+1-control.placedPieces) + " tiles.");
		}else{
			GUI.Box(new Rect(Screen.width/2-200, 0, 400, 25), "Player " + (control.firstPlayer+1) + "'s turn.  " 
					+ "Skill cap: " + (int)(1+control.extraSkillCap) + ".   Total skill cap increases reached");
		}
		
		//Global skill cap.
		//GUI.Box(new Rect(Screen.width/2-50, 10, 100, 40), "Skill cap:\n" + (int)(1+control.extraSkillCap));
		
		//Player turn.
		//GUI.Box( new Rect(Screen.width/2-200,20, 100, 25), "Player " +control.firstPlayer + "'s turn.  " + "Skill cap:\n" + (int)(1+control.extraSkillCap));
		
		//End Turn.
		if(control.playerDone && GUI.Button( new Rect(8, Screen.height-40,100, 40), "End Turn")){
			control.ChangeFirstPlayer();
		}
		
		//Skill overview.
		GUI.Box(new Rect(Screen.width/2-200, 30, 400, 100), "");

		//tanke for å switche bilder:
		//legge til 4 på telleren, og deretter håndtere skillinuse
		//kan caste boolen til en int og gange med 4 for å lage generell formel
		for(int i = 0; i<3; i++){
			if(control.skillInUse != (i+1) && GUI.Button(new Rect(Screen.width/2-200+i*100, 30, 100, 100),tSkills[i+Bool2Int(towerRow)*5]) ){
				//the user has pressed a skill-button
				skillError = control.UseSkill(i+1);
				if(skillError!=0){
					errorStartTime = Time.time;
				}
			}else if( control.skillInUse == i+1 ){
				GUI.Box(new Rect(Screen.width/2-200+i*100, 30, 100, 100),tSkills[i+Bool2Int(towerRow)*5]);
			}
		}
		
		UseSkillError(skillError,new Rect(Screen.width/2-100,Screen.height/2,250,25));
		
		if(control.skillInUse == 0){
			GUI.Box(new Rect(Screen.width/2+100, 30, 100, 100), tSkills[3+Bool2Int(towerRow)*5]);
		}else{
			if(GUI.Button(new Rect(Screen.width/2+100, 30, 100, 100), tSkills[4])){
				control.UseSkill(0);
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
		
		//dropdown-menu for skill description:
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
		
		//new game menu
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
	
	private void UseSkillError(int errorCode, Rect pos){ //no worky (only shows for one frame)
		switch(errorCode){
			case 0:
				//no error
				errorStartTime = 0;
				return;
			case 1: 
				//no more skills
				if(Time.time < errorStartTime + errorDisplayTime){
					GUI.Box(pos, "You dont have that skill (biatch)", darkTextBoxes);
				}
				return;
			case 2: //not enough squares
				if(Time.time < errorStartTime + errorDisplayTime){
					GUI.Box(pos, "Cannot use that skill that many times", darkTextBoxes);
				}
				return;
			case 3:
				if(Time.time < errorStartTime + errorDisplayTime){
					GUI.Box(pos, "Unknown error occured", darkTextBoxes);
				}
				return;
		}
		return;
	}
	
}
