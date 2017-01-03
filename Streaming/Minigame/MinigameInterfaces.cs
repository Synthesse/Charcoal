public interface ICanCarry {
	
	void Carry(Damsel damsel);

	void Drop (Damsel damsel);

	void ReceivePushedMoveCommand (MoveCommand moveCommand);

}

public interface ICanShoot {
	void Shoot();
}
