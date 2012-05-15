using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConstructBuildingEffect : MonoBehaviour {

	public float duration = 2f;
	public float startIntensity = 0f;
	public float endIntensity = 0.25f;

	public float endTime;
	public Transform lightTransform;
	public bool lightsEnabled = false;
	
	private Color buildColor = Color.cyan;
	private Color shootColor = Color.magenta;
	private Color silenceColor = Color.green;
	private Color skillCapColor = Color.yellow;
	
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
				SelfDestruct();
			}
		}
	}
	
	private void SelfDestruct(){
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
//		for(int i=0; i<Stats.totalArea;i++){
//			if(cluster[i]){
//				Transform tmp = Instantiate(lightTransform,Grid.BoardToWorldPoint(new FieldIndex(i)),Quaternion.identity) as Transform;
//				lights[i] = tmp.GetComponent<Light>();
//			}else{
//				lights[i] = null;	
//			}
//		}
		foreach( Tower t in cluster){
			foreach(FieldIndex i in t.GetList()){
				Transform tmp = Instantiate(lightTransform,Grid.BoardToWorldPoint(i),Quaternion.identity) as Transform;
				Light aLight = tmp.GetComponent<Light>();
				aLight.color = GetColor(t.type);
				lights.Add(aLight);
			}
		}
	}
	
	private Color GetColor(TowerType t){
		
		switch(t){
		case TowerType.shoot:
			return shootColor;
		case TowerType.build:
			return buildColor;
		case TowerType.emp:
			return silenceColor;
		case TowerType.square:
			return skillCapColor;
		default:
			return Color.white;
		}
	}
}
