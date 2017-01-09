using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCommand : ICommand {

	private Vector3 target;

	public ShootCommand() {
	}

	public ShootCommand(Vector3 inputTarget) {
		target = inputTarget;
	}

	public void Execute (BaseUnit unit) {
		ICanShoot castedUnit = unit as ICanShoot;
		if (castedUnit != null) {
			if (unit.facing == Direction4.North) {
				unit.animator.SetTrigger ("AttackUp");
			} else if (unit.facing == Direction4.East) {
				unit.animator.SetTrigger ("AttackRight");
			} else if (unit.facing == Direction4.South) {
				unit.animator.SetTrigger ("AttackDown");
			} else if (unit.facing == Direction4.West) {
				unit.animator.SetTrigger ("AttackLeft");
			}

			if (target != null) {
				castedUnit.Shoot (target);
			} else {
				castedUnit.Shoot ();
			}
		}
	}

}
