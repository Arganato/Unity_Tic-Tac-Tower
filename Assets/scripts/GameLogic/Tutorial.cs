using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	Vector2 scrollPos = new Vector2(0,0);
	public GUIStyle textBox;
	public Transform camera;
	
	private Control control;
	
	//camera positions

	private Vector3 camPreviewPos = new Vector3(-30,150,35);
	private Vector3 camPreviewRot = new Vector3(90,0,0);
	
	private float t1;
	private float t2;
	private int status = 0;
	
	void Start () {
		t1 = 5;
		t2 = 10;
		control = (Control)FindObjectOfType(typeof(Control));
		control.StartNewGame();
		
	}
	
	void Update () {
		if ( status == 0 && Time.time > t1){
			status = 1;
			camera.animation.Play("anim1");
		}else if(status == 1 && Time.time > t2){
			status = 2;
			camera.animation.Play("anim2");
		}
	}
	void OnGUI(){
		PrintTextWindow();		
	}
	

	private void PrintTextWindow(){
		GUI.BeginScrollView(new Rect(0,100,300, 600),scrollPos, new Rect(0,0,300,800));
		GUI.Box(new Rect(0,0,300,800),"this is a scroll text \n\n\n\n\n\n\n this is further down\n\n\n\n\nthis is even further down\n\n this sentence is so long only because I wanted to see if the line is clipped or not. Probably it is clipped, and that would make me kinda sad.",textBox);
		GUI.EndScrollView();
	}

}
