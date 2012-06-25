using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MainMenu : MonoBehaviour {
	
	public GUISkin customSkin;
	public Transform network;
	
	private Frame mainMenuFrame;
	private List<MenuContent> menuStack = new List<MenuContent>();
		
	void Start () {
		Stats.StartUpRoutine(); //should be called once at the beginning of every game (include in loading script or smt)

		NetworkInterface nif = (NetworkInterface)FindObjectOfType(typeof(NetworkInterface));
		if(nif == null){
			Instantiate(network);
		}
		
		mainMenuFrame = Frame.Create("Main Menu");

		mainMenuFrame.AddButton(new LocalGameButton(this));
		mainMenuFrame.AddButton(new NetworkedButton(this));
		mainMenuFrame.AddButton(new TutorialButton(this));
		mainMenuFrame.AddButton(new OptionsButton(this));
		
		menuStack.Add(mainMenuFrame);	
	}
	
	void OnGUI () {
		GUI.skin = customSkin;
//		string gameIntro = "This boardgame is inspired by the traditional game of Tic-Tac-Toe, where you can build tetris-like towers to gain strategic advantages. The goal of the game is to build five-in-a-row, or (if no ones does) the player with the highest score wins.";
//		string generalRules = "The towers is at the core of the game. Towers can be built with any rotation and mirroring, straight and diagonal, and the second you make the shape they will be built. If you, build something that can be several towers, you will get them all. Each tower will let you use its skill once (you may save it). The same type of skill can be used as many times as you have skill cap.";
		
		menuStack[menuStack.Count-1].PrintGUI();
		if(menuStack.Count <= 1){
			if(GUI.Button(new Rect(Screen.width-100,Screen.height-45,60,25),"Quit")){
				Quit();
			}			
		}else{
			if(GUI.Button(new Rect(Screen.width-100,Screen.height-35,60,25),"Back")){
				GoBack();
			}
		}
	}
	
	private void GoBack(){
		menuStack.RemoveAt(menuStack.Count-1);
	}
	
	public void AddMenu(MenuContent menu){
		menuStack.Add(menu);
	}
	
	public NetworkInterface FindNetworkInterface(){
		NetworkInterface nif = (NetworkInterface)FindObjectOfType(typeof(NetworkInterface));
		if(nif == null)
			Debug.LogError("No network interface found!");
		return nif;
	}
	
	private void Quit(){
		Application.Quit();
	}

}