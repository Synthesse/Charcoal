using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StreamTimeController : MonoBehaviour {

	public StreamingPhaseManager streamingPhaseManager;

	public Text streamTimerText;
	private float expireTime = 0f;
	public Button addTimeButton;

	public void StartTimer() {
		expireTime = Time.time;
		AddStreamTime (20);
	}

	public void AddStreamTime (int timeAdded) {
		expireTime += timeAdded;
	}


	// Use this for initialization
	void Start () {
		addTimeButton.GetComponent<IListenedTo>().RegisterListener(() => AddStreamTime(5));
	}

	void Update () {
		if (expireTime > 0) {
			if (Time.time > expireTime) {
				expireTime = 0f;
				streamingPhaseManager.EndStreaming ();
			} else {
				int seconds = (int)(expireTime - Time.time) % 60;
				int minutes = Mathf.FloorToInt ((expireTime - Time.time) / 60);
				streamTimerText.text = "Time Left:\n" + minutes.ToString () + ":" + seconds.ToString ();
			}
		}
	}
}
