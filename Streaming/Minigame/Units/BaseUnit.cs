using UnityEngine;
using System.Collections;

public class BaseUnit : MonoBehaviour {

	public int hitPoints = 10;
	protected int defense;
	protected float baseMoveSpeed = 0.7f;
	public float currentMoveSpeed = 0.7f;
	public Allegiance allegiance;
	public Direction4 facing;
	public Direction8 movementDirection;
	public bool isMoving = false;
	public ICommand queuedCommand;
	public ICommandController commandController;
	public SpriteRenderer spriteRenderer;
	public Animator animator;
	public Rigidbody2D rb2D;
	public bool nonAnimatedAbilityInProgress = false;


	protected virtual void ExecuteQueuedCommands() {
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
		
	public virtual void UpdateFacing(Direction4 newFacing) {
		facing = newFacing;
		animator.SetInteger ("Facing", (int)facing);
	}

	protected Vector3 TranslateDirectiontoVector3 (Direction4 dir) {
		Vector3 vec = new Vector3 (0, 0, 0);

		if (dir == Direction4.North) {
			vec = new Vector3 (0, 1, 0);
		} else if (dir == Direction4.East) {
			vec = new Vector3 (1, 0, 0);
		} else if (dir == Direction4.South) {
			vec = new Vector3 (0, -1, 0);
		} else if (dir == Direction4.West) {
			vec = new Vector3 (-1, 0, 0);
		} 

		return vec;
	}

	protected Vector3 TranslateDirectiontoVector3 (Direction8 dir) {
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

	public virtual void Move() {
		if (!animator.GetCurrentAnimatorStateInfo (0).IsTag ("Ability") && !nonAnimatedAbilityInProgress) {
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
				float speedConstant = Time.fixedDeltaTime * currentMoveSpeed;
//				transform.position += TranslateDirectiontoVector3(movementDirection) * speedConstant;
				rb2D.MovePosition(transform.position + TranslateDirectiontoVector3(movementDirection) * speedConstant);
					
			} else {
				if (!animator.GetCurrentAnimatorStateInfo (0).IsTag ("Idle") && !animator.GetCurrentAnimatorStateInfo (0).IsTag ("Interruptable"))
					animator.SetTrigger ("Stand");
				
			}
		}
	}

	public virtual void Hurt(Allegiance sourceAllegiance) {
		if (sourceAllegiance != allegiance) {
			animator.SetTrigger ("Hurt");
			hitPoints -= 5;
		}
	}

	public virtual void Hurt(Allegiance sourceAllegiance, int loss) {
		if (sourceAllegiance != allegiance) {
			animator.SetTrigger ("Hurt");
			hitPoints -= loss;
		}
	}

	public virtual void Kill() {
		if (allegiance == Allegiance.Wizard) {
			GameManager.instance.miniGameManager.RemoveWizardUnitFromList (this);
		}
		Destroy (gameObject);
	}

	protected void ProtectedStart() {
		spriteRenderer = this.gameObject.GetComponent<SpriteRenderer> ();
		animator = this.gameObject.GetComponent<Animator> ();
		rb2D = this.gameObject.GetComponent<Rigidbody2D> ();
		facing = Direction4.South;
	}

	void Start () {
		ProtectedStart ();
	}

	protected void ProtectedUpdate() {
		if (hitPoints <= 0) {
			Kill ();
		}
		ExecuteQueuedCommands ();
	}

	void FixedUpdate () {
		ProtectedUpdate ();
	}
}
