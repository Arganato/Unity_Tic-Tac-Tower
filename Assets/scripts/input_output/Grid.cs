using UnityEngine;
using System.Collections;
// doc:
// controls the physical playing grid, 
// converting user input to actual moves


public class Grid : MonoBehaviour {
	
	public Transform emptyPiece;
	public Transform redPiece; //hardcoded to player 1
	public Transform bluePiece; //hardcoded to player 2
	public Transform redTowerPiece;
	public Transform blueTowerPiece;
	public Transform destroyedPiece; 
	public Transform outOfBoundsPiece;

	private Control control;
	
	//private int gridLength = 90;
	private int red_pieceHeigth = 1;
	
	private Field<GridUnit> playFieldTransforms = new Field<GridUnit>(9);
	
	void Awake () {
		control = (Control)FindObjectOfType(typeof(Control));
		playFieldTransforms = new Field<GridUnit>(Stats.fieldSize, null);
	}
	
	void Update () {
		if( Input.GetButtonDown("mouse1") ){
			//Debug.Log(Input.mousePosition);
			//Debug.Log("calling ScreenPointToBoard");
			FieldIndex ind = ScreenPointToBoard(Input.mousePosition);
			if ( ind.index != -1){
				control.UserFieldSelect(ind);
			}
		}
	}
	
	private FieldIndex ScreenPointToBoard( Vector3 pos){
		Ray ray = Camera.main.ScreenPointToRay (pos);
		RaycastHit hit;
		FieldIndex index = new FieldIndex(Stats.fieldSize);
		if (Physics.Raycast (ray, out hit, 100)) {
			GridUnit gu = hit.transform.GetComponent<GridUnit>();
			if(gu == null){
				Debug.Log("component GridUnit not found on mouseclick-target");
			}else{
				//Debug.Log("index "+gu.index.index+" found");
				return gu.index;
			}
			//int y_tmp = Mathf.RoundToInt(hit.point.x/10f) + 4;
			//int x_tmp = Mathf.RoundToInt(hit.point.z/10f) + 4;
			//if (x_tmp >= 0 && x_tmp < Stats.fieldSize && y_tmp >= 0 && y_tmp < Stats.fieldSize ){
			//	index.Set(x_tmp,y_tmp);
			//}
		}else{
			//Debug.Log ("raycast does not intersect any objects");
		}
		return index;
	}
	
	private Vector3 BoardToWorldPoint(FieldIndex ind){
		// returns the point in the middle of the route for the given index
		float x_new = 10f*(ind.x-4);
		float z_new = 10f*(ind.y-4);
		float y_new = red_pieceHeigth;
		return new Vector3(x_new, y_new, z_new);
	}
		
	
	public void InitField(){
		for( int i = 0; i < Stats.fieldSize*Stats.fieldSize; i++){
			//create new transform 
			PlaceTransform(control.playField[i],new FieldIndex(i,Stats.fieldSize));
		}		
	}
	
	public void UpdateField(){

		for( int i = 0; i < Stats.fieldSize*Stats.fieldSize; i++){
			if( control.playField[i] != playFieldTransforms[i].type ){
				//a change has been found in the board
				//destroy old prefab
				Destroy(playFieldTransforms[i].gameObject);
				//create new transform 
				PlaceTransform(control.playField[i],new FieldIndex(i,Stats.fieldSize));
			}
		}
	}
	
	
	
	private void PlaceTransform(Route type, FieldIndex pos){
		Transform theTransform = null;
		switch (type){
			case Route.empty:
				theTransform = emptyPiece;
				break;
			case Route.red:
				theTransform = redPiece;
				break;
			case Route.blue:
				theTransform = bluePiece;
				break;
			case Route.redBuilt:
				theTransform = redTowerPiece;
				break;
			case Route.blueBuilt:
				theTransform = blueTowerPiece;
				break;
			case Route.destroyed:
				theTransform = destroyedPiece;
				break;
			case Route.outOfBounds:
				theTransform = outOfBoundsPiece;
				break;
		}
		Transform foo;
		foo= (Transform)Instantiate(theTransform,BoardToWorldPoint(pos),Quaternion.identity);
		
		GridUnit tmp = foo.GetComponent<GridUnit>();
		if( tmp != null){
			tmp.type = type;
			playFieldTransforms[pos] = tmp;
			tmp.index = pos;
		}else{
			Debug.LogError("Component GridUnit not found on Piece-transform");
		}
	}
	
}
