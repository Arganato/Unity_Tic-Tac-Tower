using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReplayCenter : MonoBehaviour{

	private List<Turn> turnList = new List<Turn>();
	private bool isPlaying = false;
	
	private float changeTime;
	private float turnDuration = 3f;
	
	private Control control;
	
	void Awake(){
		control = (Control)FindObjectOfType(typeof(Control));
	}
	
	void Update(){ 
		if(isPlaying){
			if(changeTime<Time.time){
				NextTurn();
			}
		}
	}
	
	public void LoadGame(string replayString){
		string[] split =  replayString.Split(new string[] { "\r\n", "\n" }, System.StringSplitOptions.None);
		foreach(string s in split){
			string tmpStr = s;
			if(StringIsTurn(ref tmpStr)){
//				Debug.Log("Accepting: "+tmpStr);
				Turn tmp = Turn.StringToTurn(tmpStr);
				if(tmp != null)
					turnList.Add(tmp);
			}else{
//				Debug.Log("Rejecting: "+s);
			}
		}
		setPlay(false);
	}

	public void NextTurn(){
		control.ExecuteTurn(turnList[2*Control.cState.turn+Control.cState.activePlayer]);
		changeTime = Time.time+turnDuration;
	}
	
	public void PrevTurn(){
		
	}
	public void setPlay(bool playStatus){
		if(playStatus){
			changeTime = Time.time+turnDuration;
		}
		isPlaying = playStatus;
	}

	private int Bool2Int(bool b){
		if(b)
			return 1;
		else
			return 0;
	}
		
	private bool StringIsTurn(ref string s){
		if(s.StartsWith("-")){
			int endTypeCode = s.IndexOf(' ');
			string code = s.Substring(1,endTypeCode-1);
			s = s.Substring(endTypeCode+1);
//			Debug.Log("code: "+code+". The rest: "+s);
			if(code == "t"){
				return true;
			}
		}
		s = "";
		return false;
	}
}
