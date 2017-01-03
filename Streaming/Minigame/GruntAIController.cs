using UnityEngine;
using System.Collections;

public enum GruntAIMode {
	Initial,
	BreakingGate,
	MovingToPrincess,
	CarryingPrincessAway
}

public class GruntAIController : MonoBehaviour, ICommandController {

	private Grunt attachedGrunt;
	private Seeker attachedSeeker;
	private Damsel damsel;

	private GruntAIMode currentMode;

	void Start () {
		attachedGrunt = this.gameObject.GetComponent<Grunt> ();
		attachedSeeker = this.gameObject.GetComponent<Seeker> ();
		currentMode = GruntAIMode.MovingToPrincess;
		damsel = GameManager.instance.miniGameManager.damsel;
		attachedGrunt.queuedCommand = new MoveCommand (Direction8.North, false);
	}
		
	// Grunt AI Priority: Move to wall -> Break Wall -> Move to princess and carry if under carry limit, otherwise move to hero and attack; if carrying princess move to nearest exit
	void Update () {
		if (!attachedGrunt.isCarrying) {
			if (Vector2.Distance(damsel.transform.position, gameObject.transform.position) < 0.8f) {
				attachedGrunt.queuedCommand = new CarryCommand ();
			} else {
				
			}
		} else {
			//Wait for pushed move command
		}
	}
}
