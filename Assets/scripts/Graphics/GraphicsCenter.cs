using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GraphicsCenter : MonoBehaviour {
	
	public Transform buildingConstruction;
	public Transform directionalLight;
	
	private Color silenceFlashColor = Color.blue;
	

	void Start () {
	}
	
	void Update () {
	}
	
	public void BuildingConstructionEffect(List<Tower> towers){			
		if(towers.Count > 0){
			Transform tmp = Instantiate(buildingConstruction) as Transform;
			tmp.GetComponent<ConstructBuildingEffect>().Init(towers);
		}
	}
	
	public void SilenceEffect(){
		
	}
}
