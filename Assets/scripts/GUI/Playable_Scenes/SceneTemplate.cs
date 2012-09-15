using UnityEngine;
using System.Collections;

//see documentation of how to create new scenes in /Documentation/Scene-template-system.pdf

public abstract class SceneTemplate : MonoBehaviour, IGUIMessages {
	
	protected Control control; //private to the child-classes
	protected NetworkInterface networkInterface;
	private Grid grid;
	
	protected void HandleMouseInput(){
		if(Event.current.type == EventType.MouseDown){
			if( Event.current.type != EventType.Used ){
				grid.MouseDown(Input.mousePosition);
			}
		}		
	}
	
	virtual protected void Start(){
		control = (Control)FindObjectOfType(typeof(Control));
		grid = (Grid)FindObjectOfType(typeof(Grid));
		networkInterface = (NetworkInterface)FindObjectOfType(typeof(NetworkInterface));		
	}
	
	// **Implementation of IGUIMessages functions** 
	public virtual void UserEndTurn(){
		control.UserEndTurn();
	}
	public virtual void UserResign(){
		control.UserResign();
	}
	public virtual void UndoTurn(){
		control.UndoTurn();
	}
	public virtual void QuitGame(){
		Control.QuitGame();
	}
	public virtual void UserFieldSelect(FieldIndex position){
		control.UserFieldSelect(position);
	}
	public virtual void TimeOut(){
		control.TimeOut();
	}
	
	public virtual void UseSkill (int skill){
		Skill.UseSkill(skill);
	}
	
	public virtual void AddNetworkMessageRecipient(INetworkMessage recipient){
		networkInterface.AddMessageRecipient(recipient);
	}
	public virtual void SendChatMessage(string msg){
		networkInterface.SendChatMessage(msg);
	}
	public virtual void Disconnect(){
		networkInterface.Disconnect();
	}
	public virtual void ConnectToServer(HostData game, string password){
		networkInterface.ConnectToServer(game,password);
	}
	public virtual void LaunchServer(bool hasPublicAccess,string gameName){
		networkInterface.LaunchServer(hasPublicAccess,gameName);
	}
	
	public virtual GameState GetMainGameState(){
		return Control.cState;
	}
	public MonoBehaviour GetMonoBehaviour ()
	{
		return (MonoBehaviour) this;
	}
}
