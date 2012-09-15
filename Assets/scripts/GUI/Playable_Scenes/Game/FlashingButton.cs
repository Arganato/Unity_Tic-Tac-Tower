using UnityEngine;
using System.Collections;

public abstract class FlashingButton{
	
	private Color flashColor = Color.blue;
	protected Color currentColor = GUI.backgroundColor;
	
	protected void Flash(MonoBehaviour m){
		m.StartCoroutine(DoFlash());
	}
	
	private IEnumerator DoFlash(){
		Color tmpColor = GUI.backgroundColor;
		currentColor = flashColor;
		yield return new WaitForSeconds(1f);
		currentColor = tmpColor;		
	} 
}
