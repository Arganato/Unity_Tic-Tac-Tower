using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Field<T>{
	
	private T[] a; 
	
	//**STATIC FUNCTIONS**//
	// functions to modify route colors in an easy way. may fit better somewhere else.
	
	public static Route GetDarkRoute(Route r){
		if( r == Route.red ){
			return Route.redBuilt;
		}if( r == Route.blue ){
			return Route.blueBuilt;
		}
		return r;
	}
	
	public static Route GetPlayerColor(int i){
		if(i == 0){
			return Route.red;
		}if(i == 1){
			return Route.blue;
		}else{
			Debug.LogError("Player "+i+" not found");
			return Route.outOfBounds;
		}
	}
	
	//**END STATIC FUNCTIONS**//
	
	
	
	public Field (){
		a = new T[Stats.fieldSize*Stats.fieldSize];
	}
	public Field (T defaultValue){
		a = new T[Stats.fieldSize*Stats.fieldSize];
		Reset(defaultValue);
	}
	
	public Field( Field<T> f){
		a = (T[])f.a.Clone();
	}
	
	public void Reset(T defaultValue){
		for( int i = 0; i < Stats.fieldSize*Stats.fieldSize; i++){
			a[i] = defaultValue;
		}
	}

	// properties for array-like calls
	public T this[FieldIndex ind]{
		get{ 
			if( ind.index>= 0 && ind.index < Stats.fieldSize*Stats.fieldSize){
				return a[ind.index];
			}else{
				Debug.LogError("Field: Invalid FieldIndex");
				return default(T);
			}
		}
		set
		{ 
			if( ind.index>= 0 && ind.index < Stats.fieldSize*Stats.fieldSize){
				a[ind.index] = value;
			}else{
				Debug.LogError("Field: Invalid FieldIndex");
			}
		}
	}
	public T this[int ind]{
		get{ return a[ind]; }
		set{ a[ind] = value;}
	}
	public T this[int x, int y]{
		get{ return a[x + Stats.fieldSize*y]; }
		set{  a[x + Stats.fieldSize*y] = value;}
	}
	
}