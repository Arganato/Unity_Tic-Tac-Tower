using UnityEngine;
using System.Collections;

public class OptionsGUI : MenuContent {

	
	public Rect position = new Rect(0,0,300,440);
	
	private bool settingsSaved = false;
	
	private float masterVolume = 1f;
	private float musicVolume = 1f;
	private float effectVolume = 1f;
	private bool muteMusic = false;
	private string targetFrameRateString = "15";
	
	private Sound sound;
	
	public OptionsGUI(MainMenu m){
		sound = m.GetSoundScript();
		SetUpScreen();
		SetupValues();
	}
	
	private void  SetUpScreen(){
		if( Screen.width >= 300){
			position.x = Screen.width/2 - position.width/2;
		}else{
			position.x = 0; 
			position.width = Screen.width;
		}if(Screen.height >= 480){
			position.y = Screen.height/2+20 - position.height/2;
		}else{
			position.y = 0;
			position.height = Screen.height-40;
		}
	}
	
	public override void Close (){}
	
	public override void PrintGUI (){
		GUILayout.BeginArea(position);
		if(settingsSaved){
			GUILayout.Label("Settings saved...",GUILayout.Height(25));
		}else{
			GUILayout.Space(25);
		}
		GUILayout.FlexibleSpace();
		Sound();
		GUILayout.FlexibleSpace();
		TargetFramerate();
		
		//-----save and cancel-----//
		GUILayout.FlexibleSpace();
		GUILayout.BeginHorizontal(GUILayout.Height(25));
		if(GUILayout.Button("Save")){
			SaveSettings();
		}
		if(GUILayout.Button("Restore default")){
			SetDeafult();
		}
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}
	
	private void Sound(){
		GUILayout.BeginVertical("","box");
		GUILayout.Label("Sound");
		muteMusic = GUILayout.Toggle(muteMusic,"Mute");
		GUILayout.BeginHorizontal();
		GUILayout.Label("Master volume");
		masterVolume = GUILayout.HorizontalSlider(masterVolume,0f,1f);
		GUILayout.EndHorizontal();		
		GUILayout.BeginHorizontal();
		GUILayout.Label("Music volume");
		musicVolume = GUILayout.HorizontalSlider(musicVolume,0f,1f);
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		GUILayout.Label("Effect volume");
		effectVolume = GUILayout.HorizontalSlider(effectVolume,0f,1f);
		GUILayout.EndHorizontal();
		GUILayout.EndVertical();
	}
	
	private void TargetFramerate(){
		GUILayout.BeginVertical("","box");
		GUILayout.BeginHorizontal();
		GUILayout.Label("Target Framerate");
		targetFrameRateString = GUILayout.TextField(targetFrameRateString,GUILayout.Width(40));
		GUILayout.EndHorizontal();
		GUILayout.Label("A higher framerate takes more CPU. Too low framerate makes the game choppy");
		GUILayout.EndVertical();
	}
	
	private void SaveSettings(){
		int framerateInt = System.Convert.ToInt32(targetFrameRateString);
		if(framerateInt >= 5 && framerateInt <= 60){
			Application.targetFrameRate = framerateInt;
		}else if(framerateInt < 5){
			Application.targetFrameRate = 5;
		}else{
			Application.targetFrameRate = 60;
		}
		
		//Sound...
		sound.audio.volume = masterVolume;
		sound.effectVolume = effectVolume;
		sound.musicVolume = musicVolume;
		sound.audio.mute = muteMusic; 
		
		SetupValues();
		settingsSaved = true;
	}
	
	private void SetupValues(){
		targetFrameRateString = System.Convert.ToString(Application.targetFrameRate);
		masterVolume = sound.audio.volume;
		effectVolume = sound.effectVolume;
		musicVolume = sound.musicVolume;
		muteMusic = sound.audio.mute;
	}
	
	private void SetDeafult(){
		Application.targetFrameRate = 15;
		sound.audio.volume = 1f;
		sound.effectVolume = 1f;
		sound.musicVolume = 1f;
		sound.audio.mute = false; 
		SetupValues();
	}
	
	
}
