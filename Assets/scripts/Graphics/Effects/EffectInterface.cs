using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface EffectInterface {

	void PlaySound(SoundType type);
	
	void PlayBuildingConstructionEffect(List<Tower> towersCreated);
	
	void PlaySilenceEffect();
}
