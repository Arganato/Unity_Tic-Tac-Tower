using UnityEngine;
using System.Collections;

public interface INetworkMessage {

	void ConnectionStatus(ConnectionMessage msg);
	void ChatMessage(string msg);
	void StartGameMessage();
}
