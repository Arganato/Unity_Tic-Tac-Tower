using UnityEngine;
using System.Collections;

public class GameGUIFactory{
	/// contains functions to create and call GUI's with different sets of skills enabled, etc
	
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
			ret.skillGUI = SkillGUI.Create(options.skillsEnabled, messageReceiver);
		# elif UNITY_ANDROID
			ret.buttonRow = ButtonRow.CreateAndroid(messageReceiver);
			ret.skillGUI = SkillGUI.CreateAndroid(options.skillsEnabled, messageReceiver);
		# else
			ret.buttonRow = ButtonRow.Create(messageReceiver);
			ret.skillGUI = SkillGUI.Create(options.skillsEnabled, messageReceiver);
		# endif
		ret.header = new HeaderBar(messageReceiver, options.makeNetworkGUI);
		return ret;
	}
	
	public void SetSkillButtons(SkillEnabled enabledButtons){
		skillGUI.SetSkillButtons(enabledButtons);
	}
	
	public void PrintGUI(){
		header.PrintGUI();
		if(gameGUIEnabled){
			skillGUI.PrintGUI();
			buttonRow.PrintGUI(); //catches tooltips from skillGUI (must be called afterwards)
		}
		
		Console.PrintWindow();
		PopupMessage.PrintGUI();		
	}
	
	public void FlashEndTurnButton(){
		buttonRow.FlashEndTurnButton();
	}
	
	public void FlashUndoButton(){
		buttonRow.FlashUndoButton();
	}
	
	public void FlashSkillButton(int skill){
		if(skill < 0 || skill > 3){
			Debug.LogError("Tried to flash skill "+skill+". Valid range is 0-3");
		}else{
			skillGUI.FlashSkillButton(skill);
		}
		
		
	}
	
	public void FlashHelpButton(){
		skillGUI.FlashHelpButton();
	}
	
}
