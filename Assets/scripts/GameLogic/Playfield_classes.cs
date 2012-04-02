using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Contains:
//-FieldIndex
//-Field

public enum Route {empty, red, blue, redBuilt, blueBuilt, destroyed, outOfBounds};


public struct FieldIndex {

	public int index;
	public int n;
	
	public FieldIndex(int n_tmp){
		n = n_tmp;
		index = -1;
	}
	public FieldIndex(int i, int n_tmp){
		n = n_tmp;
		index = i;
	}
	public FieldIndex(int i, int j, int n_tmp){
		n = n_tmp;
		index = n*i + j;
	}
	public void Set(int i){
		index = i;
	}
	public void Set(int i, int j){
		index = i + n*j;
	}
	//~ public int X(){
		//~ return (int)index/n;
	//~ }
	//~ public int Y(){
		//~ return index%n;
	//~ }
	
	override public string ToString(){
		string s = "(" + x + "," + y + ")";
		return s;
	}
	
	public int x
	{
		get { return (int)index/n; }
	}
	public int y
	{
		get { return index%n; }
	}


	public FieldIndex up{
		get{ 
			if (y<8)
				return new FieldIndex(x,y+1,n);
			else
				return new FieldIndex(-1,n);
			}
		}
	public FieldIndex down{
		get{ 
			if (y>0)
				return new FieldIndex(x,y-1,n);
			else
				return new FieldIndex(-1,n);
			}
		}
	public FieldIndex left{
		get{ 
			if (x>0)
				return new FieldIndex(x-1,y,n);
			else
				return new FieldIndex(-1,n);
			}
		}
	public FieldIndex right{
		get{ 
			if (x<8)
				return new FieldIndex(x+1,y,n);
			else
				return new FieldIndex(-1,n);
			}
		}
	public FieldIndex upLeft{
		get{ 
			if (y<8 && x>0)
				return new FieldIndex(x-1,y+1,n);
			else
				return new FieldIndex(-1,n);
			}
		}
	public FieldIndex downLeft{
		get{ 
			if (y>0 && x>0)
				return new FieldIndex(x-1,y-1,n);
			else
				return new FieldIndex(-1,n);
			}
		}
	public FieldIndex downRight{
		get{ 
			if (x<8 && y>0)
				return new FieldIndex(x+1,y-1,n);
			else
				return new FieldIndex(-1,n);
			}
		}
	public FieldIndex upRight{
		get{ 
			if (x<8 && y<8 )
				return new FieldIndex(x+1,y+1,n);
			else
				return new FieldIndex(-1,n);
			}
		}

	public List<FieldIndex> GetStraightNeighbours(){
		List<FieldIndex> set = new List<FieldIndex>();
		if( up.index != -1)
			set.Add(up);
		if( right.index != -1)
			set.Add(right);
		if( down.index != -1)
			set.Add(down);
		if( left.index != -1)
			set.Add(left);
		return set;
	}
	
	public string LogStraightNeighbours(){
		List<FieldIndex> set = GetStraightNeighbours();
		string s = "";
		int cnt = 1;
		foreach( FieldIndex i in set){
			s += cnt+": "+i.x+", "+i.y+"; ";
			cnt++;
		}
		return s;
	}
	public List<FieldIndex> GetDiagNeighbours(){
		List<FieldIndex> set = new List<FieldIndex>();
		if( upRight.index != -1)
			set.Add(upRight);
		if( upLeft.index != -1)
			set.Add(upLeft);
		if( downRight.index != -1)
			set.Add(downRight);
		if( downLeft.index != -1)
			set.Add(downLeft);
		return set;
	}
	
	public string LogDiagNeighbours(){
		List<FieldIndex> set = GetDiagNeighbours();
		string s = "";
		int cnt = 1;
		foreach( FieldIndex i in set){
			s += cnt+": "+i.x+","+i.y+"; ";
			cnt++;
		}
		return s;
	}
}

public class Field<T>{
	
	private T[] a; 
	private int n;
	
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
			return Route.outOfBounds;
		}
	}
	
	//**END STATIC FUNCTIONS**//
	
	
	
	public Field(){
		n = 9;
		a = new T[n*n];
	}
	public Field (int n_tmp){
		n = n_tmp;
		a = new T[n*n];
	}
	public Field (int n_tmp, T defaultValue){
		n = n_tmp;
		a = new T[n*n];
		Reset(defaultValue);
	}
	
	public Field( Field<T> f){
		n = f.n;
		a = (T[])f.a.Clone();
	}
	
	public void Reset(T defaultValue){
		for( int i = 0; i < n*n; i++){
			a[i] = defaultValue;
		}
	}
	public void Set(T element, int index){
		a[index] = element;
	}
	public void Set(T element, FieldIndex x){
		if (n == x.n){
			a[x.index] = element;
		}else{
			Debug.LogError("Field.Add() - size mismatch");
		}
	}
	public T At(int index){
		return a[index];
	}	
	public T At(FieldIndex x){
		if (n == x.n){
			return a[x.index];
		}else{
			Debug.LogError("Field.Add() - size mismatch");
			return default(T);
		}
	}

	// properties for array-like calls
	public T this[FieldIndex ind]{
		get{ return a[ind.index]; }
		set{ a[ind.index] = value;}
	}
	public T this[int ind]{
		get{ return a[ind]; }
		set{ a[ind] = value;}
	}
	public T this[int x, int y]{
		get{ return a[x + n*y]; }
		set{  a[x + n*y] = value;}
	}
	
}


