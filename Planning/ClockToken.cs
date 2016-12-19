using UnityEngine;
using System.Collections;

public class ClockToken : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

//	public void OnDrag(){
//		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
//		this.transform.position = new Vector3 (pos.x, pos.y, 0);
//	}

	void OnMouseDrag() {
		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		this.transform.position = new Vector3 (pos.x, pos.y, 0);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
