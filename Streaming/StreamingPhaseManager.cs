using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StreamingPhaseManager : MonoBehaviour {

	public GameObject StreamUI;
	public Button EndStreamingButton;

	public StreamChatController streamChatController;
	public StreamTimeController streamTimeController;

	public void InitializeStreamingPhase() {
		streamTimeController.StartTimer ();
	}

	public void EndStreaming() {
		GameManager.instance.phaseManager.PhaseTransition (GamePhase.NarrativePostStream);
	}

	// Use this for initialization
	void Start () {
		EndStreamingButton.GetComponent<IListenedTo>().RegisterListener(() => EndStreaming());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
