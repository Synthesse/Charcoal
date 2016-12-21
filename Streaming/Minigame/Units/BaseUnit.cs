using UnityEngine;
using System.Collections;

public class BaseUnit : MonoBehaviour {

	protected int hitPoints;
	protected int defense;
	protected int moveSpeed = 1;
	protected Allegiance allegiance;
	public Direction4 facing;
	public Direction8 movementDirection;
	public bool isMoving = false;
	public ICommand queuedCommand;
	public ICommandController commandController;
	public SpriteRenderer spriteRenderer;
	public Animator animator;


	// Use this for initialization
	protected void Initialize() {
		spriteRenderer = this.gameObject.GetComponent<SpriteRenderer> ();
		animator = this.gameObject.GetComponent<Animator> ();
	}

	protected void ExecuteQueuedCommands() {
		if (queuedCommand != null) {
			queuedCommand.Execute (this);
//			if (queuedCommand is MoveCommand) {
//				Move ();
//			}
			queuedCommand = null;
		} else {
			Move ();
		}
	}
		
	public void UpdateFacing(Direction4 newFacing) {
		facing = newFacing;
		animator.SetInteger ("Facing", (int)facing);
	}

	private Vector3 TranslateDirectiontoVector3 (Direction8 dir) {
		Vector3 vec = new Vector3 (0, 0, 0);

		if (dir == Direction8.North) {
			vec = new Vector3 (0, 1, 0);
		} else if (dir == Direction8.Northeast) {
			vec = new Vector3 (1 / Mathf.Sqrt (2), 1 / Mathf.Sqrt (2), 0);
		} else if (dir == Direction8.East) {
			vec = new Vector3 (1, 0, 0);
		} else if (dir == Direction8.Southeast) {
			vec = new Vector3 (1 / Mathf.Sqrt (2), -1 / Mathf.Sqrt (2), 0);
		} else if (dir == Direction8.South) {
			vec = new Vector3 (0, -1, 0);
		} else if (dir == Direction8.Southwest) {
			vec = new Vector3 (-1 / Mathf.Sqrt (2), -1 / Mathf.Sqrt (2), 0);
		} else if (dir == Direction8.West) {
			vec = new Vector3 (-1, 0, 0);
		} else if (dir == Direction8.Northwest) {
			vec = new Vector3 (-1 / Mathf.Sqrt (2), 1 / Mathf.Sqrt (2), 0);
		} 

		return vec;
	}

	public void Move() {
		if (!animator.GetCurrentAnimatorStateInfo (0).IsTag ("Ability")) {
			if (isMoving) {
				if (facing == Direction4.North && !animator.GetCurrentAnimatorStateInfo (0).IsName ("WalkUp")) {
					animator.SetTrigger ("WalkUp");
				} else if (facing == Direction4.West && !animator.GetCurrentAnimatorStateInfo (0).IsName ("WalkLeft")) {
					animator.SetTrigger ("WalkLeft");
				} else if (facing == Direction4.East && !animator.GetCurrentAnimatorStateInfo (0).IsName ("WalkRight")) {
					animator.SetTrigger ("WalkRight");
				} else if (facing == Direction4.South && !animator.GetCurrentAnimatorStateInfo (0).IsName ("WalkDown")) {
					animator.SetTrigger ("WalkDown");
				}
				float speedConstant = Time.deltaTime * moveSpeed;
				transform.position += TranslateDirectiontoVector3(movementDirection) * speedConstant;
					
			} else {
				if (!animator.GetCurrentAnimatorStateInfo (0).IsTag ("Idle") && !animator.GetCurrentAnimatorStateInfo (0).IsTag ("Interruptable"))
					animator.SetTrigger ("Stand");
				
			}
		}
	}

	public void Hurt() {
		animator.SetTrigger ("Hurt");
		Debug.Log ("Hurt");
	}

	void Start () {
		Initialize ();
	}

	void Update () {
		ExecuteQueuedCommands ();
	}
}
