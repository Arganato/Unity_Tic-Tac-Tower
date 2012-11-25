using UnityEngine;
using System.Collections;

public class AdustCamera : MonoBehaviour {
	
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
	}

}
