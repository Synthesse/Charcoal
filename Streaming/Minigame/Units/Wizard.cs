using UnityEngine;
using System.Collections;

public class Wizard : BaseUnit {

	void Start () {
		base.Initialize ();
	}

	void Update () {
		base.ExecuteQueuedCommands ();
	}
}
