using UnityEngine;
using System.Collections;

public class SlashCommand : ICommand {

	//TODO: Abstract to N-degree cone
	private bool CheckPositionInDirectionalCone(Vector3 relativePosition, Direction4 direction) {
		bool condition1 = false;
		bool condition2 = false;
		if (direction == Direction4.North || direction == Direction4.South) {
			condition1 = (Mathf.Abs (relativePosition.y) >= Mathf.Abs (relativePosition.x));
			if (direction == Direction4.North) {
				condition2 = (relativePosition.y >= 0);
			} else {
				condition2 = (relativePosition.y <= 0);
			}
		} else {
			condition1 = (Mathf.Abs (relativePosition.y) <= Mathf.Abs (relativePosition.x));
			if (direction == Direction4.East) {
				condition2 = (relativePosition.x >= 0);
			} else {
				condition2 = (relativePosition.x <= 0);
			}
		}
			
		return (condition1 && condition2);
	}

	public void Execute(BaseUnit unit) {
		Collider2D[] struckColliders = Physics2D.OverlapCircleAll(unit.transform.position, 0.4f);

		if (unit.facing == Direction4.North) {
			unit.animator.SetTrigger ("AttackUp");
		} else if (unit.facing == Direction4.East) {
			unit.animator.SetTrigger ("AttackRight");
		} else if (unit.facing == Direction4.South) {
			unit.animator.SetTrigger ("AttackDown");
		} else if (unit.facing == Direction4.West) {
			unit.animator.SetTrigger ("AttackLeft");
		}

		foreach (Collider2D col in struckColliders) {
			Debug.Log ("CheckingStruckCollider");
			Vector3 relativePosition = col.transform.position - unit.transform.position;
			if (relativePosition.magnitude > 0 && CheckPositionInDirectionalCone (relativePosition, unit.facing)) {
				col.GetComponent<BaseUnit> ().Hurt ();
			}
		}
	}
}
