using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PathfindingEventController : MonoBehaviour, IListenedTo {

	protected UnityEvent seekingEvent;
	private const float repathRate = 0.5f;
	private float lastRepath = -999f;
	private AstarPath pathfinder;

	public void RegisterListener(params UnityAction[] calls) {
		UnityAction call = calls [0];
		for (int i = 1; i < calls.Length; i++) {
			call += calls [i];
		}
		seekingEvent.AddListener (call);
	}

	public void UnregisterListener(UnityAction call) {
		seekingEvent.RemoveListener (call);
	}

	public void UnregisterAllListeners() {
		seekingEvent.RemoveAllListeners();
	}

	void Awake() {
		seekingEvent = new UnityEvent ();
	}

	// Use this for initialization
	void Start () {
		pathfinder = gameObject.GetComponent<AstarPath> ();
	}

	private IEnumerator TriggerRepath() {
		yield return new WaitWhile (() => pathfinder.isScanning);
		seekingEvent.Invoke ();
		lastRepath = Time.time;
	}

	// Update is called once per frame
	void Update () {
		if (Time.time - lastRepath > repathRate && !pathfinder.isScanning) {
			pathfinder.Scan ();
			StartCoroutine (TriggerRepath());
		}
	}
}
