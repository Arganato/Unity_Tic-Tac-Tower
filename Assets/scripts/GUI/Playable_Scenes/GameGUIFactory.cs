using UnityEngine;
using System.Collections;

public class GameGUIFactory{
	// maybe not exactly a factory... 
	// but should contain functions to create and call GUI's with different sets of skills enabled, etc
	

	
	private SkillGUI skillGUI;
	private HeaderBar header;
	private ButtonRow buttonRow;
	
	private ConnectGUI connectGUI;
	private Chat chat;
	
	public bool gameGUIEnabled = true;
	
	public static GameGUIFactory Create(GameGUIOptions options, IGUIMessages messageReceiver){
		GameGUIFactory ret = new GameGUIFactory();
		# if UNITY_WEBPLAYER
			ret.buttonRow = ButtonRow.Create(messageReceiver);
			ret.skillGUI = SkillGUI.Create();
		# elif UNITY_ANDROID
			ret.buttonRow = ButtonRow.CreateAndroid(messageReceiver);
			ret.skillGUI = SkillGUI.CreateAndroid();
		# else
			ret.buttonRow = ButtonRow.Create(messageReceiver);
			ret.skillGUI = SkillGUI.Create();
		# endif
		ret.header = new HeaderBar(messageReceiver, options.makeNetworkGUI);
		
		return ret;
	}
	
	public void PrintGUI(){
		header.PrintGUI();
		if(gameGUIEnabled){
			skillGUI.PrintGUI();
			buttonRow.PrintGUI(); //catches tooltips from skillGUI (must be called after)
		}
		
		Console.PrintWindow();
		PopupMessage.PrintGUI();		
	}
	
}
