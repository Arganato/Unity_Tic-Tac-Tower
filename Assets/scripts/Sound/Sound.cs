using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {
	
	public static Vector3 position = new Vector3(0f,100f,-10.6f); 
	
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
	
	private float[] volumeNormalizer = new float[] {0.30f, 0.34f, 1f, 0.30f};
	private int[][] mixingTable = new int[][] { new int[]{1,1,1,2,2,3}, new int[]{0,0,0,2,2,3}, new int[]{0,1,3}, new int[]{0,1,2} };
	
	
	void Start () {
		DontDestroyOnLoad(gameObject);
		musicVolume = 1f;
		effectVolume = 1f;
		transform.position = position;
		PlaySong(RandomInt(backgroudMusic.Length));
	}
	
	private void PlaySong(int id){
		audio.PlayOneShot(backgroudMusic[id],musicVolume*volumeNormalizer[id]);
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
