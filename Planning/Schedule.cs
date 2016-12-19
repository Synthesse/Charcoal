using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Schedule : MonoBehaviour {

	public Button scheduleConfirmButton;

	private void ConfirmSchedule() {
		GameManager.instance.phaseManager.PhaseTransition (GamePhase.NarrativePreStream);
	}

	// Use this for initialization
	void Start () {
		scheduleConfirmButton.GetComponent<IListenedTo>().RegisterListener(() => ConfirmSchedule());
	}
}
