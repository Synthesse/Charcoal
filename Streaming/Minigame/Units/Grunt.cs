using UnityEngine;
using System.Collections;

public class Grunt : BaseUnit, ICanCarry {

	public bool isCarrying { get; private set;}
	public Gate targetGate;
	public Direction8 spawnDirection;

//	public void SetWall (Gate wall) {
//		targetGate = wall;
//	}

	public void Carry (Damsel damsel) {
		if (damsel.AddCarryUnit (this)) {
			rb2D.bodyType = RigidbodyType2D.Kinematic;
			isCarrying = true;
		}
	}

	public void Drop(Damsel damsel) {
		damsel.RemoveCarryUnit (this);
		rb2D.bodyType = RigidbodyType2D.Dynamic;
	}

	public void ReceivePushedMoveCommand (MoveCommand moveCommand) {
		moveCommand.Execute (this);
	}

	public Direction8 GetSpawnDirection() {
		return spawnDirection;
	}

	public override void Kill ()
	{
		if (isCarrying) {
			Drop(GameManager.instance.miniGameManager.damsel);
		}
		base.Kill ();
	}

	void Start () {
		base.ProtectedStart ();
		isCarrying = false;
		allegiance = Allegiance.Wizard;
		animator.SetFloat ("SlashSpeed", 0.75f);
	}

	void FixedUpdate () {
		base.ProtectedUpdate ();
	}
}
