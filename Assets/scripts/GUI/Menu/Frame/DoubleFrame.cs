using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoubleFrame : Frame{


	private List<MenuButton> buttonList = new List<MenuButton>();
	private Vector2 scrollPosition = Vector2.zero;
	
	
	public DoubleFrame (string title,int buttonHeight, Rect position){
		this.title = title;
		this.position = position;
		this.title = title;
		SetUp (buttonHeight);
	}
	
	public DoubleFrame (string title,int buttonHeight, Rect position, int border){
		this.title = title;
		this.position = position;
		this.border = border;
		SetUp (buttonHeight);
		
	}

	private void SetUp(int buttonHeight){
		buttonSize = new Rect(0,0,position.width/2-2*border,(float)buttonHeight);
		SetSpacing();
	}
	
	
	private void SetSpacing(){
		if(buttonList.Count == 0){
			spacing = maxSpacing;
		}else{
			int height = buttonList.Count*(int)buttonSize.height;
			spacing = ((int)position.height-height-border)/buttonList.Count;
			if(spacing > maxSpacing)
				spacing = maxSpacing;
			if(spacing < minSpacing)
				spacing = minSpacing;
		}
	}
	
	public override void Close (){}


	public override void PrintGUI(){
		GUI.BeginGroup(position);
		GUI.Box(new Rect(0,0,position.width/2,20),title);
		float height = spacing*buttonList.Count+border-20;
		scrollPosition = GUI.BeginScrollView(new Rect(0,20,position.width/2,position.height-20),scrollPosition,new Rect(0,0,position.width/2-50,height));
		foreach(MenuButton button in buttonList){
			if( GUI.Button(button.position,button.Name()) ){
				button.ButtonDown();
			}
		}
		GUI.EndScrollView();
		GUI.EndGroup();
	}
	
	public override void AddButton( MenuButton button){
		buttonList.Add(button);
		SetSpacing();
		for(int i=0; i<buttonList.Count; i++){
			buttonList[i].position = new Rect(border,spacing*(i)+border,buttonSize.width,buttonSize.height);
		}
	}
}