using UnityEngine;
using System.Collections;

public class ClockToken : MonoBehaviour {

	private ScheduleContainer currentBucket;

	// Use this for initialization
	void Start () {
	
	}


	void OnMouseDrag() {
		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		this.transform.position = new Vector3 (pos.x, pos.y, 0);
	}


	// Update is called once per frame
	void Update () {
	
	}
}
