using UnityEngine;
using System.Collections;

public abstract class OptionsMenu : MenuContent {
	
	

	public static OptionsMenu Create(MainMenu m){
		# if UNITY_WEBPLAYER
			return new OptionsPC(m);
		# elif UNITY_ANDROID
			return new OptionsAndroid(m);
		# else
			return new OptionsAndroid(m);
		# endif		
	}
}
