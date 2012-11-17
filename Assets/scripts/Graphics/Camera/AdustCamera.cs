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
		
		float boardPixHeight = Screen.height-150; //the size of the board on-screen (in pixels)
		if(Screen.width < boardPixHeight){
			boardPixHeight = Screen.width;
		}
		float screenBoardPercentage = boardPixHeight/Screen.height;
		//the screen height in world coords is camera.orthographicSize*2:
		camera.orthographicSize = Stats.fieldSize*10/screenBoardPercentage/2;
		//Debug.Log("BoardP = "+screenBoardPercentage+", ortSize = "+camera.orthographicSize+".");
		
		float center = (Stats.fieldSize*10-90)/2;
		Vector3 newPos = camera.transform.position;
		newPos.x = center;
		newPos.z = center -11.6f; //(Screen.height/2-110-(Screen.height-110)/2)*camera.orthographicSize*2/Screen.height;
		//Debug.Log("new position = "+newPos);
		camera.transform.position = newPos;
//		float ratio = (float)Screen.height/(float)Screen.width;
//		if( ratio > 1.5f){
//			camera.orthographicSize = sizeFactor*ratio;
//			Vector3 camPos = camera.transform.position;
//			camPos.z = height_b+height_a*ratio;
//			camera.transform.position = camPos;
//		}
	}

}
