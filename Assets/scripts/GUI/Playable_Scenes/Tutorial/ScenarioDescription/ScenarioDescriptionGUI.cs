using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScenarioDescriptionGUI : FlashingButton{

	private string headerText; //short description which is updated for every new mission
	private string fullText; //the full text contained in the window
	private List<GUIContent> fullContent =  new List<GUIContent>();
	
	private bool showContinue = false;
	private bool showFinish = false; //overrides continue
	public bool isMaximized = true;
	
	private Rect windowRect = new Rect(Screen.width/2-150,40,300,300);
	private Rect minimizedSize = new Rect(0,0,300,50);
	private Rect maximizedSize = new Rect(0,0,300,300);
	
	private IScenarioDescription receiver;
	
	private Vector2 scrollPos = Vector2.zero;
	private int boxheight = 80;
	
	public ScenarioDescriptionGUI(IScenarioDescription receiver){
		this.receiver = receiver;
	}
	
	public void AddNote(string note){
		//adds a note to the full text
		fullContent.Add(new GUIContent(note));
		scrollPos.y += boxheight;
	}
	
	public void AddPicture(Texture pic){
		fullContent.Add(new GUIContent(pic));
		scrollPos.y += boxheight;
	}
	
	public void AddMission(string header, string longDescr, bool maximize){
		headerText = header;
		fullContent.Add(new GUIContent(longDescr));
		scrollPos.y += boxheight;
		if(maximize)
			Maximize();
	}
	public void AddMission(string header, string longDescr){
		AddMission(header,longDescr,false);
	}
	
	public void ClearWindow(){
		fullContent = new List<GUIContent>();
		headerText = "";
	}
	
	public void PrintGUI(){
		//draw the actual window here...
		if(isMaximized){
			GUI.Window(124,windowRect,MaximizedWindow,"Mission Description");
		}else{
			GUI.Window(123,windowRect,MinimizedWindow,"Mission Description");			
		}
	}
	
	public void ShowContinue(bool show){
		showContinue = show;
	}
	
	public void ShowFinish(bool show){
		// finishbutton at the last stage of the tutorial. Overrides continue-button
		showFinish = show;
	}
	
	public void FlashArrow(MonoBehaviour m){
		Flash(m);
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
		GUI.Label(new Rect(5,20,windowRect.width-5,windowRect.height-20),headerText);
		Color tmp = GUI.backgroundColor;
		GUI.backgroundColor = currentColor;
		if(GUI.Button(new Rect(windowRect.width-25,20,25,20),ResourceFactory.GetArrowUp())){
			Minimize();
		}
		GUI.backgroundColor = tmp;
		scrollPos = GUI.BeginScrollView(new Rect(0,45,windowRect.width,windowRect.height-80),scrollPos,new Rect(0,0,windowRect.width-25,boxheight*fullContent.Count));
		for(int i=0;i<fullContent.Count;i++){
			GUI.Box(new Rect(5,i*boxheight,windowRect.width-25,boxheight),"");
			GUI.Label(new Rect(5,i*boxheight,windowRect.width-25,boxheight),fullContent[i]);
		}
		//GUI.Label(new Rect(5,0,windowRect.width-65,scrollHeight),fullText);
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
		GUI.Label(new Rect(5,20,windowRect.width-5,windowRect.height-20),headerText);
		Color tmp = GUI.backgroundColor;
		GUI.backgroundColor = currentColor;
		if(GUI.Button(new Rect(windowRect.width-25,20,25,20),ResourceFactory.GetArrowDown())){
			Maximize();
		}
		GUI.backgroundColor = tmp;
	}
}
