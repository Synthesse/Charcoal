using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class ToggleEventController : SelectableEventController {

	private Toggle.ToggleEvent toggleEvent;

	public void RegisterListener(params UnityAction<bool>[] calls) {
		UnityAction<bool> call = calls [0];
		for (int i = 1; i < calls.Length; i++) {
			call += calls [i];
		}
		toggleEvent.AddListener (call);
	}

	public void UnregisterListener(UnityAction<bool> call) {
		toggleEvent.RemoveListener (call);
	}

	public override void UnregisterAllListeners() {
		toggleEvent.RemoveAllListeners();
	}

	protected override void Awake() {
		base.Awake ();
		toggleEvent = GetComponent<Toggle> ().onValueChanged;
	}
}
