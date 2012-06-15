using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIList {

	public Rect position;
	public int choise;
	
	private List<string> elements = new List<string>();
	private int activeElement;
	private bool showList = false;
	
	public GUIList(Rect pos){
		position = pos;
	}
	public GUIList(Rect pos, List<string> elem){
		position = pos;
		elements = elem;
	}
	public void AddElement(string element){
		elements.Add(element);
	}
	public void PrintGUI(){
		if(showList){
			for (int i=0;i<elements.Count;i++){
				if(GUI.Button(new Rect(position.x,position.y+30*i,position.width-30,25),elements[i])){
					choise = i;
					showList = false;
				}
			}
			if(GUI.Button(new Rect(position.x+position.width-25,position.y,25,20),ResourceFactory.GetArrowUp())){
				showList = false;
			}
		}else{
			GUI.Box(new Rect(position.x,position.y,position.width-30,position.height),elements[choise]);
			if(GUI.Button(new Rect(position.x+position.width-25,position.y,25,20),ResourceFactory.GetArrowDown())){
				showList = true;
			}
		}
	}
	
}
