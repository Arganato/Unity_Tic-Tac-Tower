using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuFrame{
	
	public Rect buttonSize = new Rect(0,0,300,100);
	public Rect position = new Rect(200,100,400,800);
	public int border = 20;
	
	private int spacing = 200; //to be made dynamic
	private List<MenuButton> buttonList = new List<MenuButton>();
	private Vector2 scrollPosition = Vector2.zero;
	
	
	public void PrintGUI(){
		GUI.BeginGroup(position);
		scrollPosition = GUI.BeginScrollView(new Rect(0,0,position.width,position.height),scrollPosition,new Rect(0,0,position.width-50,position.height));
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
		button.position = new Rect(border,spacing*buttonList.Count,buttonSize.width,buttonSize.height);
			
	}
	
	
}