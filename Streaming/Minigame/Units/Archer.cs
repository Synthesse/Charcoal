using UnityEngine;
using System.Collections;

public class Archer : BaseUnit {

	public Projectile projectilePrefab;

	public void Shoot() {
		Projectile projectile = Instantiate (projectilePrefab, transform.position + TranslateDirectiontoVector3 (facing)*0.25f, Quaternion.identity) as Projectile;
		projectile.SetVelocity (TranslateDirectiontoVector3 (facing));
	}

	void Start () {
		base.ProtectedStart ();
	}

	void Update () {
		base.ProtectedUpdate ();
	}
}
