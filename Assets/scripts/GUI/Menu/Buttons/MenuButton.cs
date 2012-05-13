using UnityEngine;
using System.Collections;

public abstract class MenuButton{
	
	public Rect position;
	
	public abstract void ButtonDown();
	
	public abstract string Name();
	
}



