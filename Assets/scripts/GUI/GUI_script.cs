using UnityEngine;
using System.Collections;

public class GUI_script : MonoBehaviour {

	private Control control;
	private Grid grid;
	
	public GUISkin customSkin;
	
	public bool enable;
	public bool lockGUI;

	private SkillGUI skillGUI;
	private HeaderBar header;
	private ButtonRow buttonRow;
	
	private ConnectGUI connectGUI;
	private Chat chat;

	
	void Start () {
		control = (Control)FindObjectOfType(typeof(Control));
		grid = (Grid)FindObjectOfType(typeof(Grid));
		NetworkInterface netIf = (NetworkInterface)FindObjectOfType(typeof(NetworkInterface));
		# if UNITY_WEBPLAYER
			buttonRow = ButtonRow.Create(control);
			skillGUI = SkillGUI.Create();
		# elif UNITY_ANDROID
			buttonRow = ButtonRow.CreateAndroid(control);
			skillGUI = SkillGUI.CreateAndroid();
		# else
			buttonRow = ButtonRow.Create(control);
			skillGUI = SkillGUI.Create();
		# endif
		header = new HeaderBar(control,netIf);
	}
	
	void OnGUI() {
		if(!enable){
			return;
		}
		GUI.enabled = !lockGUI;
		GUI.skin = customSkin;

		header.PrintGUI();
		skillGUI.PrintGUI();
		buttonRow.PrintGUI(); //catches tooltips from skillGUI (must be called after)

		Console.PrintWindow();
		PopupMessage.PrintGUI();
		
		//----Framework to handle mouse-input etc----//
		GUI.enabled = true;

		if(Event.current.type == EventType.MouseDown){
			if( Event.current.type != EventType.Used ){
				grid.MouseDown(Input.mousePosition);
			}
		}
	}
	

}
