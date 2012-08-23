using UnityEngine;
using System.Collections;

public class ButtonRow{
	
	public bool enable = true;
	public Rect position;
	private EndTurnButton endturn;
	private UndoButton undo;
	private ClockGUI p1Clock = new ClockGUI(0);
	private ClockGUI p2Clock = new ClockGUI(1);
	private StatusField statusField;
	
	private ButtonRow(Control c){
		endturn = new EndTurnButton(c);
		statusField = new StatusField();
		undo = new UndoButton(c);
		float width = 300f;
		position = new Rect(Screen.width/2-width/2,Screen.height-110,width,40);
	}
	
	public static ButtonRow Create(Control c){
		return new ButtonRow(c);
	}
	
	public static ButtonRow CreateAndroid(Control c){
		ButtonRow row = new ButtonRow(c);
		Rect gameGUIRect = SkillGUI.GetGameGUIRect(); 
		row.position = new Rect(gameGUIRect.x,gameGUIRect.y,gameGUIRect.width,gameGUIRect.height*(40f/110f));
		int sector = (int)(gameGUIRect.width/3);
		int buttonWidth = (int)(sector*0.6f);
		int timerWidth = sector-buttonWidth;
		row.p1Clock.position = new Rect(0,0,timerWidth,row.position.height);
		row.endturn.position = new Rect(timerWidth,0,buttonWidth,row.position.height);
		row.p2Clock.position = new Rect(row.position.width-timerWidth,0,timerWidth,row.position.height);
		row.undo.position = new Rect(row.position.width-sector,0,buttonWidth,row.position.height);
		row.statusField.position = new Rect(sector,0,row.position.width-sector*2,row.position.height);
		Debug.Log("creating buttonRow in rect: "+row.position);
		return row;
	}
	
	public void PrintGUI(){
		if(enable){
			if(Stats.playerController[Control.cState.activePlayer] != Stats.PlayerController.localPlayer){
				GUI.enabled = false;
			}
			GUI.BeginGroup(position);
			endturn.PrintGUI();
			undo.PrintGUI();
			p1Clock.PrintGUI();
			p2Clock.PrintGUI();
			statusField.PrintGUI();
			GUI.EndGroup();
			GUI.enabled = true;
		}
	}
	
	
}