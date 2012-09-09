using UnityEngine;
using System.Collections;

public struct SkillEnabled{
	public bool shoot;
	public bool build;
	public bool silence;
	public bool skillCap;
	public bool five;
	public bool diagShoot;
	public bool diagBuild;
	public bool diagSilence;
	public bool diagSkillCap;
	public bool diagFive;
	
	public void SetDiag(bool b){
		diagShoot = b;
		diagBuild = b;
		diagSilence = b;
		diagSkillCap = b;
		diagFive = b;
	}
	
	public void SetStraight(bool b){
		shoot = b;
		build = b;
		silence = b;
		skillCap = b;
		five = b;
	}
	public void SetAll(bool b){
		SetDiag(b);
		SetStraight(b);
	}
	
	public static SkillEnabled AllActive(){
		SkillEnabled ret = new SkillEnabled();
		ret.SetAll(true);
		return ret;
	}
	
	public override string ToString(){
		string s = "{";
		s += (shoot ? "1" : "0");
		s += (build ? "1" : "0");
		s += (silence ? "1" : "0");
		s += (skillCap ? "1" : "0");
		s += (five ? "1" : "0");
		s += (diagShoot ? "1" : "0");
		s += (diagBuild ? "1" : "0");
		s += (diagSilence ? "1" : "0");
		s += (diagSkillCap ? "1" : "0");
		s += (diagFive ? "1" : "0");
		s += "}";
		return s;
	}
	public bool ReadFromString(string s){
		if(s.StartsWith("{") && s.EndsWith("}") && s.Length == 12){
			shoot = s[1] == '1';
			build = s[2] == '1';
			silence = s[3] == '1';
			skillCap = s[4] == '1';
			five = s[5] == '1';
			
			diagShoot = s[6] == '1';
			diagBuild = s[7] == '1';
			diagSilence = s[8] == '1';
			diagSkillCap = s[9] == '1';
			diagFive = s[10] == '1';
			return true;
		}else{
			Debug.LogWarning("string: "+s+"; found to be incorrect");
			return false;
		}
	}
}
