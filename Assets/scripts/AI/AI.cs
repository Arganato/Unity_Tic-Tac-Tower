using UnityEngine;
using System.Collections;

public class AI{

//	private MoveInterface receiver;
	public static AIDatabase database = new AIDatabase();
	public static GameTreeBuilder gameTreeBuilder = new GameTreeBuilder();
	
//	public void SetReceiver(MoveInterface rec){
//		receiver = rec;
//	}
	
	public void Calculate(){
		Debug.Log("Constructing Gametree");
		gameTreeBuilder.Build();
		Debug.Log("Gametree Constructed...");
		Turn bestTurn = null;
		int bestValue = int.MinValue;
		foreach(TreeNode node in gameTreeBuilder.rootNode.GetChildList()){
			Debug.Log("finding a child with value "+node.GetValue()+".");
			if( node.GetValue() > bestValue){
				bestValue = node.GetValue();
				bestTurn = node.GetTurn();
			}
		}
		//send it with the interface
		if (bestTurn == null){
			Debug.Log("No best turn found. BestValue = "+bestValue+".");
		}else{
			Debug.Log("AI's suggestion: "+bestTurn);
		}
	}
	
	
}
