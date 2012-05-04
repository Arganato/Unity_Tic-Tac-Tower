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

	
	public static void FindBuildTower(Shape s, FieldIndex ind, Field<bool> taken, ref List<Tower> buildList){
		//Debug.Log("Finding 2nd piece forward on "+tmp.ToString());
		Tower newTower = new Tower();
		FieldIndex tmp = s.Forward(ind);
		newTower.Add(ind);
		newTower.Add(tmp);
		newTower.towerType = TowerType.build;						
		ind = tmp;
		tmp = s.Forward(ind);
		
		if(tmp.index != -1 && taken[tmp]==true){
			//Debug.Log("Finding 3rd piece forward on "+tmp.ToString());
			newTower.Add(tmp);
			ind = tmp;
			tmp = s.Left(ind);
			bool left_tower = s.Left(ind).index != -1 && taken[s.Left(ind)] == true;
			bool right_tower = s.Right(ind).index != -1 && taken[s.Right(ind)] == true;
			if(left_tower && right_tower){ //Left and right turn build tower.
				Tower rightTower = newTower.Copy();
				newTower.Add(s.Left(ind));
				Debug.Log(newTower.ToString());
				rightTower.Add(s.Right(ind));
				Debug.Log(rightTower.ToString());
				buildList.Add(newTower);
				buildList.Add(rightTower);
			}else if(left_tower){			//Left turn build tower.
				newTower.Add(s.Left(ind));
				Debug.Log(newTower.ToString());
				buildList.Add(newTower);
			}else if(right_tower){			//Right turn build tower.
				newTower.Add(s.Right(ind));
				Debug.Log(newTower.ToString());
				buildList.Add(newTower);
			}
		}
	}
	
	public static void FindShootTower(Shape s, FieldIndex ind, Field<bool> taken, ref List<Tower> buildList){
		//Debug.Log("Finding 2nd piece forward on "+tmp.ToString());
		Tower newTower = new Tower();
		FieldIndex tmp = s.Forward(ind);
		newTower.Add(ind);
		newTower.Add(tmp);
		newTower.towerType = TowerType.shoot;						
		ind = tmp;
		
		bool left_tower = s.Left(ind).index != -1 && taken[s.Left(ind)] == true;
		bool right_tower = s.Right(ind).index != -1 && taken[s.Right(ind)] == true;
		if(left_tower && right_tower){ //Shoot tower found.
			newTower.Add(s.Left(ind));
			newTower.Add(s.Right(ind));
			Debug.Log(newTower.ToString());
			buildList.Add(newTower);
		}
	}
	
	public static void FindEmpTower(Shape s, FieldIndex ind, Field<bool> taken, ref List<Tower> buildList){
		//Debug.Log("Finding 2nd piece forward on "+tmp.ToString());
		Tower newTower = new Tower();
		FieldIndex tmp = s.Forward(ind);
		newTower.Add(ind);
		newTower.Add(tmp);
		newTower.towerType = TowerType.emp;						
		ind = tmp;
		bool left_tower = s.Left(ind).index != -1 && taken[s.Left(ind)] == true;
		bool right_tower = s.Right(ind).index != -1 && taken[s.Right(ind)] == true;
		if(left_tower && right_tower){
			//Debug.Log("Finding 3rd piece forward on "+tmp.ToString());
			Tower rightTower = newTower.Copy();
			
			newTower.Add(s.Left(ind));
			//Debug.Log(newTower.ToString());
			rightTower.Add(s.Right(ind));
			//Debug.Log(rightTower.ToString());
			
			tmp = s.Forward(s.Right(ind));
			FieldIndex tmpRight = s.Forward(s.Left(ind));
			
			if(tmp.index != -1 && taken[tmp]==true){
				newTower.Add(tmp);
				Debug.Log(newTower.ToString());
				buildList.Add(newTower);
			}			
			if(tmpRight.index != -1 && taken[tmpRight]==true){
				newTower.Add(tmpRight);
				Debug.Log(newTower.ToString());
				buildList.Add(rightTower);
			}
		}			
		else if(left_tower){
			newTower.Add(s.Left(ind));
			tmp = s.Forward(s.Left(ind));
		
			if(tmp.index != -1 && taken[tmp]==true){
				newTower.Add(tmp);
				Debug.Log(newTower.ToString());
				buildList.Add(newTower);
			}
		}
		else if(right_tower){
			newTower.Add(s.Right(ind));
			tmp = s.Forward(s.Right(ind));
			
			if(tmp.index != -1 && taken[tmp]==true){
				newTower.Add(tmp);
				Debug.Log(newTower.ToString());
				buildList.Add(newTower);
			}				
		}
	}
	
	public static void FindSquareTower(Shape s, FieldIndex ind, Field<bool> taken, ref List<Tower> buildList){
		Tower newTower = new Tower();
		newTower.towerType = TowerType.square;
		//Debug.Log("Found 2nd piece forward on "+ind.ToString());
		newTower.Add(ind);
		newTower.Add(s.Forward(ind));
		ind = s.Right(s.Forward(ind));
		//Debug.Log("Looking for 3rd piece forward on "+tmp.ToString());
		
		if(ind.index != -1 && taken[ind]==true){
			newTower.Add(ind);
			ind = s.Down(ind);
			//Debug.Log("Looking for 4th piece right on "+tmp.ToString());
			
			if(ind.index != -1 && taken[ind]==true){
				newTower.Add(ind);
				Debug.Log(newTower.ToString());
				buildList.Add(newTower);
			}
		}	
	}
	
	public static void FindFiveTower(Shape s, FieldIndex ind, Field<bool> taken, ref List<Tower> buildList){
		Tower newTower = new Tower();
		FieldIndex tmp = s.Forward(ind);
		newTower.Add(ind);
		newTower.Add(tmp);
		newTower.towerType = TowerType.five;
		ind = tmp;
		tmp = s.Forward(ind);
		
		if(tmp.index != -1 && taken[tmp] == true){
			newTower.Add(tmp);
			ind = tmp;
			tmp = s.Forward(ind);
			if(tmp.index != -1 && taken[tmp] == true){
				newTower.Add(tmp);
				ind = tmp;
				tmp = s.Forward(ind);
				if(tmp.index != -1 && taken[tmp] == true){
					newTower.Add(tmp);
					Debug.Log(newTower.ToString());
					buildList.Add(newTower);
				}
			}
		}
	}		
	
	public static void FindDiagBuildTower(Shape s, FieldIndex ind, Field<bool> takenDiag, ref List<Tower> buildList){
		Tower newTower = new Tower();
		FieldIndex tmp = s.DiagForward(ind);
		//Debug.Log("Finding 2nd piece forward on "+tmp.ToString());
		newTower.Add(ind);
		newTower.Add(tmp);
		newTower.towerType = TowerType.build;						
		ind = tmp;
		tmp = s.DiagForward(ind);
		
		if(tmp.index != -1 && takenDiag[tmp]==true){
			//Debug.Log("Finding 3rd piece forward on "+tmp.ToString());
			newTower.Add(tmp);
			ind = tmp;
			tmp = s.DiagLeft(ind);
			bool left_tower = s.DiagLeft(ind).index != -1 && takenDiag[s.DiagLeft(ind)] == true;
			bool right_tower = s.DiagRight(ind).index != -1 && takenDiag[s.DiagRight(ind)] == true;
			if(left_tower && right_tower){ //Left and right turn build tower.
				Tower rightTower = newTower.Copy();
				newTower.Add(s.DiagLeft(ind));
				Debug.Log(newTower.ToString());
				rightTower.Add(s.DiagRight(ind));
				Debug.Log(rightTower.ToString());
				buildList.Add(newTower);
				buildList.Add(rightTower);
			}else if(left_tower){			//Left turn build tower.
				newTower.Add(s.DiagLeft(ind));
				Debug.Log(newTower.ToString());
				buildList.Add(newTower);
			}else if(right_tower){			//Right turn build tower.
				newTower.Add(s.DiagRight(ind));
				Debug.Log(newTower.ToString());
				buildList.Add(newTower);
			}
		}
	}
	
	public static void FindDiagShootTower(Shape s, FieldIndex ind, Field<bool> takenDiag, ref List<Tower> buildList){
		//Debug.Log("Finding 2nd piece forward on "+tmp.ToString());
		Tower newTower = new Tower();
		FieldIndex tmp = s.DiagForward(ind);
		newTower.Add(ind);
		newTower.Add(tmp);
		newTower.towerType = TowerType.shoot;						
		ind = tmp;
		
		bool left_tower = s.DiagLeft(ind).index != -1 && takenDiag[s.DiagLeft(ind)] == true;
		bool right_tower = s.DiagRight(ind).index != -1 && takenDiag[s.DiagRight(ind)] == true;
		if(left_tower && right_tower){ //Shoot tower found.
			newTower.Add(s.DiagLeft(ind));
			newTower.Add(s.DiagRight(ind));
			Debug.Log(newTower.ToString());
			buildList.Add(newTower);
		}
	}
	
	public static void FindDiagEmpTower(Shape s, FieldIndex ind, Field<bool> takenDiag, ref List<Tower> buildList){
		//Debug.Log("Finding 2nd piece forward on "+tmp.ToString());
		Tower newTower = new Tower();
		FieldIndex tmp = s.DiagForward(ind);
		newTower.Add(ind);
		newTower.Add(tmp);
		newTower.towerType = TowerType.emp;						
		ind = tmp;
		bool left_tower = s.DiagLeft(ind).index != -1 && takenDiag[s.DiagLeft(ind)] == true;
		bool right_tower = s.DiagRight(ind).index != -1 && takenDiag[s.DiagRight(ind)] == true;
		if(left_tower && right_tower){
			//Debug.Log("Finding 3rd piece forward on "+tmp.ToString());
			Tower rightTower = newTower.Copy();
			
			newTower.Add(s.DiagLeft(ind));
			//Debug.Log(newTower.ToString());
			rightTower.Add(s.DiagRight(ind));
			//Debug.Log(rightTower.ToString());
			
			tmp = s.DiagForward(s.DiagRight(ind));
			FieldIndex tmpRight = s.DiagForward(s.DiagLeft(ind));
			
			if(tmp.index != -1 && takenDiag[tmp]==true){
				newTower.Add(tmp);
				Debug.Log(newTower.ToString());
				buildList.Add(newTower);
			}			
			if(tmpRight.index != -1 && takenDiag[tmpRight]==true){
				newTower.Add(tmpRight);
				Debug.Log(newTower.ToString());
				buildList.Add(rightTower);
			}
		}			
		else if(left_tower){
			newTower.Add(s.DiagLeft(ind));
			tmp = s.DiagForward(s.DiagLeft(ind));
		
			if(tmp.index != -1 && takenDiag[tmp]==true){
				newTower.Add(tmp);
				Debug.Log(newTower.ToString());
				buildList.Add(newTower);
			}
		}
		else if(right_tower){
			newTower.Add(s.DiagRight(ind));
			tmp = s.DiagForward(s.DiagRight(ind));
			
			if(tmp.index != -1 && takenDiag[tmp]==true){
				newTower.Add(tmp);
				Debug.Log(newTower.ToString());
				buildList.Add(newTower);
			}				
		}
	}
	
	public static void FindDiagSquareTower(Shape s, FieldIndex ind, Field<bool> takenDiag, ref List<Tower> buildList){
		Tower newTower = new Tower();
		newTower.towerType = TowerType.square;
		//Debug.Log("Found 2nd piece forward on "+ind.ToString());
		newTower.Add(ind);
		newTower.Add(s.DiagForward(ind));
		ind = s.DiagRight(s.DiagForward(ind));
		//Debug.Log("Looking for 3rd piece forward on "+tmp.ToString());
		
		if(ind.index != -1 && takenDiag[ind]==true){
			newTower.Add(ind);
			ind = s.DiagDown(ind);
			//Debug.Log("Looking for 4th piece right on "+tmp.ToString());
			
			if(ind.index != -1 && takenDiag[ind]==true){
				newTower.Add(ind);
				Debug.Log(newTower.ToString());
				buildList.Add(newTower);
			}
		}	
	}
	
	public static void FindDiagFiveTower(Shape s, FieldIndex ind, Field<bool> takenDiag, ref List<Tower> buildList){
		Tower newTower = new Tower();
		FieldIndex tmp = s.DiagForward(ind);
		newTower.Add(ind);
		newTower.Add(tmp);
		newTower.towerType = TowerType.five;
		ind = tmp;
		tmp = s.DiagForward(ind);
		
		if(tmp.index != -1 && takenDiag[tmp] == true){
			newTower.Add(tmp);
			ind = tmp;
			tmp = s.DiagForward(ind);
			if(tmp.index != -1 && takenDiag[tmp] == true){
				newTower.Add(tmp);
				ind = tmp;
				tmp = s.DiagForward(ind);
				if(tmp.index != -1 && takenDiag[tmp] == true){
					newTower.Add(tmp);
					Debug.Log(newTower.ToString());
					buildList.Add(newTower);
				}
			}
		}
	}	
	
	public static List<Tower> FindTower( Field<bool> taken, Field<bool> takenDiag){
		List<Tower> buildList = new List<Tower>();
		Shape s;
		for( int i = 0; i < 81; i++){
			if( taken[i] == true || takenDiag[i] == true){
				
				//Debug.Log("starting from "+new FieldIndex(i,9).ToString());
				for( int j = 0; j<4; j++){	
					FieldIndex ind = new FieldIndex(i,Stats.fieldSize);
					s.dir = j;
					//Debug.Log("reference direction set to "+j);
					FieldIndex tmp = s.Forward(ind);
					
					// **Straight towers**
					if(tmp.index != -1 && taken[tmp]==true){ //If true; there might be towers.
						//checks with Stats if the towers has been disabled:
						if(Stats.skillEnabled.build){ // build
							FindBuildTower(s, ind, taken, ref buildList);
						}if(Stats.skillEnabled.shoot){ // shoot
							FindShootTower(s, ind, taken, ref buildList);
						}if(j<2){
							if(Stats.skillEnabled.emp){ // emp
								FindEmpTower(s, ind, taken, ref buildList);
							}
							FindFiveTower(s, ind, taken, ref buildList);
						}
						if(j==3 && Stats.skillEnabled.square){ // square
							FindSquareTower(s, ind, taken, ref buildList);
						}
						
					}
					
					// **Diagonal Towers**
					FieldIndex tmpDiag = s.DiagForward(ind);
					//Debug.Log("takenDiag: " + tmpDiag.LogDiagNeighbours());
					if(tmpDiag.index != -1 && takenDiag[tmpDiag]==true){
						if(Stats.skillEnabled.diagEmp){ // diag build
							FindDiagBuildTower(s, ind, takenDiag, ref buildList);
						}if(Stats.skillEnabled.diagShoot){ // diag shoot
							FindDiagShootTower(s, ind, takenDiag, ref buildList);
						}
						if(j<2){
							if(Stats.skillEnabled.diagEmp){ // diag emp
								FindDiagEmpTower(s, ind, takenDiag, ref buildList);
							}
							FindDiagFiveTower(s, ind, takenDiag, ref buildList);
						}
						if(j==3 && Stats.skillEnabled.diagSquare){ // diag square
							FindDiagSquareTower(s, ind, takenDiag, ref buildList);
						}
						
					}
				}
			}
		}
		return buildList;
	}
	
	
}