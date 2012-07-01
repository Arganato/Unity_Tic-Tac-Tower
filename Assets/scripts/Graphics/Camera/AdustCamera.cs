using UnityEngine;
using System.Collections;

public class AdustCamera : MonoBehaviour {
	
	private float sizeFactor = 45.5f; //the relation between screen aspect ratio and orthographic size of the camera to make the field fit

	//the relation between aspect ratio and camera z position to keep the field on the top of the screen (non-porportional):
	private float height_a = -61.25f; 
	private float height_b = 81.275f;
	
	
	void Start(){
		if( camera == null){
			Debug.LogError("No camera found!");
			return;
		}
		float ratio = (float)Screen.height/(float)Screen.width;
		if( ratio > 1.5f){
			camera.orthographicSize = sizeFactor*ratio;
			Vector3 camPos = camera.transform.position;
			camPos.z = height_b+height_a*ratio;
			camera.transform.position = camPos;
		}
	}

}
