using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Damsel : BaseUnit {

	public BaseUnit[] carryingUnits;
	private bool beingCarried;

	private int NumberOfCarryingUnits () {
		int i = 0;
		foreach (BaseUnit unit in carryingUnits) {
			if (unit != null)
				i++;
		}
		return i;
	}

	private bool CheckForCarryRedundancy (BaseUnit referenceUnit) {
		foreach (BaseUnit unit in carryingUnits) {
			if (unit == referenceUnit)
				return false;
		}
		return true;
	}

	public bool AddCarryUnit (BaseUnit unit) {
		if (NumberOfCarryingUnits() < 4 && unit is ICanCarry && CheckForCarryRedundancy(unit)) {
			Vector2 carryPoint = unit.transform.position;
			int carryIndex = -1;
			Vector2 currentPositionDamsel = gameObject.transform.position;
			Vector2 currentPositionUnit = unit.transform.position;


			if (carryingUnits [0] == null) {
				carryPoint = new Vector2(currentPositionDamsel.x - 0.25f, currentPositionDamsel.y);
				carryIndex = 0;
			}
			if (carryingUnits [1] == null) {
				Vector2 reference = new Vector2(currentPositionDamsel.x + 0.25f, currentPositionDamsel.y);
				if (carryIndex == -1 || Vector2.Distance(currentPositionUnit, carryPoint) > Vector2.Distance(currentPositionUnit, reference)) {
					carryPoint = reference;
					carryIndex = 1;
				}
			}
			if (carryingUnits [2] == null) {
				Vector2 reference = new Vector2(currentPositionDamsel.x, currentPositionDamsel.y - 0.25f);
				if (carryIndex == -1 || Vector2.Distance(currentPositionUnit, carryPoint) > Vector2.Distance(currentPositionUnit, reference)) {
					carryPoint = reference;
					carryIndex = 2;
				}
			}
			if (carryingUnits [3] == null) {
				Vector2 reference = new Vector2(currentPositionDamsel.x, currentPositionDamsel.y + 0.25f);
				if (carryIndex == -1 || Vector2.Distance(currentPositionUnit, carryPoint) > Vector2.Distance(currentPositionUnit, reference)) {
					carryPoint = reference;
					carryIndex = 3;
				}
			}

			unit.transform.position = carryPoint;
			carryingUnits [carryIndex] = unit;

			MoveCommand moveCommand = new MoveCommand (Direction8.West, false);

			if (NumberOfCarryingUnits () == 1) {
				queuedCommand = moveCommand;
				beingCarried = false;
			}
			ICanCarry castedUnit = unit as ICanCarry;
			castedUnit.ReceivePushedMoveCommand (moveCommand);
			SetCarryMoveSpeed ();

			return true;
		}
		return false;
	}

	public void RemoveCarryUnit(BaseUnit unit) {
		for (int i = 0; i < 4; i++) {
			if (carryingUnits [i] == unit) {
				carryingUnits [i] = null;
			}
		}
		if (NumberOfCarryingUnits () == 0) {
			queuedCommand = new MoveCommand (Direction8.South, true);
			beingCarried = false;
		}
		SetCarryMoveSpeed ();
	}

	private void SetCarryMoveSpeed() {
		float newMovespeed = baseMoveSpeed * (NumberOfCarryingUnits ()+1)*0.2f;
		currentMoveSpeed = newMovespeed;
		foreach (BaseUnit unit in carryingUnits) {
			if (unit != null) {
				unit.currentMoveSpeed = newMovespeed;
			}
		}
	}

	public override void UpdateFacing (Direction4 newFacing) {
		facing = newFacing;
	}

	protected override void ExecuteQueuedCommands() {
		if (queuedCommand != null) {
			queuedCommand.Execute (this);
//			if (queuedCommand is MoveCommand) {
//				MoveCommand moveCommand = queuedCommand as MoveCommand;
//				foreach (BaseUnit carryingUnit in carryingUnits) {
//					if (carryingUnit != null) {
//						ICanCarry castedUnit = carryingUnit as ICanCarry;
//						castedUnit.ReceivePushedMoveCommand (moveCommand);
//					}
//				}
//			}
			queuedCommand = null;
		} else {
			Move ();
		}
	}

	public override void Move() {
		if (isMoving) {
			float speedConstant = Time.fixedDeltaTime * currentMoveSpeed;
			//				transform.position += TranslateDirectiontoVector3(movementDirection) * speedConstant;
			rb2D.MovePosition (transform.position + TranslateDirectiontoVector3 (movementDirection) * speedConstant);

		}
	}

	public override void Hurt() {
	}



	// Use this for initialization
	void Start () {
		carryingUnits = new BaseUnit[4];
		base.ProtectedStart ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		base.ProtectedUpdate ();
	}
}
