using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Class Tower:
// Contains static function for finding towers
// A tower-instance stores the position of a tower

public class Tower {
	
	private List<FieldIndex> list;
	private TowerType type;
	
	public Tower(){
		list = new List<FieldIndex>();
	}
	
	public TowerType towerType{ //remove due to uselessness
		get{return type;}
		set{type = value;}
	}
	
	public List<FieldIndex> GetList(){
		return list;
	}
	
	public void Add(FieldIndex ind){
		//Debug.Log("calling Tower.Add");
		if(list.Count == 0){
			list.Add(ind);
			//Debug.Log("Tower: Adding "+ind.ToString()+" on pos 0");
		}else{
			for( int i = 0; i < list.Count; i++){
				if( list[i].index > ind.index){
					list.Insert(i,ind);
					//Debug.Log("Tower: Adding "+ind.ToString()+" on pos "+i);
					return;
				}
			}
			//if all the elements in the list is smaller than the new element
			list.Insert(list.Count,ind);
			//Debug.Log("Tower: Adding "+ind.ToString()+" on pos "+list.Count);
		}
	}
	
	public Tower Copy(){
		Tower tmp = new Tower();
		tmp.type = type;
		tmp.list = new List<FieldIndex>(list);
		return tmp;
	}
	
	override public string ToString(){
		string s;
		if(type == TowerType.build){
			s = "Build tower: ";
		}
		else if(type == TowerType.shoot){
			s = "Shoot tower: ";
		}
		else if(type == TowerType.emp){
			s = "EMP tower: ";
		}
		else if(type == TowerType.square){
			s = "Square tower: ";
		}
		else if(type == TowerType.five){
			s = "\"Five in a row\"-tower: ";
		}
		else{s = "Unknown tower type:";
		}
		for( int i = 0; i < list.Count; i++){
			s = s + " " + list[i].ToString();
		}
		return s + ".";
	}
	
	//****STATIC FIND-TOWER-FUNCTIONS****
	
	public static Field<bool> FindAllClusterRecurse(FieldIndex ind, Field<bool> taken){
		taken[ind] = true;
		foreach( FieldIndex i in ind.GetNeighbours() ){
			if( Control.playField[i] == Control.playField[ind] && taken[i] == false){
				taken = FindAllClusterRecurse(i, taken);
			}
		}
		return taken;
	}
	
	public static Field<bool> FindClusterRecurse( FieldIndex ind, Field<bool> taken){
		taken[ind] = true;
		//Debug.Log("FindCluster, NB's: "+ind.LogStraightNeighbours());
		foreach( FieldIndex i in ind.GetStraightNeighbours() ){
			if( Control.playField[i] == Control.playField[ind] && taken[i] == false ){
				//Debug.Log("calling FCR from "+i.x+", "+i.y+"...");
				taken = FindClusterRecurse(i, taken);
			}
		}
		return taken;
	}
	
	public static Field<bool> FindDiagClusterRecurse( FieldIndex ind, Field<bool> takenDiag){
		takenDiag[ind] = true;
		foreach( FieldIndex i in ind.GetDiagNeighbours() ){
			if( Control.playField[i] == Control.playField[ind] && takenDiag[i] == false ){
				takenDiag = FindDiagClusterRecurse(i, takenDiag);
			}
		}
		return takenDiag;
	}

	
	public static void FindBuildTower(int direction, FieldIndex ind, Field<bool> taken, ref List<Tower> buildList){
		//Debug.Log("Finding 2nd piece forward on "+tmp.ToString());
		Tower newTower = new Tower();
		FieldIndex tmp = ind.Up(direction);
		newTower.Add(ind);
		newTower.Add(tmp);
		newTower.towerType = TowerType.build;						
		ind = tmp;
		tmp = ind.Up(direction);
		
		if(tmp.index != -1 && taken[tmp]==true){
			//Debug.Log("Finding 3rd piece forward on "+tmp.ToString());
			newTower.Add(tmp);
			ind = tmp;
			FieldIndex left = ind.Left(direction);
			FieldIndex right = ind.Right(direction); 
			bool left_tower = left.index != -1 && taken[left] == true;
			bool right_tower = right.index != -1 && taken[right] == true;
			if(left_tower && right_tower){ //Left and right turn build tower.
				Tower rightTower = newTower.Copy();
				newTower.Add(left);
				Debug.Log(newTower.ToString());
				rightTower.Add(right);
				Debug.Log(rightTower.ToString());
				buildList.Add(newTower);
				buildList.Add(rightTower);
			}else if(left_tower){			//Left turn build tower.
				newTower.Add(left);
				Debug.Log(newTower.ToString());
				buildList.Add(newTower);
			}else if(right_tower){			//Right turn build tower.
				newTower.Add(right);
				Debug.Log(newTower.ToString());
				buildList.Add(newTower);
			}
		}
	}
	
	public static void FindShootTower(int direction, FieldIndex ind, Field<bool> taken, ref List<Tower> buildList){
		//Debug.Log("Finding 2nd piece forward on "+tmp.ToString());
		Tower newTower = new Tower();
		FieldIndex tmp = ind.Up(direction);
		newTower.Add(ind);
		newTower.Add(tmp);
		newTower.towerType = TowerType.shoot;						
		ind = tmp;
		FieldIndex left = ind.Left(direction);
		FieldIndex right = ind.Right(direction);
		bool left_tower = left.index != -1 && taken[left] == true;
		bool right_tower = right.index != -1 && taken[right] == true;
		if(left_tower && right_tower){ //Shoot tower found.
			newTower.Add(left);
			newTower.Add(right);
			Debug.Log(newTower.ToString());
			buildList.Add(newTower);
		}
	}
	
	public static void FindEmpTower(int direction, FieldIndex ind, Field<bool> taken, ref List<Tower> buildList){
		//Debug.Log("Finding 2nd piece forward on "+tmp.ToString());
		Tower newTower = new Tower();
		FieldIndex tmp = ind.Up(direction);
		newTower.Add(ind);
		newTower.Add(tmp);
		newTower.towerType = TowerType.emp;						
		ind = tmp;
		FieldIndex right = ind.Right(direction);
		FieldIndex left = ind.Left(direction);
		bool left_tower = left.index != -1 && taken[left] == true;
		bool right_tower = right.index != -1 && taken[right] == true;
		if(left_tower && right_tower){
//			Debug.Log("Finding 3rd piece forward on "+tmp.ToString());
			Tower rightTower = newTower.Copy();
			
			newTower.Add(left);
			//Debug.Log(newTower.ToString());
			rightTower.Add(right);
			//Debug.Log(rightTower.ToString());
			
			tmp = left.Up(direction);
			FieldIndex tmpRight = right.Up(direction);
			
			if(tmp.index != -1 && taken[tmp]==true){
				newTower.Add(tmp);
				Debug.Log(newTower.ToString());
				buildList.Add(newTower);
			}			
			if(tmpRight.index != -1 && taken[tmpRight]==true){
				rightTower.Add(tmpRight);
				Debug.Log(newTower.ToString());
				buildList.Add(rightTower);
			}
		}			
		else if(left_tower){
			newTower.Add(left);
			tmp = left.Up(direction);
		
			if(tmp.index != -1 && taken[tmp]==true){
				newTower.Add(tmp);
				Debug.Log(newTower.ToString());
				buildList.Add(newTower);
			}
		}
		else if(right_tower){
			newTower.Add(right);
			tmp = right.Up(direction);
			
			if(tmp.index != -1 && taken[tmp]==true){
				newTower.Add(tmp);
				Debug.Log(newTower.ToString());
				buildList.Add(newTower);
			}				
		}
	}
	
	public static void FindSquareTower(int direction, FieldIndex ind, Field<bool> taken, ref List<Tower> buildList){
		Tower newTower = new Tower();
		newTower.towerType = TowerType.square;
		//Debug.Log("Found 2nd piece forward on "+ind.ToString());
		newTower.Add(ind);
		FieldIndex tmp = ind.Up(direction);
		newTower.Add(tmp);
		ind = tmp.Right(direction);
		//Debug.Log("Looking for 3rd piece forward on "+tmp.ToString());
		
		if(ind.index != -1 && taken[ind]==true){
			newTower.Add(ind);
			ind = ind.Down(direction);
			//Debug.Log("Looking for 4th piece right on "+tmp.ToString());
			
			if(ind.index != -1 && taken[ind]==true){
				newTower.Add(ind);
				Debug.Log(newTower.ToString());
				buildList.Add(newTower);
			}
		}	
	}
	

	public static void FindFiveTower(int direction, FieldIndex ind, Field<bool> taken, ref List<Tower> buildList){
		Tower newTower = new Tower();
		FieldIndex tmp = ind.Up(direction);
		newTower.Add(ind);
		newTower.Add(tmp);
		newTower.towerType = TowerType.five;
		ind = tmp;
		tmp = ind.Up(direction);
		
		if(tmp.index != -1 && taken[tmp] == true){
			newTower.Add(tmp);
			ind = tmp;
			tmp = ind.Up(direction);
			if(tmp.index != -1 && taken[tmp] == true){
				newTower.Add(tmp);
				ind = tmp;
				tmp = ind.Up(direction);
				if(tmp.index != -1 && taken[tmp] == true){
					newTower.Add(tmp);
					Debug.Log(newTower.ToString());
					buildList.Add(newTower);
				}
			}
		}
	}	
	
	public static List<Tower> FindTower( Field<bool> taken){
		List<Tower> buildList = new List<Tower>();
		for( int i = 0; i < Stats.fieldSize*Stats.fieldSize; i++){
			if( taken[i] == true ){
				
				//Debug.Log("starting from "+new FieldIndex(i).ToString());
				for( int j = 0; j<8; j++){	//all directions
					FieldIndex ind = new FieldIndex(i);
					FieldIndex tmp = ind.Up(j);

					if(tmp.index != -1 && taken[tmp]==true){ //If true; there might be towers.
						//checks with Stats if the towers has been disabled:
						if(Stats.skillEnabled.build){ // build
							FindBuildTower(j, ind, taken, ref buildList);
						}if(Stats.skillEnabled.shoot){ // shoot
							FindShootTower(j, ind, taken, ref buildList);
						}if(j<4){
							if(Stats.skillEnabled.emp){ // emp
								FindEmpTower(j, ind, taken, ref buildList);
							}
							FindFiveTower(j, ind, taken, ref buildList);
						}
						if(j>5 && Stats.skillEnabled.square){ // square
							FindSquareTower(j, ind, taken, ref buildList);
						}
						
					}
					
				}
			}
		}
		return buildList;
	}
	
	
}