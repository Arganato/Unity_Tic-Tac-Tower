using UnityEngine;
using System.Collections;

public class GameScene : SceneTemplate {

	public GUISkin customSkin;
	public bool enable;
	public bool lockGUI;
	private GameGUIFactory gui;
	
	protected override void Start () {
		base.Start();
		gui = GameGUIFactory.Create(GameGUIOptions.Create(Stats.networkEnabled), (IGUIMessages)this);
	}
	
	//Overridden IGUIMessages-functions:

	
	//GUI-drawing
	void OnGUI() {
		if(!enable){
			return;
		}
		GUI.enabled = !lockGUI;
		GUI.skin = customSkin;
		
		gui.PrintGUI();

		GUI.enabled = true;
		base.HandleMouseInput();
	}
}