using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public abstract class SelectableEventController : MonoBehaviour, IListenedTo {

	protected Selectable thisSelectable;
	protected UnityEvent selectableEvent;

	public virtual void RegisterListener(params UnityAction[] calls) {
		UnityAction call = calls [0];
		for (int i = 1; i < calls.Length; i++) {
			call += calls [i];
		}
		selectableEvent.AddListener (call);
	}

	public virtual void UnregisterListener(UnityAction call) {
		selectableEvent.RemoveListener (call);
	}

	public virtual void UnregisterAllListeners() {
		selectableEvent.RemoveAllListeners();
	}

	protected virtual void Awake () {
		thisSelectable = gameObject.GetComponent<Selectable>();
	}

}