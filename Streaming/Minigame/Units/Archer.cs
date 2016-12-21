using UnityEngine;
using System.Collections;

public class Archer : BaseUnit {

	void Start () {
		base.Initialize ();
	}

	void Update () {
		base.ExecuteQueuedCommands ();
	}
}
