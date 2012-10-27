//using UnityEngine;
//using System.Collections;
//
//public class TutorialButtonRow{
//
//	public bool enable = true;
//	public Rect position;
//	private CheckSolutionButton checkSolution;
//	private UndoButton undo;
//	private StatusField statusField;
//	private IGUIMessages receiver;
//
//	public TutorialButtonRow(IGUIMessages receiver){
//		this.receiver = receiver;		
//		checkSolution = new CheckSolutionButton();
//		undo = new UndoButton(receiver);
//		statusField = new StatusField();
//		float width = 300f;
//		position = new Rect(Screen.width/2-width/2,Screen.height-110,width,40);
//	}
//	
//	public bool PrintGUI(){
//		if(enable){
//			if(Stats.playerController[receiver.GetMainGameState().activePlayer] != Stats.PlayerController.localPlayer){
//				GUI.enabled = false;
//			}
//			GUI.BeginGroup(position);
//			bool check = checkSolution.PrintGUI();
//			undo.PrintGUI();
//			statusField.PrintGUI();
//			GUI.EndGroup();
//			GUI.enabled = true;
//			return check;
//		}
//		return false;
//	}
//	
//	
//}
