using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TreeNode{

	private List<TreeNode> children = new List<TreeNode>();
	private TreeNode parent;
	private int nodeValue;
	private Turn nodeTurn;
	public GameState state; //public due to lazyness
	
	public TreeNode(Turn t, int val){
		nodeValue = val;
		nodeTurn = t;
	}
	
	public void AddChild(TreeNode ch){
		children.Add(ch);
		ch.parent = this;
	}
	
	public void AddChild(Turn t, int val){
		TreeNode child = new TreeNode(t,val);
		this.AddChild(child);
	}
	
	public TreeNode GetChild(int index){
		return children[index];
	}
	
	public TreeNode GetParent(){
		return parent;
	}
	
	public int GetValue(){
		return nodeValue;
	}
	public void SetValue(int val){
		nodeValue = val;
	}
	
	public Turn GetTurn(){
		return nodeTurn;
	}
	
	public int ChildCount(){
		return children.Count;
	}
	public List<TreeNode> GetChildList(){
		return children;
	}
	

}
