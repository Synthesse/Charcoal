using UnityEngine;
using System.Collections;

public class Grunt : BaseUnit, ICanCarry {

	public bool isCarrying { get; private set;}
	public Gate targetGate { get; private set;}

	public void SetWall (Gate wall) {
		targetGate = wall;
	}

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
	}

	void FixedUpdate () {
		base.ProtectedUpdate ();
	}
}
