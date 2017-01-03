using UnityEngine;
using System.Collections;

public class Knight : BaseUnit {


	public override void Kill ()
	{
		GameManager.instance.miniGameManager.GameOver ();
	}

	void Start () {
		base.ProtectedStart ();
	}

	void FixedUpdate () {
		base.ProtectedUpdate ();
	}
}
