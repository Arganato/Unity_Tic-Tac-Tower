using UnityEngine;
using System.Collections;

public class InitGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Control c = (Control)FindObjectOfType(typeof(Control));
		c.StartNewGame();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
