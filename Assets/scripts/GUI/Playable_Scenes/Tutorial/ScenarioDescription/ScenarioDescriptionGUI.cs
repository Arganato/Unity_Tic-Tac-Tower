using UnityEngine;
using System.Collections;

public class ScenarioDescriptionGUI{

	private string headerText; //short description which is updated for every new mission
	private string fullText; //the full text contained in the window
	
	private bool showContinue = false;
	private bool showFinish = false; //overrides continue
	private bool isMaximized = true;
	
	private Rect windowRect = new Rect(Screen.width/2-150,40,300,300);
	private Rect minimizedSize = new Rect(0,0,300,50);
	private Rect maximizedSize = new Rect(0,0,300,300);
	
	private IScenarioDescription receiver;
	
	private Vector2 scrollPos = Vector2.zero;
	private int scrollHeight = 20;
	
	public ScenarioDescriptionGUI(IScenarioDescription receiver){
		this.receiver = receiver;
	}
	
	public void AddNote(string note){
		//adds a note to the full text
		fullText += note+"\n"; //add bullet points or something
		scrollHeight += 30;
	}
	
	public void AddMission(string header, string longDescr){
		headerText = header;
		fullText += longDescr + '\n';
		scrollHeight += 30;
	}
	
	public void ClearWindow(){
		fullText = "";
		headerText = "";
		scrollHeight = 20;
	}
	
	public void PrintGUI(){
		//draw the actual window here...
		if(isMaximized){
			GUI.Window(6,windowRect,MaximizedWindow,"Mission Description");
		}else{
			GUI.Window(6,windowRect,MinimizedWindow,"Mission Description");			
		}
	}
	
	public void ShowContinue(bool show){
		showContinue = show;
	}
	
	public void ShowFinish(bool show){
		// finishbutton at the last stage of the tutorial. Overrides continue-button
		showFinish = show;
	}
	
	public void Maximize(){
		windowRect.width = maximizedSize.width;
		windowRect.height = maximizedSize.height;
		isMaximized = true;
	}
	
	public void Minimize(){
		windowRect.width = minimizedSize.width;
		windowRect.height = minimizedSize.height;
		isMaximized = false;
	}
	
	private void MaximizedWindow(int windowID){
		if(GUI.Button(new Rect(windowRect.width-25,20,25,20),ResourceFactory.GetArrowUp())){
			Minimize();
		}
		GUI.BeginScrollView(new Rect(0,45,windowRect.width,windowRect.height-6),scrollPos,new Rect(0,0,windowRect.width-60,scrollHeight));
		GUI.Label(new Rect(0,45,windowRect.width-60,scrollHeight),fullText);
		GUI.EndScrollView();
		if(showFinish){
			if(GUI.Button(new Rect(windowRect.width-150,windowRect.height-25,150,25),"Finish")){
				receiver.OnFinished();
			}			
		}else if(showContinue){
			if(GUI.Button(new Rect(windowRect.width-150,windowRect.height-25,150,25),"Continue")){
				receiver.OnContinue();
			}
		}
	}
	
	private void MinimizedWindow(int windowID){
		GUI.Label(new Rect(0,20,windowRect.width,windowRect.height-20),headerText);
		if(GUI.Button(new Rect(windowRect.width-25,windowRect.height-20,25,20),ResourceFactory.GetArrowDown())){
			Maximize();
		}	
	}
}
