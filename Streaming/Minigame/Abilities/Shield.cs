using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shield : MonoBehaviour, IListenedTo {

	private float blockDuration = 0.75f;
	private float timeToDespawn;
	protected UnityEvent deathEvent;

	public void RegisterListener(params UnityAction[] calls) {
		UnityAction call = calls [0];
		for (int i = 1; i < calls.Length; i++) {
			call += calls [i];
		}
		deathEvent.AddListener (call);
	}

	public void UnregisterListener(UnityAction call) {
		deathEvent.RemoveListener (call);
	}

	public void UnregisterAllListeners() {
		deathEvent.RemoveAllListeners();
	}

	void Awake() {
		deathEvent = new UnityEvent ();
	}

	// Use this for initialization
	void Start () {
		timeToDespawn = Time.time + blockDuration;
	}
	
	// Update is called once per frame
	void Update () {
		if (timeToDespawn <= Time.time) {
			deathEvent.Invoke ();
			UnregisterAllListeners ();
			Destroy (gameObject);
		}
	}
}
