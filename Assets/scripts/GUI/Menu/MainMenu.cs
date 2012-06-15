using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	public enum ActiveMenu {mainMenu, tutorial, options, startGame}
	
	public GUISkin customSkin;
	
	private MenuFrame mainMenuFrame;
	private MenuFrame tutorialFrame;
	private MenuFrame optionsFrame;
	private ActiveMenu activeMenu = ActiveMenu.mainMenu;
	private Rect framePosition = new Rect(25,40,Screen.width-50,Screen.height-80);
	
	private StartGameScreen startGameScreen = new StartGameScreen();
	
	void Start () {
		mainMenuFrame = new MenuFrame("Main Menu",25,framePosition);
		tutorialFrame = new MenuFrame("Tutorial",25,framePosition);
		optionsFrame  = new MenuFrame("Options",25,framePosition);
		
		mainMenuFrame.AddButton(new LocalGameButton(this));
		mainMenuFrame.AddButton(new NetworkedButton(this));
		mainMenuFrame.AddButton(new TutorialButton(this));
		mainMenuFrame.AddButton(new OptionsButton(this));
		
		tutorialFrame.AddButton(new TutorialShootButton());
		tutorialFrame.AddButton(new TutorialBuildButton());
		tutorialFrame.AddButton(new TutorialSilenceButton());
		tutorialFrame.AddButton(new TutorialSkillCapButton());
		
	}
	
	void OnGUI () {
		GUI.skin = customSkin;
//		string gameIntro = "This boardgame is inspired by the traditional game of Tic-Tac-Toe, where you can build tetris-like towers to gain strategic advantages. The goal of the game is to build five-in-a-row, or (if no ones does) the player with the highest score wins.";
//		string generalRules = "The towers is at the core of the game. Towers can be built with any rotation and mirroring, straight and diagonal, and the second you make the shape they will be built. If you, build something that can be several towers, you will get them all. Each tower will let you use its skill once (you may save it). The same type of skill can be used as many times as you have skill cap.";
		
		GUI.Box(framePosition,"");
		switch(activeMenu){
		case ActiveMenu.mainMenu:
			mainMenuFrame.PrintGUI();
			break;
		case ActiveMenu.tutorial:
			tutorialFrame.PrintGUI();
			break;
		case ActiveMenu.options:
			optionsFrame.PrintGUI();
			break;
		case ActiveMenu.startGame:
			startGameScreen.PrintGUI();
			break;
		}
		if(activeMenu == ActiveMenu.mainMenu){
			if(GUI.Button(new Rect(framePosition.x+framePosition.width-60,framePosition.y+framePosition.height+5,60,25),"Quit")){
				Quit();
			}			
		}else{
			if(GUI.Button(new Rect(framePosition.x+framePosition.width-60,framePosition.y+framePosition.height+5,60,25),"Back")){
				GoBack();
			}
		}
	}
	
	private void GoBack(){
		switch(activeMenu){
		case ActiveMenu.mainMenu:
			//quit game..
			break;
		default:
			activeMenu = ActiveMenu.mainMenu;
			break;
		}
	}
	
	public void StartGame(bool singlePlayer){
		activeMenu = ActiveMenu.startGame;
		startGameScreen.singlePlayer = singlePlayer;
	}
	public void TutorialMenu(){
		activeMenu = ActiveMenu.tutorial;
	}
	public void OptionsMenu(){
		activeMenu = ActiveMenu.options;
	}
	private void Quit(){
		Application.Quit();
	}

}