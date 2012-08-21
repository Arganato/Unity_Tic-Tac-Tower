using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameTreeBuilder{
	
	public TreeNode rootNode = null;
	public int maximizingPlayer = 1; //player 2
	public int depth = 3;
	
	public void Build(){
		rootNode = new TreeNode(null,0);
		BuildGameTreeRecursive(rootNode,Control.cState,depth,1);
	}
	
		
	private int BuildGameTreeRecursive(TreeNode node, GameState state, int depth, int color){
		//color is 1 for maximizing player and -1 for minimizing player
		if(depth > 0){
//			Debug.Log("Calculating moves on game-board...");
			List<Turn> moves = FindAllMovesOnBoard(state);
//			Debug.Log("Finding values recursively. Depth = "+depth+".");
			int val = int.MinValue; //negative infinity
			foreach( Turn t in moves){
				GameState tmpState = new GameState(state);
				if(tmpState.EvaluateTurn(t)){ // the turn was valid
					TreeNode child = new TreeNode(t,val);
					rootNode.AddChild(child);
					val = Mathf.Max(val, -BuildGameTreeRecursive(child,tmpState, depth-1,-color));					
				}
			}
			return val;
		}else{
			return AI.database.EvaluateGameState(state, maximizingPlayer)*color;
		}
	}
	
	
	private List<Turn> FindAllMovesOnBoard(GameState state){
		List<Turn> ret = new List<Turn>();
//		for (int i=0;i<Stats.fieldSize*Stats.fieldSize;i++){
		for (int i=0;i<3*3;i++){
			Turn tmpTurn = new Turn();
			Order tmpOrder = new Order();
			tmpOrder.endTurn = true;
			tmpOrder.skill = SkillType.place;
			tmpOrder.position = new FieldIndex(3+i%3,3+i/3); //3x3 area in the center
			tmpTurn.Add(tmpOrder);
			ret.Add(tmpTurn);
		}
//		Order tmpo = new Order();
//		tmpo.position = new FieldIndex(3,3);
//		tmpo.skill = SkillType.place;
//		Turn tmpt = new Turn();
//		tmpt.Add(tmpo);
//		ret.Add(tmpt);
		return ret;
	}
}
