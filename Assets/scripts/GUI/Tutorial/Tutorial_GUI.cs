using UnityEngine;
using System.Collections;

public class Tutorial_GUI : MonoBehaviour {
	
	private Control control;
	private Grid grid;
	
	public GUISkin customSkin;
	
	public bool enable;
	public bool lockGUI;

	private SkillGUI skillGUI = SkillGUI.TutorialCreate();
	private TutorialHeader header;
	private TutorialButtonRow buttonRow;
	
	// Use this for initialization
	void Start () {
		control = (Control)FindObjectOfType(typeof(Control));
		grid = (Grid)FindObjectOfType(typeof(Grid));
		buttonRow =  new TutorialButtonRow(control);
		header = new TutorialHeader();
	}

	void OnGUI() {
		if(!enable){
			return;
		}
		GUI.enabled = !lockGUI;
		GUI.skin = customSkin;

		header.PrintGUI();
		skillGUI.PrintGUI();
		buttonRow.PrintGUI();

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
