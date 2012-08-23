using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SingleFrame : Frame{
	
	private List<MenuButton> buttonList = new List<MenuButton>();
	private Vector2 scrollPosition = Vector2.zero;
	
	public SingleFrame(string title,int buttonHeight, Rect position){
		this.title = title;
		this.position = position;
		this.title = title;
		SetUp (buttonHeight);
	}
	
	public SingleFrame(string title,int buttonHeight, Rect position, int border){
		this.title = title;
		this.position = position;
		this.border = border;
		SetUp (buttonHeight);
		
	}

	private void SetUp(int buttonHeight){
		buttonSize = new Rect(0,0,position.width-2*border,(float)buttonHeight);
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
		GUI.Box(new Rect(0,0,position.width,20),title);
		float height = (buttonSize.height+spacing)*buttonList.Count+border-spacing-20;
		scrollPosition = GUI.BeginScrollView(new Rect(0,20,position.width,position.height-20),scrollPosition,new Rect(0,0,position.width-50,height));
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
			buttonList[i].position = new Rect(border,border +(spacing+buttonSize.height)*i,buttonSize.width,buttonSize.height);
		}
	}
}