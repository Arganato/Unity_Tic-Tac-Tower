using UnityEngine;
using System.Collections;


public struct Order{
	public SkillType skill;
	public FieldIndex position;
	public bool endTurn;
	
	public override string ToString ()
	{
		string s = "";
		switch(skill){
		case SkillType.noSkill:
			return "no_skill";
		case SkillType.place:
			s = "place "+ position.ToString(); 
			break;
		case SkillType.build:
			s = "build "+ position.ToString(); 
			break;
		case SkillType.shoot:
			s = "shoot "+ position.ToString(); 
			break;
		case SkillType.emp:
			s = "silence"; 
			break;
		}
		if(endTurn){
			s += ".";
		}else{
			s += ";";
		}
		return s;
	}
	
		public static Order StringToOrder( string str){
//		Debug.Log("string to order: "+str);
		Order ret = new Order();
		ret.skill = SkillType.noSkill; //default
		ret.endTurn = false;
		string[] splitStr = str.Split(' ');
		if(splitStr[0] == "place"){
			ret.skill = SkillType.place;
		}else if(splitStr[0] == "shoot"){
			ret.skill = SkillType.shoot;
		}else if(splitStr[0] == "build"){
			ret.skill = SkillType.build;
		}else if(splitStr[0] == "silence" || splitStr[0] == "silence."){
			ret.skill = SkillType.emp;
		}
		if(splitStr.Length > 1){ //it contains a field index
			string fStr = splitStr[1];
			int startID = fStr.IndexOf('(');
			int endID = fStr.IndexOf(')');
			int comma = fStr.IndexOf(',');
//			Debug.Log("start: "+startID+" comma: "+comma+" end: "+endID);
//			Debug.Log("str1: "+fStr.Substring(startID+1,comma-startID-1)+" str2: "+fStr.Substring(comma+1,endID-comma-1));
			int id1 = System.Convert.ToInt32(fStr.Substring(startID+1,comma-startID-1));
			int id2 = System.Convert.ToInt32(fStr.Substring(comma+1,endID-comma-1));
			ret.position = new FieldIndex(id1,id2);
		}
		if(splitStr[splitStr.Length-1].EndsWith(".")){
			ret.endTurn = true;
		}
		return ret;
	}

}
