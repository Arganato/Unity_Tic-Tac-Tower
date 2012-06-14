using UnityEngine;
using System.Collections;

public class DropdownMenu{
	
	public bool enable;
	private Rect positionClosed = new Rect(Screen.width-50,0,50,20);
	private Rect positionOpen = new Rect(Screen.width-80,20,80,100);
	
	private bool menuActive;
	
	private ConfirmMenu endGame;
	private ConfirmMenu resign;
	
	private Control control;
	
	public DropdownMenu( Control c){
		Console.buttonRect = new Rect(positionOpen.x,positionOpen.y+60,80,25);
		endGame = new ConfirmMenu("Quit Game",(int)positionOpen.x,(int)positionOpen.y);
		resign = new ConfirmMenu("Resign",(int)positionOpen.x,(int)positionOpen.y+30);
		control = c;
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
				Application.LoadLevel("mainMenu");
			}
			if(resign.PrintGUI()){
				control.UserResign();
			}
			Console.PrintButton();
			//option...
		}
	}
}
