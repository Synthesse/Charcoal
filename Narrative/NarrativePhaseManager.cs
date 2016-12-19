using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NarrativePhaseManager : MonoBehaviour {

	public Button narrativeButton;
	private bool skipPreStreamNarrativePhase = true;
	private GameManager gameManager;

	public void InitializeNarrativePhase() {
		if (gameManager == null) {
			gameManager = GameManager.instance;
		}
		if (gameManager.phaseManager.currentGamePhase == GamePhase.NarrativePreStream) {
			if (skipPreStreamNarrativePhase) {
				gameManager.phaseManager.PhaseTransition (GamePhase.Streaming);
			}
		}
	}
		
	private void EndNarrative() {
		if (gameManager.phaseManager.currentGamePhase == GamePhase.NarrativePreStream) {
			gameManager.phaseManager.PhaseTransition (GamePhase.Streaming);
		} else {
			gameManager.phaseManager.PhaseTransition (GamePhase.Planning);
		}
	}

	// Use this for initialization
	void Awake () {
		gameManager = GameManager.instance;
	}

	void Start() {
		narrativeButton.GetComponent<IListenedTo>().RegisterListener(() => EndNarrative());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
