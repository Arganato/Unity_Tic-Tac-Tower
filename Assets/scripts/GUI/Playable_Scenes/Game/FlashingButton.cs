using UnityEngine;
using System.Collections;

public abstract class FlashingButton{
	//See tutorial for how to use at /Documentation/FlashingGuiTutorial.txt
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
		yield return new WaitForSeconds(0.5f);
		currentColor = flashColor;
		yield return new WaitForSeconds(0.5f);
		currentColor = tmpColor;	
		yield return new WaitForSeconds(0.5f);
		currentColor = flashColor;
		yield return new WaitForSeconds(0.5f);
		currentColor = tmpColor;	
		yield return new WaitForSeconds(0.5f);
		currentColor = flashColor;
		yield return new WaitForSeconds(0.5f);
		currentColor = tmpColor;	
	} 
}
