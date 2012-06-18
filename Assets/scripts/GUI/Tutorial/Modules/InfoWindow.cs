using UnityEngine;
using System.Collections;

public class InfoWindow : MonoBehaviour {
	
	private Control control;
	private Grid grid;
	
	public GUISkin customSkin;
	
	public bool enable;
	public bool lockGUI;

	//private SkillGUI skillGUI = SkillGUI.Create();
	private TutorialHeader header;
	private ButtonRow buttonRow;
	
	void Start () {
		control = (Control)FindObjectOfType(typeof(Control));
		grid = (Grid)FindObjectOfType(typeof(Grid));
		buttonRow =  new ButtonRow(control);
		header = new TutorialHeader();
	}
	
	void OnGUI() {
		if(!enable){
			return;
		}
		GUI.enabled = !lockGUI;
		GUI.skin = customSkin;

		//header.PrintGUI(tower);	//Denne skal brukes.
		
		//skillGUI.PrintGUI();
		buttonRow.PrintGUI();
		
		//Console.PrintWindow();
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
