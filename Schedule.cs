using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Schedule : MonoBehaviour {

	public struct ScheduleKey {
		public readonly GameTimeBlock ScheduleGameTimeBlock;
		public readonly System.DayOfWeek ScheduleDayOfWeek;
		public ScheduleKey(GameTimeBlock gtb, System.DayOfWeek dow) { ScheduleGameTimeBlock = gtb; ScheduleDayOfWeek = dow;} 
	}

	public GameObject scheduleContainer;
	public GameObject roomActivityContainer;
	public Dropdown scheduleDropdownPrefab;
	public Button scheduleConfirmButton;
	public Button sleepButton;
	public TimeManager timeManager;
	public ViewStateManager viewStateManager;
	public ProgramStateManager programStateManager;

	public Button outsideButtonForAltTiming;
	public GameObject lesbianWerewolfGirlfriend;
	public Button LWGButton;

	private Dictionary<ScheduleKey, Dropdown> schedule;
	private int altTimingInt;


	private void InitializeSchedule() {
		schedule = new Dictionary<ScheduleKey, Dropdown> ();
		int daysInWeek = System.Enum.GetValues (typeof(System.DayOfWeek)).Length;
		for (int i = 1; i < daysInWeek + 1; i++) {
			foreach (GameTimeBlock gtb in System.Enum.GetValues(typeof(GameTimeBlock))) {
				int j = i % daysInWeek;
				System.DayOfWeek dow = (System.DayOfWeek)j;
				ScheduleKey key = new ScheduleKey(gtb, dow);
				Dropdown newScheduleDropdown = Instantiate (scheduleDropdownPrefab);
				if ((gtb == GameTimeBlock.Morning && ((dow != System.DayOfWeek.Saturday) && (dow != System.DayOfWeek.Sunday))) || (gtb == GameTimeBlock.Afternoon && ((dow == System.DayOfWeek.Tuesday) || (dow == System.DayOfWeek.Thursday)))) {
					newScheduleDropdown.value = 2;
				} else if ((gtb == GameTimeBlock.Night) || (gtb == GameTimeBlock.Dawn)) {
					newScheduleDropdown.value = 1;
					if (gtb == GameTimeBlock.Dawn)
						newScheduleDropdown.interactable = false;
				} else if (gtb == GameTimeBlock.Evening && (dow == System.DayOfWeek.Monday || dow == System.DayOfWeek.Wednesday || dow == System.DayOfWeek.Friday)) {
					newScheduleDropdown.value = 3;
				} else {
					newScheduleDropdown.value = 0;
				}
				schedule.Add (key, newScheduleDropdown);
				newScheduleDropdown.transform.SetParent (transform);
			}
		}
	}

	private void ConfirmSchedule() {
		scheduleContainer.SetActive (false);

	}

	private void StartClass() {
		Debug.Log ("Start Class");
		viewStateManager.ViewStateTransition (ViewStates.Outside);
	}

	private void StartSleep() {
		Debug.Log ("Start Sleep");
		viewStateManager.ViewStateTransition (ViewStates.Room);
		Debug.Log ("Start Enable Sleep");
		roomActivityContainer.SetActive (false);
		sleepButton.gameObject.SetActive (true);
		Debug.Log ("End Enable Sleep");
	}

	private void StartOpen() {
		Debug.Log ("Start Open");
		viewStateManager.ViewStateTransition (ViewStates.Room);
		sleepButton.gameObject.SetActive (false);
		roomActivityContainer.SetActive(true);
	}

	private void StartStream() {
		Debug.Log ("Start Stream");
		viewStateManager.ViewStateTransition (ViewStates.Computer);
		programStateManager.ToggleStreaming ();
	}

	private void AbstractClass() {
		viewStateManager.ViewStateTransition (ViewStates.Outside);
		outsideButtonForAltTiming.GetComponentInChildren<Text> ().text = "Your classes go well this week";
	}

	private void AbstractRelax() {
		viewStateManager.ViewStateTransition (ViewStates.Room);
		Debug.Log ("Start Enable Sleep");
		roomActivityContainer.SetActive (false);
		sleepButton.gameObject.SetActive (true);
		sleepButton.GetComponentInChildren<Text> ().text = "You spend a considerable chunk of time relaxing, reading, and talking with people";
	}

	private void AbstractSleep() {
		viewStateManager.ViewStateTransition (ViewStates.Room);
		Debug.Log ("Start Enable Sleep");
		roomActivityContainer.SetActive (false);
		sleepButton.gameObject.SetActive (true);
		sleepButton.GetComponentInChildren<Text> ().text = "You get poor rest this week. Too much time spent streaming";
	}

	private void VisitLesbianWerewolfGirlfriend() {
		viewStateManager.ViewStateTransition (ViewStates.Outside);
		Debug.Log ("Start Enable Sleep");
		lesbianWerewolfGirlfriend.SetActive (true);
		outsideButtonForAltTiming.GetComponentInChildren<Text> ().text = "Hello Lesbian Werewolf Girlfriend - your fangs are looking particularly beautiful today.";
	}

	private void DoScheduleActivity(GameTimeBlock gtb, System.DayOfWeek dow) {
		if (timeManager.altTimingDropdown.value != 1) {
			int currentActivityValue = schedule [new ScheduleKey (gtb, dow)].value;

			if (currentActivityValue == 0)
				StartOpen ();
			else if (currentActivityValue == 1)
				StartSleep ();
			else if (currentActivityValue == 2)
				StartClass ();
			else if (currentActivityValue == 3)
				StartStream ();
			else
				timeManager.TimePasses ();
		} else {
			if (altTimingInt == 0) {
				AbstractClass ();
			} else if (altTimingInt == 1) {
				AbstractRelax ();
			} else if (altTimingInt == 2) {
				VisitLesbianWerewolfGirlfriend ();
			} else if (altTimingInt == 3) {
				StartStream ();
			} else if (altTimingInt == 4) {
				AbstractSleep ();
			} else {
				UnityEngine.SceneManagement.SceneManager.LoadScene ("MainGame");
			}
			altTimingInt++;
		}
	}

	// Use this for initialization
	void Start () {
		InitializeSchedule ();
		scheduleConfirmButton.GetComponent<IListenedTo>().RegisterListener(() => ConfirmSchedule());
		timeManager.RegisterTimeEventListener (DoScheduleActivity);
//		sleepButton.GetComponent<IListenedTo>().RegisterListener(() => Debug.Log ("Start Disable Sleep"), () => sleepButton.gameObject.SetActive(false), () => roomActivityContainer.SetActive(true), () => Debug.Log ("End Disable Sleep"));
		sleepButton.gameObject.SetActive (false);

		altTimingInt = 0;
		outsideButtonForAltTiming.GetComponent<IListenedTo> ().RegisterListener (() => lesbianWerewolfGirlfriend.SetActive (false), () => outsideButtonForAltTiming.GetComponentInChildren<Text> ().text = "Class time!");
		lesbianWerewolfGirlfriend.SetActive (false);
		LWGButton.GetComponent<IListenedTo> ().RegisterListener (() => VisitLesbianWerewolfGirlfriend ());
	}
}
