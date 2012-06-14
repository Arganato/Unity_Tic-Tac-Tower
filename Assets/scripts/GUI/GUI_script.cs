using UnityEngine;
using System.Collections;

public class GUI_script : MonoBehaviour {

	private Control control;
	private Grid grid;
	
	public Texture[] tSkills;
	private int showSkillInfo = 0; //0 = Reveals no info.
	
	public GUISkin customSkin;
	
	public bool enable;
	public bool lockGUI;
		
	private bool towerRow; // whether the straight or diagonal towers shall be shown	
		
	private SkillGUI skillGUI = SkillGUI.Create();
	private HeaderBar header;
	private ButtonRow buttonRow;
	
	void Start () {
		control = (Control)FindObjectOfType(typeof(Control));
		grid = (Grid)FindObjectOfType(typeof(Grid));
		buttonRow =  new ButtonRow(control);
		header = new HeaderBar(control);
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
