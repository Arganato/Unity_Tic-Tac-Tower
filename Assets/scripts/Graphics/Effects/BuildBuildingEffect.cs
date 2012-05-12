using UnityEngine;
using System.Collections;

public class BuildBuildingEffect : MonoBehaviour {

	public float duration = 2f;
	public float startIntensity = 0f;
	public float endIntensity = 0.25f;

	public float endTime;
	public Transform lightTransform;
	public bool lightsEnabled = false;
	
	private Field<bool> cluster;
	
	private Field<Light> lights;

	
	
	public void Init(Field<bool> cluster){
		this.cluster = cluster;
		endTime = Time.time+duration;
		lights = new Field<UnityEngine.Light>();
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
		for(int i=0; i<Stats.totalArea;i++){
			
			if(lights[i] != null){
				Destroy(lights[i].gameObject);
			}
		}
		Destroy(gameObject);		
	}
	
	private void IncreaseIntensity(){
		for(int i=0; i<Stats.totalArea;i++){
			if(lights[i] != null){
				lights[i].intensity += Time.deltaTime*(endIntensity - startIntensity)/duration;
				
			}
		}
	}
	

	private void MakeLights(){
		lightTransform.light.intensity = startIntensity;
		for(int i=0; i<Stats.totalArea;i++){
			if(cluster[i]){
				Transform tmp = Instantiate(lightTransform,Grid.BoardToWorldPoint(new FieldIndex(i)),Quaternion.identity) as Transform;
				lights[i] = tmp.GetComponent<Light>();
			}else{
				lights[i] = null;	
			}
		}
	}
}
