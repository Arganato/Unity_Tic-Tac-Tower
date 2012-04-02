using UnityEngine;
using System.Collections;


public class GridUnit : MonoBehaviour {

	public Route type;
	public FieldIndex index;

	void Awake(){
		type = Route.outOfBounds;
	}
}
