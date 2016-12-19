using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class DropdownEventController : SelectableEventController {

	private Dropdown.DropdownEvent dropdownEvent;

	public void RegisterListener(params UnityAction<int>[] calls) {
		UnityAction<int> call = calls [0];
		for (int i = 1; i < calls.Length; i++) {
			call += calls [i];
		}
		dropdownEvent.AddListener (call);
	}

	public void UnregisterListener(UnityAction<int> call) {
		dropdownEvent.RemoveListener (call);
	}

	public override void UnregisterAllListeners() {
		dropdownEvent.RemoveAllListeners();
	}

	protected override void Awake() {
		base.Awake ();
		dropdownEvent = GetComponent<Dropdown> ().onValueChanged;
	}
}
