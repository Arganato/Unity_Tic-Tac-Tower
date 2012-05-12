using UnityEngine;
using System.Collections;

public abstract class MenuButton : MonoBehaviour {
	
	public static int buttonWidth = 200;
	public static int buttonHeight = 40;
	public static int border = 10;
	
	public static int b1Start = Screen.height/4;
	public static int b2Start = b1Start + buttonHeight + border;
	public static int b3Start = b2Start + buttonHeight + border;
	public static int b4Start = b3Start + buttonHeight + border;
	public static int backButtonStart = Screen.height - buttonHeight;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}

public class MainMenuButtons : MenuButton {
	
	public NewGame newGame = new NewGame();
	public Multiplayer multiPlayer = new Multiplayer();
	public TutorialMenu tutorial = new TutorialMenu();
	public Options options = new Options();
	
	public void PrintMainMenuButtons(){
		newGame.PrintButton();
		multiPlayer.PrintButton();
		tutorial.PrintButton();
		options.PrintButton();
	}
	
}

//New Game menus
public class NewGame : MainMenuButtons {
	
	public NewGame(){
	}
	public void PrintButton(){
		if(GUI.Button(new Rect(Screen.width/2-buttonWidth/2, b1Start, buttonWidth, buttonHeight), "New Game")){
			//Enter the New Game menu.
		}
	}
}

//Multiplayer menus
public class Multiplayer : MainMenuButtons {
	
	public Multiplayer(){
	}
	public void PrintButton(){
		if(GUI.Button(new Rect(Screen.width/2-buttonWidth/2, b2Start, buttonWidth, buttonHeight), "Multiplayer")){
			//Enter the Multiplayer menu.
		}
	}
}

//Tutorial menus
public class TutorialMenu : MainMenuButtons {
	
	public TutorialBuild tutorialBuild = new TutorialBuild();
	public TutorialShoot tutorialShoot = new TutorialShoot();
	public TutorialSilence tutorialSilence = new TutorialSilence();
	public TutorialSkill tutorialSkill = new TutorialSkill();
	
	public TutorialMenu(){
	}
	
	public void PrintButton(){
		if(GUI.Button(new Rect(Screen.width/2-buttonWidth/2, b3Start, buttonWidth, buttonHeight), "Tutorial")){
			//Enter the Tutorial menu.
		}
	}
	
	public void PrintTutorialMenu(){
	}
}

public class TutorialBuildMenu : Tutorial {
	
	public TutorialBuildMenu(){
	}
}

public class TutorialShootMenu : Tutorial {
	
	public TutorialShootMenu(){
	}
}

public class TutorialSilenceMenu : Tutorial {
	
	public TutorialSilenceMenu(){
	}
}

public class TutorialSkillMenu : Tutorial {
	
	public TutorialSkillMenu(){
	}
}

//Option menus
public class Options : MainMenuButtons {
	
	public Options(){
	}
	public void PrintButton(){
		if(GUI.Button(new Rect(Screen.width/2-buttonWidth/2, b4Start, buttonWidth, buttonHeight), "Options")){
			//Enter the Options menu.
		}
	}
}

