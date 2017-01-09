using UnityEngine;
using System.Collections;

public class Knight : BaseUnit, ICanBlock {

	public Shield shieldPrefab;


	public override void Kill () {
		GameManager.instance.miniGameManager.GameOver ();
	}

	public void Block() {
		animator.SetTrigger ("Stand");
		nonAnimatedAbilityInProgress = true;

		Shield shield = Instantiate (shieldPrefab, transform.position + TranslateDirectiontoVector3(facing)*0.2f, Quaternion.FromToRotation(TranslateDirectiontoVector3(Direction4.North),TranslateDirectiontoVector3(facing))) as Shield;
		shield.RegisterListener (() => nonAnimatedAbilityInProgress = false, () => FindObjectOfType<PlayerInputController>().manualRedetectMovement = true); 
		//TODO: remove usage of FindObject
	}

	void Start () {
		base.ProtectedStart ();
		allegiance = Allegiance.Knight;
		hitPoints = 60;
	}

	void FixedUpdate () {
		base.ProtectedUpdate ();
	}
}
