using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct FieldIndex {

	public int index;
	
	public FieldIndex(int i){
		index = i;
	}
	public FieldIndex(int i, int j){
		index = Stats.fieldSize*i + j;
	}
	public void Set(int i){
		index = i;
	}
	public void Set(int i, int j){
		index = i + Stats.fieldSize*j;
	}
	
	override public string ToString(){
		string s = "(" + x + "," + y + ")";
		return s;
	}
	
	public int x
	{
		get { return (int)index/Stats.fieldSize; }
	}
	public int y
	{
		get { return index%Stats.fieldSize; }
	}


	public FieldIndex up{
		get{ 
			if (y<Stats.fieldSize-1)
				return new FieldIndex(x,y+1);
			else
				return new FieldIndex(-1);
			}
		}
	public FieldIndex down{
		get{ 
			if (y>0)
				return new FieldIndex(x,y-1);
			else
				return new FieldIndex(-1);
			}
		}
	public FieldIndex left{
		get{ 
			if (x>0)
				return new FieldIndex(x-1,y);
			else
				return new FieldIndex(-1);
			}
		}
	public FieldIndex right{
		get{ 
			if (x<Stats.fieldSize-1)
				return new FieldIndex(x+1,y);
			else
				return new FieldIndex(-1);
			}
		}
	public FieldIndex upLeft{
		get{ 
			if (y<Stats.fieldSize-1 && x>0)
				return new FieldIndex(x-1,y+1);
			else
				return new FieldIndex(-1);
			}
		}
	public FieldIndex downLeft{
		get{ 
			if (y>0 && x>0)
				return new FieldIndex(x-1,y-1);
			else
				return new FieldIndex(-1);
			}
		}
	public FieldIndex downRight{
		get{ 
			if (x<Stats.fieldSize-1 && y>0)
				return new FieldIndex(x+1,y-1);
			else
				return new FieldIndex(-1);
			}
		}
	public FieldIndex upRight{
		get{ 
			if (x<Stats.fieldSize-1 && y<Stats.fieldSize-1 )
				return new FieldIndex(x+1,y+1);
			else
				return new FieldIndex(-1);
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
	
	public List<FieldIndex> GetNeighbours(){
		List<FieldIndex> set = new List<FieldIndex>();
		if( up.index != -1)
			set.Add(up);
		if( right.index != -1)
			set.Add(right);
		if( down.index != -1)
			set.Add(down);
		if( left.index != -1)
			set.Add(left);
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
	
	//** Relative direction-functions **//
	
	public FieldIndex Up(int direction){
		switch(direction){
			case 0: return this.up;
			case 1: return this.upLeft;
			case 2: return this.left;
			case 3: return this.downLeft;
			case 4: return this.down;
			case 5: return this.downRight;
			case 6: return this.right;
			case 7: return this.upRight;
			default: return new FieldIndex(-1);
		}
	}
	public FieldIndex Left(int direction){
		switch(direction){
			case 0: return this.left;
			case 1: return this.downLeft;
			case 2: return this.down;
			case 3: return this.downRight;
			case 4: return this.right;
			case 5: return this.upRight;
			case 6: return this.up;
			case 7: return this.upLeft;
			default: return new FieldIndex(-1);
		}
	}
	public FieldIndex Down(int direction){
		switch(direction){
			case 0: return this.down;
			case 1: return this.downRight;
			case 2: return this.right;
			case 3: return this.upRight;
			case 4: return this.up;
			case 5: return this.upLeft;
			case 6: return this.left;
			case 7: return this.downLeft;
			default: return new FieldIndex(-1);
		}
	}

	public FieldIndex Right(int direction){
		switch(direction){
			case 0: return this.right;
			case 1: return this.upRight;
			case 2: return this.up;
			case 3: return this.upLeft;
			case 4: return this.left;
			case 5: return this.downLeft;
			case 6: return this.down;
			case 7: return this.downRight;
			default: return new FieldIndex(-1);
		}
	}
}