using UnityEngine;
using System.Collections;
using Pathfinding;

public enum GruntAIMode {
	Initial,
	BreakingGate,
	MovingToPrincess,
	CarryingPrincessAway
}

public class GruntAIController : MonoBehaviour, ICommandController {

	//TODO: Separate Seeking methods/variables into their own component
	private Seeker attachedSeeker;
	private int currentWaypoint = 0;
	private Path path;
	private Vector3 targetPosition;
	private BaseUnit targetUnit;

	private Grunt attachedGrunt;

	private float AIDelay = 0.05f;
	private float timeOfLastCommand = 0f;
	private GruntAIMode currentMode;

	public void OnPathComplete(Path p) {
		if (!p.error) {
			currentWaypoint = 0;
			path = p;
		}
	}

	private void RecalculatePath() {
		if (attachedSeeker.IsDone ()) {
			attachedSeeker.StartPath (transform.position, targetPosition, OnPathComplete);
		}
	}

	//Revise two methods below to something more generic?
	private Direction8 PathWaypointToDirection() {
		float xComponent = path.vectorPath [currentWaypoint].x - attachedGrunt.transform.position.x;
		float yComponent = path.vectorPath [currentWaypoint].y - attachedGrunt.transform.position.y;
		float angle = Mathf.Atan2 (yComponent, xComponent);



		if (angle <= -7 * Mathf.PI / 8f) {
			return Direction8.West;
		} else if (angle < -5 * Mathf.PI / 8f) {
			return Direction8.Southwest;
		} else if (angle <= -3 * Mathf.PI / 8f) {
			return Direction8.South;
		} else if (angle < -1 * Mathf.PI / 8f) {
			return Direction8.Southeast;
		} else if (angle <= 1 * Mathf.PI / 8f) {
			return Direction8.East;
		} else if (angle < 3 * Mathf.PI / 8f) {
			return Direction8.Northeast;
		} else if (angle <= 5 * Mathf.PI / 8f) {
			return Direction8.North;
		} else if (angle < 7 * Mathf.PI / 8f) {
			return Direction8.Northwest;
		} else {
			return Direction8.West;
		}

	}

	private Direction4 DirectionToTarget(Vector3 vec) {
		float xComponent = vec.x - attachedGrunt.transform.position.x;
		float yComponent = vec.y - attachedGrunt.transform.position.y;
		float angle = Mathf.Atan2 (yComponent, xComponent);

		if (angle < -3 * Mathf.PI / 4f) {
			return Direction4.West;
		} else if (angle <= -1 * Mathf.PI / 4f) {
			return Direction4.South;
		} else if (angle < 1 * Mathf.PI / 4f) {
			return Direction4.East;
		} else if (angle <= 3 * Mathf.PI / 4f) {
			return Direction4.North;
		} else {
			return Direction4.West;
		}
	}

	private void TargetPrincess() {
		currentMode = GruntAIMode.MovingToPrincess;
		targetUnit = GameManager.instance.miniGameManager.damsel;
	}

	void Start () {
		attachedGrunt = this.gameObject.GetComponent<Grunt> ();
		attachedSeeker = this.gameObject.GetComponent<Seeker> ();
		currentMode = GruntAIMode.BreakingGate;
		if (attachedGrunt.targetGate != null) {
			targetUnit = attachedGrunt.targetGate;
			attachedGrunt.targetGate.RegisterListener (() => TargetPrincess ());
		} else {
			TargetPrincess ();
		}
		targetPosition = targetUnit.transform.position;
		GameManager.instance.miniGameManager.gameObject.GetComponent<PathfindingEventController> ().RegisterListener(() => RecalculatePath());

	}
		
	// Grunt AI Priority: Move to wall -> Break Wall -> Move to princess and carry if under carry limit, otherwise move to hero and attack; if carrying princess move to nearest exit
	void Update () {
		if (Time.time - timeOfLastCommand > AIDelay && !attachedGrunt.animator.GetCurrentAnimatorStateInfo (0).IsTag ("Ability") && !attachedGrunt.nonAnimatedAbilityInProgress) {
			timeOfLastCommand = Time.time;
			if (!attachedGrunt.isCarrying) {
				Collider2D[] struckColliders = Physics2D.OverlapCircleAll (attachedGrunt.transform.position, 0.25f);
				BaseUnit heldUnit = null;
				foreach (Collider2D col in struckColliders) {
					BaseUnit unit = col.gameObject.GetComponent<BaseUnit> ();
					if (unit != null) {
						if (unit.allegiance == Allegiance.Damsel) {
							attachedGrunt.queuedCommand = new CarryCommand ();
							return;
						} else if (heldUnit == null && unit.allegiance == Allegiance.Knight) {
							heldUnit = unit;
						}
					}
				}

				if (heldUnit != null) {
					attachedGrunt.UpdateFacing (DirectionToTarget (heldUnit.transform.position));
					attachedGrunt.queuedCommand = new SlashCommand ();
					Debug.Log ("Slash");
				} else {
					targetPosition = targetUnit.transform.position;
					if (path != null && path.vectorPath.Count > 0 && currentWaypoint < path.vectorPath.Count) {
						Direction8 waypointDirection = PathWaypointToDirection ();
						if (!attachedGrunt.isMoving || attachedGrunt.movementDirection != waypointDirection) {
							attachedGrunt.queuedCommand = new MoveCommand (PathWaypointToDirection (), false);
						}
						if (Vector2.Distance (path.vectorPath [currentWaypoint], attachedGrunt.transform.position) < 0.05f) {
							currentWaypoint++;
						}
					}
				}
			} else {
				//Wait for pushed move command
			}
		}
	}
}
