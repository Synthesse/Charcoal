using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	private float speed = 1f;
	public Allegiance allegiance;

	public void SetVelocity(Vector3 vec) {
		GetComponent<Rigidbody2D> ().velocity = vec * speed;
	}

	void OnTriggerEnter2D(Collider2D otherCollider) {
		BaseUnit hitUnit = otherCollider.GetComponent<BaseUnit> ();
		if (hitUnit != null) {
			hitUnit.Hurt (allegiance);
			if (allegiance == hitUnit.allegiance) {
				return;
			}
		}
		Destroy (gameObject);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
