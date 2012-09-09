using UnityEngine;
using System.Collections;

public struct GameGUIOptions{
	public SkillEnabled skillsEnabled;
	public bool makeNetworkGUI;
	
	public static GameGUIOptions Default(){
		GameGUIOptions o = new GameGUIOptions();
		o.skillsEnabled = SkillEnabled.AllActive();
		o.makeNetworkGUI = false;
		return o;
	}
	
	public static GameGUIOptions Create(SkillEnabled skillsEnabled, bool makeNetworkGUI){
		GameGUIOptions ret = new GameGUIOptions();
		ret.skillsEnabled = skillsEnabled;
		ret.makeNetworkGUI = makeNetworkGUI;
		return ret;
	}
}
