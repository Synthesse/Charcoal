using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class OldMiniGameController : MonoBehaviour {

	private int gold;
	private float nextMinionTime;
	public Button[] allButtons;
	public Text goldText;
	private List<Button> activeButtons;
	private List<Button> inactiveButtons;

	private void KillMinion(Button button) {
		activeButtons.Remove (button);
		inactiveButtons.Add (button);
		button.gameObject.SetActive (false);
		gold += 1;
		UpdateGoldCounter ();
	}

	private void UpdateGoldCounter() {
		goldText.text = "Gold: " + gold.ToString ();
	}

	public void ResetGame() {
		inactiveButtons.AddRange (activeButtons);
		activeButtons.Clear ();
		DeactivateButtons ();
		nextMinionTime = 0;
	}

	private void DeactivateButtons() {
		foreach (Button button in allButtons) {
			button.gameObject.SetActive (false);
		}
	}

	// Use this for initialization
	void Start () {
		foreach (Button button in allButtons) {
			Button b = button;
			button.GetComponent<IListenedTo>().RegisterListener(() => KillMinion(b));
		}

		activeButtons = new List<Button> ();
		inactiveButtons = new List<Button> ();
		inactiveButtons.AddRange(allButtons);
		DeactivateButtons ();
		gold = 0;
		nextMinionTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if ((activeButtons.Count < allButtons.Length) && (Time.time >= nextMinionTime)) {
			nextMinionTime = Time.time + (Random.value * 2f +0.5f);
			Button chosenButton = inactiveButtons[Random.Range (0, inactiveButtons.Count)];
			inactiveButtons.Remove (chosenButton);
			activeButtons.Add (chosenButton);
			chosenButton.gameObject.SetActive (true);
		}

	}
}
