using UnityEngine;
using System.Collections;

public interface IGUIMessages{
	
	void UserEndTurn();
	void UserResign();
	void UndoTurn();
	void QuitGame();
	void UserFieldSelect(FieldIndex position);
	void TimeOut();
	
	void AddNetworkMessageRecipient(INetworkMessage recipient);
	void SendChatMessage(string msg);
	void Disconnect();
	void ConnectToServer(HostData game, string password);
	void LaunchServer(bool hasPublicAccess,string gameName);
	
	GameState GetMainGameState();
	

}
