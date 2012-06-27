using UnityEngine;
using System.Collections;

public class Tutorial_GUI : MonoBehaviour {
	
	private Control control;
	private Grid grid;
	
	public GUISkin customSkin;
	
	public bool enable;

	private SkillGUI skillGUI = SkillGUI.TutorialCreate();
	private TutorialButtonRow buttonRow;
	
	// Use this for initialization
	void Start () {
		control = (Control)FindObjectOfType(typeof(Control));
		grid = (Grid)FindObjectOfType(typeof(Grid));
		buttonRow =  new TutorialButtonRow(control);
	}

	void OnGUI() {
		if(!enable){
			return;
		}
		GUI.skin = customSkin;

		if(Tutorial.chapter == Tutorial.Chapter.tutStr || Tutorial.chapter == Tutorial.Chapter.tutDiag){
			skillGUI.PrintGUI();
		}
		buttonRow.PrintGUI();

		PopupMessage.PrintGUI();
		
		//----Framework to handle mouse-input etc----//

		if(Event.current.type == EventType.MouseDown){
			if( Event.current.type != EventType.Used ){
				grid.MouseDown(Input.mousePosition);
				print("sending mouse click");
			}else{
				print("not sending mouse click");
			}
		}
	}
}
