using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MinigameManager : MonoBehaviour {

	public Damsel damsel;
	public Portal westPortal;
	public Portal eastPortal;
	public Portal southPortal;
	public int roundNumber = 0;
	private bool roundInitializing;
	public Text roundDisplayText;
	private List<BaseUnit> wizardUnits;

	public void StartGame() {

	}

	public void GameOver() {
		Destroy (gameObject);
	}

	public void AddWizardUnitToList(BaseUnit unit) {
		wizardUnits.Add (unit);
	}

	public void RemoveWizardUnitFromList(BaseUnit unit) {
		wizardUnits.Remove (unit);
	}

	public IEnumerator NewRound() {
		roundDisplayText.text = "Round starts in 5 seconds";
		yield return new WaitForSeconds (5f);
		roundNumber++;
		roundDisplayText.text = string.Concat ("Round ", roundNumber.ToString ());
		if (roundNumber == 1) {
			westPortal.SetSpawns (1, 0, false);
			eastPortal.SetSpawns (1, 0, false);
			southPortal.SetSpawns (0, 1, false);
		} else if (roundNumber == 2) {
			westPortal.SetSpawns (0, 1, false);
			eastPortal.SetSpawns (1, 1, false);
			southPortal.SetSpawns (2, 0, false);
		} else if (roundNumber == 3) {
			westPortal.SetSpawns (2, 1, false);
			eastPortal.SetSpawns (1, 1, false);
			southPortal.SetSpawns (1, 1, false);
		} else if (roundNumber == 4) {
			westPortal.SetSpawns (2, 1, false);
			eastPortal.SetSpawns (2, 1, false);
			southPortal.SetSpawns (1, 1, true);
		}
		roundInitializing = false;
	}

	// Use this for initialization
	void Start () {
		wizardUnits = new List<BaseUnit> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (wizardUnits.Count == 0 && !roundInitializing && westPortal.NumberOfSpawnsLeft() == 0 && eastPortal.NumberOfSpawnsLeft() == 0 && southPortal.NumberOfSpawnsLeft() == 0) {
			roundInitializing = true;
			StartCoroutine(NewRound());
		}
	}
}
