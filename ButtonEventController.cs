using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class ButtonEventController : SelectableEventController {


	// Causing non-deterministic errors on awake?
	protected override void Awake() {
		base.Awake ();
		selectableEvent = gameObject.GetComponent<Button> ().onClick;
	}

//	private Button.ButtonClickedEvent buttonEvent;
//
//	public void RegisterListener(params UnityAction[] calls) {
//		UnityAction call = calls [0];
//		for (int i = 1; i < calls.Length; i++) {
//			call += calls [i];
//		}
//		buttonEvent.AddListener (call);
//	}
//
//	public void UnregisterListener(UnityAction call) {
//		buttonEvent.RemoveListener (call);
//	}
//
//	public void UnregisterAllListeners() {
//		buttonEvent.RemoveAllListeners();
//	}
//
//	void Awake () {
//		buttonEvent = gameObject.GetComponent<Button> ().onClick;
//	}

}