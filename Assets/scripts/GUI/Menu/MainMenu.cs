using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	public enum ActiveMenu {mainMenu, tutorial, options, startGame}
	
	public GUISkin customSkin;
	
	private FrameContent mainMenuFrame;
	private FrameContent tutorialFrame;
	private FrameContent optionsFrame;
	private ActiveMenu activeMenu = ActiveMenu.mainMenu;
	
	private StartGameScreen startGameScreen = new StartGameScreen();
	
	void Start () {
		mainMenuFrame = FrameContent.Create("Main Menu");
		tutorialFrame = FrameContent.Create("Tutorial");
		optionsFrame  = FrameContent.Create("Options");
		
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
			if(GUI.Button(new Rect(Screen.width-100,Screen.height-45,60,25),"Quit")){
				Quit();
			}			
		}else{
			if(GUI.Button(new Rect(Screen.width-100,Screen.height-45,60,25),"Back")){
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
	
	public void StartGame(bool localGame){
		activeMenu = ActiveMenu.startGame;
		startGameScreen.localGame = localGame;
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