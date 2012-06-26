using UnityEngine;
using System.Collections;

public class DropdownMenu{
	
	public bool enable;
	private Rect positionClosed = new Rect(Screen.width-50,0,50,20);
	private Rect positionOpen = new Rect(Screen.width-80,20,80,150);
	
	private bool menuActive;
	
	private ConfirmMenu endGame;
	private ConfirmMenu resign;
	private NetworkWindow networkGUI;
	
	private Control control;
	
	public DropdownMenu( Control c, NetworkInterface nif){
		control = c;

		endGame = new ConfirmMenu("Quit Game",(int)positionOpen.x,(int)positionOpen.y);
		resign = new ConfirmMenu("Resign",(int)positionOpen.x,(int)positionOpen.y+30);
		Console.buttonRect = new Rect(positionOpen.x,positionOpen.y+60,80,25);
		
		if(nif != null){
			networkGUI = new NetworkWindow(nif);
			networkGUI.togglePos = new Rect(positionOpen.x,positionOpen.y+90,80,55);
		}
	}
	
	public void PrintGUI(){
		if(!menuActive){
			if(GUI.Button(positionClosed,"Menu")){
				menuActive = true;
			}
		}else{
			if(GUI.Button(positionClosed,ResourceFactory.GetArrowUp())){
				menuActive = false;
			}
			GUI.Box(positionOpen,"");
			if(endGame.PrintGUI()){
				Control.QuitGame();
			}
			if(Stats.gameRunning && resign.PrintGUI()){
				control.UserResign();
			}
			Console.PrintButton();
			if(Stats.networkEnabled){
				networkGUI.ToggleGUI();
			}
			//option...
		}
		if(Stats.networkEnabled){
			networkGUI.WindowGUI();
		}
	}
}
