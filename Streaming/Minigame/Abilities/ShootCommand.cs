using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCommand : ICommand {

	public void Execute (BaseUnit unit) {
		ICanShoot castedUnit = unit as ICanShoot;
		if (castedUnit != null) {
			castedUnit.Shoot ();
		}
	}

}
