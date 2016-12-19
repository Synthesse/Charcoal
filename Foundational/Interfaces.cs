using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public interface IListenedTo {
	void RegisterListener(params UnityAction[] calls);
	void UnregisterListener (UnityAction call);
	void UnregisterAllListeners ();
}