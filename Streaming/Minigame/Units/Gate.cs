using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Gate : BaseUnit, IListenedTo {

	protected UnityEvent deathEvent;

	public void RegisterListener(params UnityAction[] calls) {
		UnityAction call = calls [0];
		for (int i = 1; i < calls.Length; i++) {
			call += calls [i];
		}
		deathEvent.AddListener (call);
	}

	public void UnregisterListener(UnityAction call) {
		deathEvent.RemoveListener (call);
	}

	public void UnregisterAllListeners() {
		deathEvent.RemoveAllListeners();
	}



	public override void Kill() {
		deathEvent.Invoke ();
		Destroy (gameObject);
	}

	protected override void ExecuteQueuedCommands() {
	}

	public override void Hurt(Allegiance sourceAllegiance) {
		if (sourceAllegiance != allegiance) {
			hitPoints -= 5;
		}
	}

	public override void Hurt(Allegiance sourceAllegiance, int loss) {
		if (sourceAllegiance != allegiance) {
			hitPoints -= loss;
		}
	}

	public override void Move() {
	}

	public override void UpdateFacing(Direction4 newFacing) {
	}

	void Awake() {
		deathEvent = new UnityEvent ();
	}

	// Use this for initialization
	void Start () {
		baseMoveSpeed = 0f;
		hitPoints = 40;
		allegiance = Allegiance.Knight;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
