using UnityEngine;
using System.Collections;

public class PhaseManager : MonoBehaviour {

	public PlanningPhaseManager planningPhaseManager;
	public NarrativePhaseManager narrativePhaseManager;
	public StreamingPhaseManager streamingPhaseManager;

	public GamePhase currentGamePhase;

	public void PhaseTransition(GamePhase phase) {
		DeactivatePhase ();
		ActivatePhase (phase);
	}

	private void DeactivatePhase() {
		if (currentGamePhase == GamePhase.Planning) {
			planningPhaseManager.gameObject.SetActive (false);
		} else if (currentGamePhase == GamePhase.NarrativePreStream || currentGamePhase == GamePhase.NarrativePostStream) {
			narrativePhaseManager.gameObject.SetActive (false);
		} else if (currentGamePhase == GamePhase.Streaming) {
			streamingPhaseManager.gameObject.SetActive (false);
		}
		currentGamePhase = GamePhase.None;
	}

	private void ActivatePhase(GamePhase phase) {
		currentGamePhase = phase;

		if (phase == GamePhase.Planning) {
			planningPhaseManager.gameObject.SetActive (true);
			planningPhaseManager.InitializePlanningPhase ();
		} else if (phase == GamePhase.NarrativePreStream || phase == GamePhase.NarrativePostStream) {
			narrativePhaseManager.gameObject.SetActive (true);
			narrativePhaseManager.InitializeNarrativePhase ();
		} else if (phase == GamePhase.Streaming) {
			streamingPhaseManager.gameObject.SetActive (true);
			streamingPhaseManager.InitializeStreamingPhase ();
		}
	}

	// Use this for initialization
	void Start () {
		narrativePhaseManager.gameObject.SetActive (false);
		planningPhaseManager.gameObject.SetActive (false);
		currentGamePhase = GamePhase.Streaming;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
