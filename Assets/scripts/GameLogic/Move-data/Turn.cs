using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//a turn is a set of (one or more) orders

public class Turn{
	private List<Order> orders;
	private bool valid; // if the turn contains exactly one place-order
	
	public static Turn StringToTurn( string str){
		//TODO...
		Turn ret = new Turn();
		string[] splitStr = str.Split(';');
		foreach( string s in splitStr){
			ret.Add(stringToOrder(s));	
		}
		return ret;
	}
	private static Order stringToOrder( string str){
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
		}else if(splitStr[0] == "silence"){
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
	
	public override string ToString ()
	{
		string s = "";
		foreach(Order o in orders){
			s += o.ToString();
		}
		return s;
	}
	public Turn(){
		orders = new List<Order>();
		valid = false;
	}
	
	public bool IsValid(){
		return valid;
	}
	
	public void Add(Order o){
		if(valid && o.skill == SkillType.place){
			Debug.LogError("Turn already has a place-order: Only one place-order allowed each turn"); 
			return;
		}
		if(o.skill != SkillType.noSkill){
			orders.Add(o);
		}else if(o.endTurn){
			if(valid){
				Order tmp = orders[orders.Count-1];
				tmp.endTurn = true;
				orders[orders.Count-1] = tmp;
			}
		}
		if(o.skill == SkillType.place){
			valid = true;
		}
	}
	
	public List<Order> GetOrderList(){
		return orders;
	}
	
}
