using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MainMenu : MonoBehaviour {
	
	public GUISkin customSkin;
	public Transform network;
	public Transform soundObject;
	
	private List<MenuContent> menuStack = new List<MenuContent>();
	
	private NetworkInterface nif;
	private Sound sound;
		
	void Start () {
		
		Debug.Log("Screen: h = "+Screen.height+", w = "+Screen.width+", h/w = "+((double)Screen.height/(double)Screen.width));
		Stats.StartUpRoutine(); //should be called once at the beginning of every game (include in loading script or smt)

		nif = (NetworkInterface)FindObjectOfType(typeof(NetworkInterface));
		if(nif == null){
			Instantiate(network);
		}
		sound = (Sound)FindObjectOfType(typeof(Sound));
		if(sound == null)
			Instantiate(soundObject);
		
		
		Frame mainMenuFrame;
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
			if(GUI.Button(new Rect(Screen.width-150,Screen.height-45,150,45),"Quit")){
				Quit();
			}			
		}else{
			if(GUI.Button(new Rect(Screen.width-150,Screen.height-45,150,45),"Back")){
				GoBack();
			}
		}
	}
	
	private void GoBack(){
		menuStack[menuStack.Count-1].Close();
		menuStack.RemoveAt(menuStack.Count-1);
	}
	
	public void AddMenu(MenuContent menu){
		menuStack.Add(menu);
	}
	
	public NetworkInterface FindNetworkInterface(){
		if(nif == null)
			nif = (NetworkInterface)FindObjectOfType(typeof(NetworkInterface));
		return nif;
	}
	public Sound GetSoundScript(){
		if(sound == null)
			sound = (Sound)FindObjectOfType(typeof(Sound));		
		return sound;
	}
	
	
	private void Quit(){
		Application.Quit();
	}

}