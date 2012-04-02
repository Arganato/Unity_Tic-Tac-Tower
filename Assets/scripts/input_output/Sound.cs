using UnityEngine;
using System.Collections;

public enum SoundType {background, onClick, error, shoot, build, emp, defeat, victory}

public class Sound : MonoBehaviour {
	
	public AudioClip backgroudMusic;
	public AudioClip onButtonPress;
	public AudioClip onError;
	public AudioClip onShoot;
	public AudioClip onBuild;
	public AudioClip onEmp;
	public AudioClip onDefeat;
	public AudioClip onVictory;
	
	void Start () {

	}
	
	
	public void PlaySound(SoundType id){
		// plays an audio clip after its id
		switch(id){
		case SoundType.background:
			gameObject.audio.PlayOneShot(backgroudMusic);
			break;
		case SoundType.onClick:
			gameObject.audio.PlayOneShot(onButtonPress);
			break;
		case SoundType.build:
			gameObject.audio.PlayOneShot(onButtonPress);
			break;
		case SoundType.shoot:
			gameObject.audio.PlayOneShot(onButtonPress);
			break;
		case SoundType.emp:
			gameObject.audio.PlayOneShot(onButtonPress);
			break;
		case SoundType.error:
			gameObject.audio.PlayOneShot(onButtonPress);
			break;
		case SoundType.victory:
			gameObject.audio.PlayOneShot(onButtonPress);
			break;
		case SoundType.defeat:
			gameObject.audio.PlayOneShot(onButtonPress);
			break;
		}
	}
	
}
