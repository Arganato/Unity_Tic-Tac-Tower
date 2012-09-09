using UnityEngine;
using System.Collections;

public class ReplayGUI : MonoBehaviour {
	
	public GUISkin customSkin;
	
	private ConfirmMenu quitMenu = new ConfirmMenu("Quit Replay");
//	private PlayerInfoText info = new PlayerInfoText();
	private ReplayButtons replayButtons = new ReplayButtons();
	private SupplyTextWindow textwindow = new SupplyTextWindow();
	
	private ReplayCenter replayCenter;
	
	// Use this for initialization
	void Start () {
	replayCenter = (ReplayCenter)FindObjectOfType(typeof(ReplayCenter));
		replayButtons.enabled = false;
	}
	
	// Update is called once per frame
	void OnGUI () {
	
		GUI.skin = customSkin;
		
		if( quitMenu.PrintGUI() ){
			Application.LoadLevel("mainMenu");
		}
		
//		info.PrintGUI();
	
		switch(replayButtons.PrintGUI()){
		case ReplayButtons.UserAction.BACK:
			//no worky yet
			break;
		case ReplayButtons.UserAction.FORWARD:
			replayCenter.NextTurn();
			break;
		case ReplayButtons.UserAction.PAUSE:
			replayCenter.setPlay(false);
			break;
		case ReplayButtons.UserAction.PLAY:
			replayCenter.setPlay(true);
			break;
		case ReplayButtons.UserAction.NO_ACTION:
			break;
		}
		
		if(textwindow.PrintGUI()){
			replayCenter.LoadGame(textwindow.GetText());
			textwindow.enable = false;
			replayButtons.enabled = true;
		}
	}
	

}
