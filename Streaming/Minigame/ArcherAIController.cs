using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ArcherAIController : MonoBehaviour {

	private Seeker attachedSeeker;
	private int currentWaypoint = 0;
	private Path path;
	private Vector3 targetPosition;
	private BaseUnit targetUnit;

	private Archer attachedArcher;

	private float AIDelay = 0.05f;
	private float timeOfLastCommand = 0f;

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
		float xComponent = path.vectorPath [currentWaypoint].x - attachedArcher.transform.position.x;
		float yComponent = path.vectorPath [currentWaypoint].y - attachedArcher.transform.position.y;
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
		float xComponent = vec.x - attachedArcher.transform.position.x;
		float yComponent = vec.y - attachedArcher.transform.position.y;
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

	private void TargetKnight() {
		targetUnit = FindObjectOfType<Knight> () as BaseUnit;
	}

	void Start () {
		attachedArcher = this.gameObject.GetComponent<Archer> ();
		attachedSeeker = this.gameObject.GetComponent<Seeker> ();
		if (attachedArcher.targetGate != null) {
			targetUnit = attachedArcher.targetGate;
			attachedArcher.targetGate.RegisterListener (() => TargetKnight ());
		} else {
			TargetKnight ();
		}
		targetPosition = targetUnit.transform.position;
		GameManager.instance.miniGameManager.gameObject.GetComponent<PathfindingEventController> ().RegisterListener(() => RecalculatePath());
	}

	void Update () {
		if (Time.time - timeOfLastCommand > AIDelay && !attachedArcher.animator.GetCurrentAnimatorStateInfo (0).IsTag ("Ability") && !attachedArcher.nonAnimatedAbilityInProgress) {
			timeOfLastCommand = Time.time;
			targetPosition = targetUnit.transform.position;
			BaseUnit linecastUnit = Physics2D.Linecast (attachedArcher.transform.position, targetPosition, 1 << 11 | 1 << 9).collider.gameObject.GetComponent<BaseUnit> ();
			if (linecastUnit != null && linecastUnit.allegiance == Allegiance.Knight) {
				attachedArcher.UpdateFacing (DirectionToTarget (targetPosition));
				attachedArcher.queuedCommand = new ShootCommand (targetPosition);
			} else {
				if (path != null && path.vectorPath.Count > 0 && currentWaypoint < path.vectorPath.Count) {
					Direction8 waypointDirection = PathWaypointToDirection ();
					if (!attachedArcher.isMoving || attachedArcher.movementDirection != waypointDirection) {
						attachedArcher.queuedCommand = new MoveCommand (PathWaypointToDirection (), false);
					}
					if (Vector2.Distance (path.vectorPath [currentWaypoint], attachedArcher.transform.position) < 0.05f) {
						currentWaypoint++;
					}
				}
			}
		}
	}
}
