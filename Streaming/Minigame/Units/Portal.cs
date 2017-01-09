using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

	public Grunt gruntPrefab;
	public Archer archerPrefab;
	public Wizard wizardPrefab;
	public Gate correspondingGate;
	private Damsel damsel;
	private int gruntsToSpawn = 0;
	private int archersToSpawn = 0;
	private bool spawnWizard = false;
	private float timeSinceLastSpawn = 0;
	public float spawnTimer = 1f;
	private MinigameManager miniGameManager;

	public int NumberOfSpawnsLeft() {
		return (gruntsToSpawn + archersToSpawn + (spawnWizard ? 1 : 0));
	}

	public void SetSpawns(int numGrunt, int numArchers, bool wizard) {
		timeSinceLastSpawn = 0f;
		gruntsToSpawn = numGrunt;
		archersToSpawn = numArchers;
		spawnWizard = wizard;
	}

	// Use this for initialization
	void Start () {
		miniGameManager = GameManager.instance.miniGameManager;
		damsel = miniGameManager.damsel;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector2.Distance (damsel.transform.position, transform.position) < 0.1f) {
			miniGameManager.GameOver ();
		}
		if (Time.time - timeSinceLastSpawn > spawnTimer) {
			timeSinceLastSpawn = Time.time;
			if (gruntsToSpawn > 0) {
				Grunt spawnedGrunt = Instantiate (gruntPrefab, transform.position, Quaternion.identity) as Grunt;
				spawnedGrunt.transform.SetParent (miniGameManager.gameObject.transform);
				spawnedGrunt.targetGate = correspondingGate;
				gruntsToSpawn--;
				miniGameManager.AddWizardUnitToList (spawnedGrunt as BaseUnit);
			} else if (archersToSpawn > 0) {
				Archer spawnedArcher = Instantiate (archerPrefab, transform.position, Quaternion.identity) as Archer;
				spawnedArcher.targetGate = correspondingGate;
				spawnedArcher.transform.SetParent (miniGameManager.gameObject.transform);
				archersToSpawn--;
				miniGameManager.AddWizardUnitToList (spawnedArcher as BaseUnit);
			} else if (spawnWizard) {
				Wizard spawnedWizard = Instantiate (wizardPrefab, transform.position, Quaternion.identity) as Wizard;
				spawnedWizard.targetGate = correspondingGate;
				spawnedWizard.transform.SetParent (miniGameManager.gameObject.transform);
				spawnWizard = false;
				miniGameManager.AddWizardUnitToList (spawnedWizard as BaseUnit);
			}
		}
	}
}
