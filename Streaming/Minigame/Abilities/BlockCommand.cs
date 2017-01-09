using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCommand : ICommand {

	public void Execute(BaseUnit unit) {
		ICanBlock castedUnit = unit as ICanBlock;
		if (castedUnit != null) {
			castedUnit.Block ();
		}
	}
}
