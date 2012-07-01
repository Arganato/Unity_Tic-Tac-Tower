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
	
	private DropdownMenu( Control c, NetworkInterface nif, int border, int buttonHeight){
		control = c;
		if(nif != null){
			networkGUI = new NetworkWindow(nif);
		}
		SetUpButtons(border, buttonHeight);
	}
	
	private void SetUpButtons(int border, int buttonHeight){
		endGame = new ConfirmMenu("Quit Game",new Rect(positionOpen.x,positionOpen.y+border,positionOpen.width,buttonHeight),
			new Rect(positionOpen.x-positionOpen.width,positionOpen.y+border,positionOpen.width,2*buttonHeight));
		resign = new ConfirmMenu("Resign",new Rect(positionOpen.x,positionOpen.y+buttonHeight+2*border,positionOpen.width,buttonHeight),
			new Rect(positionOpen.x-positionOpen.width,positionOpen.y+buttonHeight+2*border,positionOpen.width,buttonHeight*2));
		Console.buttonRect = new Rect(positionOpen.x,positionOpen.y+buttonHeight*2+border*3,positionOpen.width,buttonHeight);
		positionOpen.height = 3*buttonHeight+4*border;
		if(networkGUI != null){
			networkGUI.togglePos = new Rect(positionOpen.x,positionOpen.y+buttonHeight*4+border*5,positionOpen.width,buttonHeight*2+border);
			positionOpen.height = 5*buttonHeight+6*border;		
		}
	}
	
	public static DropdownMenu Create(Control c, NetworkInterface nif){
		return new DropdownMenu(c,nif,10,25);
	}
	public static DropdownMenu CreateAndroid(Control c, NetworkInterface nif){
		DropdownMenu dropdown = new DropdownMenu(c,nif,(int)(Screen.height*0.02),(int)(Screen.height*0.08));
		dropdown.positionOpen = new Rect(Screen.width-120,40,120,150);
		dropdown.positionClosed = new Rect(Screen.width-80,0,80,40);
		dropdown.SetUpButtons((int)(Screen.height*0.02),(int)(Screen.height*0.08));
		return dropdown;
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
