using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//Contains:
//-Shape
//-Tower
//-SkillContainer
public enum TowerType {build, shoot, emp, square, five};

public struct Shape {
	
	public int dir;
	
	public FieldIndex Forward( FieldIndex ind){
		//string s = "XXFWC with "+ind.ToString()+" dir = "+dir; //for debug
		FieldIndex tmp;
		if( dir == 0){tmp = ind.right;}
		else if( dir == 1){tmp = ind.down;}
		else if( dir == 2){tmp = ind.left;}
		else{tmp = ind.up;}
		//Debug.Log(s+": "+tmp.ToString());
		return tmp;
	}
	
	public FieldIndex Right( FieldIndex ind){
		if( dir == 0){return ind.down;}
		else if( dir == 1){return ind.left;}
		else if( dir == 2){return ind.up;}
		else{return ind.right;}
	}

	public FieldIndex Down( FieldIndex ind){
		if( dir == 0){return ind.left;}
		else if( dir == 1){return ind.up;}
		else if( dir == 2){return ind.right;}
		else{return ind.down;}
	}
	
	public FieldIndex Left( FieldIndex ind){
		if( dir == 0){return ind.up;}
		else if( dir == 1){return ind.right;}
		else if( dir == 2){return ind.down;}
		else{return ind.left;}
	}
	
	public FieldIndex DiagForward( FieldIndex ind){
		//string s = "XXFWC with "+ind.ToString()+" dir = "+dir; //for debug
		FieldIndex tmp;
		if( dir == 0){tmp = ind.downRight;}
		else if( dir == 1){tmp = ind.downLeft;}
		else if( dir == 2){tmp = ind.upLeft;}
		else{tmp = ind.upRight;}
		//Debug.Log(s+": "+tmp.ToString());
		return tmp;
	}
	
	public FieldIndex DiagRight( FieldIndex ind){
		if( dir == 0){return ind.downLeft;}
		else if( dir == 1){return ind.upLeft;}
		else if( dir == 2){return ind.upRight;}
		else{return ind.downRight;}
	}

	public FieldIndex DiagDown( FieldIndex ind){
		//string s = "XXFWC with "+ind.ToString()+" dir = "+dir; //for debug
		FieldIndex tmp;
		if( dir == 0){tmp = ind.upLeft;}
		else if( dir == 1){tmp = ind.upRight;}
		else if( dir == 2){tmp = ind.downRight;}
		else{tmp = ind.downLeft;}
		//Debug.Log(s+": "+tmp.ToString());
		return tmp;
	}
	
	public FieldIndex DiagLeft( FieldIndex ind){
		if( dir == 0){return ind.upRight;}
		else if( dir == 1){return ind.downRight;}
		else if( dir == 2){return ind.downLeft;}
		else{return ind.upLeft;}
	}
}



// Struct SkillContainer:
// -stores the availible skills for each player

public struct SkillContainer{
	
	public int shoot;
	public int build;
	public int emp;
	public int square;
		
	public void Reset(){
		shoot = 0;
		build = 0;
		emp = 0;
		square = 0;
	}
	
}