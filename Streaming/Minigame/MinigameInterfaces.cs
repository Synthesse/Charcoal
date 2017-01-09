using UnityEngine;

public interface ICanCarry {
	
	void Carry(Damsel damsel);

	void Drop (Damsel damsel);

	void ReceivePushedMoveCommand (MoveCommand moveCommand);

	Direction8 GetSpawnDirection ();

}

public interface ICanShoot {
	
	void Shoot();

	void Shoot (Vector3 target);

}

public interface ICanBlock {

	void Block();

}
