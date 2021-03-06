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
	public Transform frame;

	private SceneTemplate gui;
	
	
	private Field<GridUnit> playFieldTransforms = new Field<GridUnit>(); //this is the actual playfield as it appears to the players.
	
	void Awake () {
		playFieldTransforms = new Field<GridUnit>((GridUnit)null);
	}
	void Start(){
		gui = (SceneTemplate)FindObjectOfType(typeof(SceneTemplate));
	}	
	
	public void MouseDown(Vector3 mousePosition){
		if( Stats.gameRunning){
			FieldIndex ind = ScreenPointToBoard(mousePosition);
			if ( ind.index != -1){
				gui.UserFieldSelect(ind);
			}
		}
	}
	
	private FieldIndex ScreenPointToBoard( Vector3 pos){
		Ray ray = Camera.main.ScreenPointToRay (pos);
		RaycastHit hit;
		//Debug.Log("Looking for planet");
		if (Physics.Raycast (ray, out hit, 1000)) {
			GridUnit gu = hit.transform.GetComponent<GridUnit>();
			if(gu == null){
				Debug.Log("component GridUnit not found on mouseclick-target");
			}else{
				//Debug.Log("index "+gu.index.index+" found");
				return gu.index;
			}
		}else{
			//Debug.Log ("raycast does not intersect any objects");
		}
		return new FieldIndex(-1);
	}
	
	public static Vector3 BoardToWorldPoint(FieldIndex ind){
		// returns the point in the middle of the route for the given index
		float x_new = 10f*(ind.x-4);
		float z_new = 10f*(ind.y-4);
		float y_new = 1;
		return new Vector3(x_new, y_new, z_new);
	}
		
	
	public void InitField(){
		if(Stats.fieldSize == 9){
			Instantiate(frame);//Vector3.zero
		}
		for( int i = 0; i < Stats.fieldSize*Stats.fieldSize; i++){
			//create new transform 
			if( playFieldTransforms[i] != null ){
				Destroy(playFieldTransforms[i].gameObject);
			}
			PlaceTransform(Control.cState.field[i],new FieldIndex(i));
		}		
	}
	
	public void UpdateField(){

		for( int i = 0; i < Stats.fieldSize*Stats.fieldSize; i++){
			if( Control.cState.field[i] != playFieldTransforms[i].type ){
				//a change has been found in the board
				//destroy old prefab
				Destroy(playFieldTransforms[i].gameObject);
				//create new transform 
				PlaceTransform(Control.cState.field[i],new FieldIndex(i));
			}
		}
	}
	
	public void SetFlashLayer(FieldIndex index, bool active){
		//changes the layer of the piece, so that that piece is hit by the flashing directional light (Tutorial-function)
		if(active){
			playFieldTransforms[index].gameObject.layer = 10;
		}else{
			playFieldTransforms[index].gameObject.layer = 0;
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
