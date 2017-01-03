using UnityEngine;
using System.Collections;

public class Wizard : BaseUnit, ICanShoot {

	public Projectile projectilePrefab;
	private float time;

	public void Shoot() {
		Projectile projectile = Instantiate (projectilePrefab, transform.position + TranslateDirectiontoVector3 (facing)*0.25f, Quaternion.identity) as Projectile;
		projectile.SetVelocity (TranslateDirectiontoVector3 (facing));
	}

	void Start () {
		base.ProtectedStart ();
		time = Time.time;
	}

	void Update () {
		base.ProtectedUpdate ();
	}

	void FixedUpdate() {
//		if (Time.time - time > 2) {
//			Shoot ();
//			time = Time.time;
//		}

//		float speedConstant = Time.fixedDeltaTime * moveSpeed;
//		rb2D.MovePosition(transform.position + base.TranslateDirectiontoVector3(Direction8.North) * speedConstant);
	}
}
