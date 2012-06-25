using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GraphicalEffectFactory : MonoBehaviour {
	
	public Transform buildingConstruction;
	public Transform directionalLight;
	
	private Color silenceFlashColor = new Color(0.3f,0.4f,1,1);
	

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
		StartCoroutine("SilenceEnumerator");
		SilenceShake();
	}
	
	private void SilenceShake(){
		Camera.main.animation.Play("SilenceShake");
	}
	
	private IEnumerator SilenceEnumerator(){
		Color origColor = directionalLight.light.color;
		float origIntensity = directionalLight.light.intensity;
		directionalLight.light.color = silenceFlashColor;
		directionalLight.light.intensity = 5f;
		yield return new WaitForSeconds(0.25f);
		directionalLight.light.color = origColor;
		directionalLight.light.intensity = origIntensity;
	}
}
