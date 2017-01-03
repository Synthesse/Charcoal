using UnityEngine;

public class CarryCommand : ICommand {

	private bool CheckProximityToDamsel(BaseUnit unit, Damsel damsel) {
		return (Vector2.Distance(damsel.transform.position, unit.transform.position) < 0.8f);
	}

	public void Execute(BaseUnit unit) {
		ICanCarry carryingUnit = unit as ICanCarry;
		if (carryingUnit != null && CheckProximityToDamsel(unit, GameManager.instance.miniGameManager.damsel)) {
			unit.isMoving = false;
			carryingUnit.Carry (GameManager.instance.miniGameManager.damsel);
		}
	}
}
