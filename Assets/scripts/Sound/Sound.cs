using UnityEngine;
using System.Collections;

public enum SoundType {onClick, error, undo, shoot, build, silence, newTower, defeat, victory}

public class Sound : MonoBehaviour {
	
	public AudioClip[] backgroudMusic;
	public AudioClip onButtonPress;
	public AudioClip onError;
	public AudioClip onUndo;
	public AudioClip onShoot;
	public AudioClip onBuild;
	public AudioClip onSilence;
	public AudioClip onNewTower;
	public AudioClip onDefeat;
	public AudioClip onVictory;
	
	public float musicVolume = 1f;
	public float effectVolume = 1f;
	
	private float musicPause = 0; //a pause between each new song. A negative pause would mean that they overlap
	
	private int[][] mixingTable = new int[][] { new int[]{0,1,2}, new int[]{1,2}, new int[]{0,1,2} };
	
	
	void Start () {
		musicVolume = 1f;
		effectVolume = 1f;
		PlaySong(RandomInt(backgroudMusic.Length));
	}
	
	private void PlaySong(int id){
		audio.PlayOneShot(backgroudMusic[id],musicVolume);
		StartCoroutine("WaitForSong",id);
	}
	
	private int GetNextSong(int id){
		int arrayID = RandomInt(mixingTable[id].Length);
		return mixingTable[id][arrayID];
	}
	
	private IEnumerator WaitForSong(int id){
		float wait = backgroudMusic[id].length + musicPause;
		yield return new WaitForSeconds(wait);
		PlaySong(GetNextSong(id));
	}
	
	private int RandomInt(int max){
		return (int)(Random.value*max);
	}
	
	public void PlayEffect(SoundType id){
		// plays an audio clip after its id
		switch(id){
		case SoundType.onClick:
			if( onButtonPress != null)	audio.PlayOneShot(onButtonPress,effectVolume);
			break;
		case SoundType.error:
			if( onError != null)	audio.PlayOneShot(onError, effectVolume);
			break;
		case SoundType.undo:
			if( onUndo != null)		audio.PlayOneShot(onUndo, effectVolume);
			break;
		case SoundType.build:
			if( onBuild != null)	audio.PlayOneShot(onBuild, effectVolume);
			break;
		case SoundType.shoot:
			if( onShoot != null)	audio.PlayOneShot(onShoot, effectVolume);
			break;
		case SoundType.silence:
			if( onSilence != null)	audio.PlayOneShot(onSilence, effectVolume);
			break;
		case SoundType.newTower:
			if( onNewTower != null)	audio.PlayOneShot(onNewTower,effectVolume);
			break;
		case SoundType.victory:
			if( onVictory != null)	audio.PlayOneShot(onVictory, effectVolume);
			break;
		case SoundType.defeat:
			if( onDefeat != null)	audio.PlayOneShot(onDefeat, effectVolume);
			break;
		}
	}
	
}
