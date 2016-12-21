using UnityEngine;
using System.Collections;

public class Knight : BaseUnit {



	void Start () {
		base.Initialize ();
	}

	void Update () {
		base.ExecuteQueuedCommands ();
	}
}
