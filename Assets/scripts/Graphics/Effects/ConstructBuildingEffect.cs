using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConstructBuildingEffect : MonoBehaviour {
	
	//public variables overridden by prefab
	public float duration; 
	public float startIntensity;
	public float endIntensity; //8 is max
	public Transform lightTransform;
	
	private bool lightsEnabled = false;
	private float endTime;
	
	private List<Tower> cluster;
	
	private List<Light> lights;

	
	
	public void Init(List<Tower> tow){
		this.cluster = tow;
		endTime = Time.time+duration;
		lights = new List<Light>();
		MakeLights();
		lightsEnabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (lightsEnabled){
			IncreaseIntensity();
			if(Time.time > endTime){
				DestroyLights();
			}
		}
	}
	
	private void DestroyLights(){
		foreach(Light l in lights){
			Destroy(l.gameObject);
		}
		Destroy(gameObject);		
	}
	
	private void IncreaseIntensity(){
		foreach(Light l in lights){
			l.intensity += Time.deltaTime*(endIntensity - startIntensity)/duration;				
		}
	}
	

	private void MakeLights(){
		lightTransform.light.intensity = startIntensity;

		foreach( Tower t in cluster){
			foreach(FieldIndex i in t.GetList()){
				Vector3 pos = Grid.BoardToWorldPoint(i);
				pos.y += 5;
				Transform tmp = Instantiate(lightTransform,pos,Quaternion.identity) as Transform;
				Light aLight = tmp.GetComponent<Light>();
				aLight.color = ResourceFactory.GetSkillColor(t.type);
				lights.Add(aLight);
			}
		}
	}
	
}
