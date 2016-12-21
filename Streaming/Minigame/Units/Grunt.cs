using UnityEngine;
using System.Collections;

public class Grunt : BaseUnit {

	void Start () {
		base.Initialize ();
	}

	void Update () {
		base.ExecuteQueuedCommands ();
	}
}
