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
			if( backgroudMusic != null)	gameObject.audio.PlayOneShot(backgroudMusic);
			break;
		case SoundType.onClick:
			if( onButtonPress != null)	gameObject.audio.PlayOneShot(onButtonPress);
			break;
		case SoundType.build:
			if( onBuild != null)	gameObject.audio.PlayOneShot(onBuild);
			break;
		case SoundType.shoot:
			if( onShoot != null)	gameObject.audio.PlayOneShot(onShoot);
			break;
		case SoundType.emp:
			if( onEmp != null)	gameObject.audio.PlayOneShot(onEmp);
			break;
		case SoundType.error:
			if( onError != null)	gameObject.audio.PlayOneShot(onError);
			break;
		case SoundType.victory:
			if( onVictory != null)	gameObject.audio.PlayOneShot(onVictory);
			break;
		case SoundType.defeat:
			if( onDefeat != null)	gameObject.audio.PlayOneShot(onDefeat);
			break;
		}
	}
	
}
