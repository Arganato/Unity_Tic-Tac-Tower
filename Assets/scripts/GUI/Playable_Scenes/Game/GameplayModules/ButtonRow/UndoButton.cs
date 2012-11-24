using UnityEngine;
using System.Collections;

public class UndoButton : FlashingButton{
	
	public bool enable = true;
	public Rect position = new Rect(200,0,60,40);
	
	public int[] usedUndoCounter = new int[2];
	
	private IGUIMessages receiver;
	
	
	public UndoButton(IGUIMessages receiver){
		this.receiver = receiver;
	}
	
	public void ResetCounter(){
		usedUndoCounter[0] = 0;
		usedUndoCounter[1] = 0;
	}
	
	public void PrintGUI(){
		if( enable ){
			GUI.backgroundColor = currentColor;
			GUI.BeginGroup(position);
			ColoredBox();
			if( CanUndo() ){
				if(GUI.Button(new Rect(0,0,position.width,position.height/2),"Undo")){
				usedUndoCounter[receiver.GetMainGameState().activePlayer]++;
				receiver.UndoTurn();
				}
			}else{
				GUI.Box(new Rect(0,0,position.width,position.height/2),"Undo");
			}
			GUI.EndGroup();
		}
	}
	
	public void Flash ()
	{
		Flash(receiver.GetMonoBehaviour());
	}
	
	private void ColoredBox(){
		Color old = GUI.contentColor;
		GUI.contentColor = ResourceFactory.GetPlayer1Color();
		GUI.Box(new Rect(0,position.height/2,position.width/2,position.height/2),""+usedUndoCounter[0]);
		GUI.contentColor = ResourceFactory.GetPlayer2Color();
		GUI.Box(new Rect(position.width/2,position.height/2,position.width/2,position.height/2),""+usedUndoCounter[1]);
		GUI.contentColor = old;
	}
	
	private bool CanUndo(){
		return (!receiver.GetMainGameState().skillsUsed.Empty() || receiver.GetMainGameState().playerDone) && Stats.gameRunning;
	}
}
