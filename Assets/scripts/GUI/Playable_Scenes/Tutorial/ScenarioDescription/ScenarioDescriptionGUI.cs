using UnityEngine;
using System.Collections;

public class ScenarioDescriptionGUI{

	private string headerText; //short description which is updated for every new mission
	private string fullText; //the full text contained in the window
	
	public void AddNote(string note){
		//adds a note to the full text
		fullText += note; //add bullet points or something
	}
	
	public void AddMission(string header, string longDescr){
		headerText = header;
		fullText += longDescr;
	}
	
	public void ClearWindow(){
		fullText = "";
		headerText = "";
	}
	
	public void PrintGUI(){
		//draw the actual window here...
	}
	
	public void ShowContinue(bool show){
		
	}
	
	public void ShowFinish(bool show){
		//overrides continue-button
	}
}
