using UnityEngine;
using System.Collections;

public class MoveCommand : ICommand {

	private Direction8 moveDirection;
	private bool stand;

	public MoveCommand(Direction8 inputDirection, bool inputStand) {
		moveDirection = inputDirection;
		stand = inputStand;
	}

	public void Execute(BaseUnit unit) {
		if (stand) {
			unit.isMoving = false;
		} else {
			unit.movementDirection = moveDirection;
			unit.isMoving = true;
			if (moveDirection == Direction8.North || moveDirection == Direction8.Northeast || moveDirection == Direction8.Northwest) {
				unit.UpdateFacing (Direction4.North);
			} else if (moveDirection == Direction8.East) {
				unit.UpdateFacing (Direction4.East);
			} else if (moveDirection == Direction8.South || moveDirection == Direction8.Southeast || moveDirection == Direction8.Southwest) {
				unit.UpdateFacing (Direction4.South);
			} else if (moveDirection == Direction8.West) {
				unit.UpdateFacing (Direction4.West);
			}
		}
	}
}
