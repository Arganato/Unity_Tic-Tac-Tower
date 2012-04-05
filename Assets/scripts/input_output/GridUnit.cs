using UnityEngine;
using System.Collections;


public class GridUnit : MonoBehaviour {

	public Route type;
	public FieldIndex index;
	
	public Vector3 speed;
	public float speedY;
	public float speedZ;

	void Awake(){
		type = Route.outOfBounds;
		speed = Random.onUnitSphere/4;
	}
	
	void Update(){
		transform.Rotate(speed);
	}
		
}
