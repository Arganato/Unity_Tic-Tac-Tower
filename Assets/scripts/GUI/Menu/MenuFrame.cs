using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuFrame{
	
	private Rect buttonSize = new Rect(0,0,300,100);
	private Rect position = new Rect(200,100,400,800);
	private int border = 20;
	
	private int spacing = 200; 
	private int maxSpacing = 200;
	private int minSpacing = 50;
	
	private List<MenuButton> buttonList = new List<MenuButton>();
	private Vector2 scrollPosition = Vector2.zero;
	
	public MenuFrame(int buttonHeight, Rect position){
		this.position = position;
		SetUp (buttonHeight);
	}
	
	public MenuFrame(int buttonHeight, Rect position, int border){
		this.position = position;
		this.border = border;
		SetUp (buttonHeight);
		
	}

	private void SetUp(int buttonHeight){
		buttonSize = new Rect(0,0,position.width-border,(float)buttonHeight);
		SetSpacing();
	}
	
	
	private void SetSpacing(){
		int height = buttonList.Count*(int)buttonSize.height;
		spacing = ((int)position.height-height-border)/buttonList.Count;
		if(spacing > maxSpacing)
			spacing = maxSpacing;
		if(spacing < minSpacing)
			spacing = minSpacing;
		
	}
	
	
	public void PrintGUI(){
		GUI.BeginGroup(position);
		float height = (buttonSize.height+spacing)*buttonList.Count+border;
		scrollPosition = GUI.BeginScrollView(new Rect(0,0,position.width,position.height),scrollPosition,new Rect(0,0,position.width-50,height));
		foreach(MenuButton button in buttonList){
			if( GUI.Button(button.position,button.Name()) ){
				button.ButtonDown();
			}
		}
		GUI.EndScrollView();
		GUI.EndGroup();
	}
	
	public void AddButton( MenuButton button){
		buttonList.Add(button);
		SetSpacing();
		for(int i=0; i<buttonList.Count; i++){
			buttonList[i].position = new Rect(border,spacing*(i+1),buttonSize.width,buttonSize.height);
		}
	}
}