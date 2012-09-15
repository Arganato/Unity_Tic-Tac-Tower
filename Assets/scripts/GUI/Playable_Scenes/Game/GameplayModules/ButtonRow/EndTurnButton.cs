using UnityEngine;
using System.Collections;

public class EndTurnButton : FlashingButton{

	public Rect position = new Rect(40,0,60,40);
	public bool enable = true;
	
	private IGUIMessages receiver;
	
	public EndTurnButton(IGUIMessages receiver){
		this.receiver = receiver;;
	}
	
	public void PrintGUI(){
		if(enable){
			GUI.backgroundColor = currentColor;
			if(receiver.GetMainGameState().playerDone && Stats.gameRunning && GUI.Button( position, "End\nTurn")){
				receiver.UserEndTurn();
			}else if(!receiver.GetMainGameState().playerDone){
				Color old = GUI.contentColor;
				if(receiver.GetMainGameState().activePlayer == 0)
					GUI.contentColor = Color.red;
				else
					GUI.contentColor = Color.blue;
				GUI.Box(position,"Player "+(receiver.GetMainGameState().activePlayer+1));
				GUI.contentColor=old;
			}
		}
	}
	
	public void Flash(){
		Flash(receiver.GetMonoBehaviour());
	}
}
