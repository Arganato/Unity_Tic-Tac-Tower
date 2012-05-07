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

}
