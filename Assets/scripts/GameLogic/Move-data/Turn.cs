using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//a turn is a set of (one or more) orders

public class Turn{
	private List<Order> orders;
	private bool valid; // if the turn contains exactly one place-order
	
	public Turn(){
		orders = new List<Order>();
		valid = false;
	}
	
	public Turn(Order o){
		orders = new List<Order>();
		valid = false;
		Add(o);
	}
	public Turn(FieldIndex ind){
		orders = new List<Order>();
		valid = false;
		Order o = Order.Create(ind);
		Add(o);
	}
	
	public static Turn StringToTurn( string str){
		//TODO...
//		Debug.Log("Turn: string recieved: "+str);
		Turn ret = new Turn();
		string[] splitStr = str.Split(';');
		foreach( string s in splitStr){
			ret.Add(Order.StringToOrder(s));	
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

	
	public bool IsValid(){
		return valid;
	}
	
	public bool IsEmpty(){
		return orders.Count == 0;
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
