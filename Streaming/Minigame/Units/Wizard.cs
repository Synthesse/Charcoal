using UnityEngine;
using System.Collections;

public class Wizard : BaseUnit, ICanShoot {

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


	public override void Kill () {
		GameManager.instance.miniGameManager.GameOver ();
	}

	void Start () {
		base.ProtectedStart ();
		allegiance = Allegiance.Wizard;
		hitPoints = 40;
		animator.SetFloat ("SlashSpeed", 0.4f);
	}

	void Update () {
		base.ProtectedUpdate ();
	}

	void FixedUpdate() {
		
//		float speedConstant = Time.fixedDeltaTime * moveSpeed;
//		rb2D.MovePosition(transform.position + base.TranslateDirectiontoVector3(Direction8.North) * speedConstant);
	}
}
