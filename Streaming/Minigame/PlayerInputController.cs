using UnityEngine;
using System.Collections;

public class PlayerInputController : MonoBehaviour, ICommandController {

	private BaseUnit attachedUnit;

	private bool MovementKeyChange() {
		return (Input.GetKeyDown(KeyCode.W) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyUp(KeyCode.D));
	}

	// Use this for initialization
	void Start () {
		attachedUnit = this.gameObject.GetComponent<BaseUnit> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			attachedUnit.queuedCommand = new SlashCommand ();
		
		} else if (MovementKeyChange()) {
			if (Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.A)) {
				attachedUnit.queuedCommand = new MoveCommand (Direction8.Northwest, false);
			} else if (Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.D)) {
				attachedUnit.queuedCommand = new MoveCommand (Direction8.Northeast, false);
			} else if (Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.A)) {
				attachedUnit.queuedCommand = new MoveCommand (Direction8.Southwest, false);
			} else if (Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.D)) {
				attachedUnit.queuedCommand = new MoveCommand (Direction8.Southeast, false);
			} else if (Input.GetKey (KeyCode.W)) {
				attachedUnit.queuedCommand = new MoveCommand (Direction8.North, false);
			} else if (Input.GetKey (KeyCode.S)) {
				attachedUnit.queuedCommand = new MoveCommand (Direction8.South, false);
			} else if (Input.GetKey (KeyCode.A)) {
				attachedUnit.queuedCommand = new MoveCommand (Direction8.West, false);
			} else if (Input.GetKey (KeyCode.D)) {
				attachedUnit.queuedCommand = new MoveCommand (Direction8.East, false);
			} else {
				attachedUnit.queuedCommand = new MoveCommand (Direction8.South, true);
			}
		}

//			
	}
}
