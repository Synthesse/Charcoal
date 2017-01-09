using UnityEngine;
using System.Collections;

public class Archer : BaseUnit, ICanShoot {

	public Projectile projectilePrefab;
	public Gate targetGate;

	public void Shoot() {
		//		Use this code for 4 directional projectiles
		Projectile projectile = Instantiate (projectilePrefab, transform.position + TranslateDirectiontoVector3 (facing)*0.25f, Quaternion.identity) as Projectile;
		projectile.transform.SetParent (transform);
		projectile.SetVelocity (TranslateDirectiontoVector3 (facing));
		projectile.allegiance = allegiance;
	}

	public void Shoot(Vector3 target) {
		Vector3 directionVec = (target - transform.position).normalized;
		Projectile projectile = Instantiate (projectilePrefab, transform.position + directionVec*0.25f, Quaternion.identity) as Projectile;
		projectile.transform.SetParent (transform);
		projectile.SetVelocity (directionVec);
		projectile.allegiance = allegiance;
	}

	void Start () {
		base.ProtectedStart ();
		allegiance = Allegiance.Wizard;
		animator.SetFloat ("SlashSpeed", 0.2f);
	}

	void Update () {
		base.ProtectedUpdate ();
	}
}
