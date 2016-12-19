using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class TimeManager : MonoBehaviour {


// NOTE TO SELF: UNITY COMPILER BUG FOR STRUCTS WITH GENERIC KEYS
//	public struct Tuple<T1, T2> {
//		public readonly T1 Item1;
//		public readonly T2 Item2;
//		public Tuple(T1 item1, T2 item2) { Item1 = item1; Item2 = item2;} 
//	}
//
//	public static class Tuple { // for type-inference goodness.
//		public static Tuple<T1,T2> Create<T1,T2>(T1 item1, T2 item2) { 
//			return new Tuple<T1,T2>(item1, item2); 
//		}
//	}



	private GameTimeBlock currentGameTimeBlock;
	private System.DateTime gameDT;
	private bool timePassing;
	private TimeEvent timePassesEvent;

	public Text timeText;
	public Button[] timePassesButtons;

	public class TimeEvent : UnityEvent<GameTimeBlock, System.DayOfWeek> {}

	public void TimePasses() {
		Debug.Log ("TIME IS PASSING");
		if (!timePassing)
			timePassing = true;
		if (currentGameTimeBlock == GameTimeBlock.Dawn) {
			currentGameTimeBlock = (GameTimeBlock)((((int)currentGameTimeBlock) + 1) % (System.Enum.GetValues (typeof(GameTimeBlock)).Length)); 
			TimePasses (4.0d);
		} else {
			currentGameTimeBlock = (GameTimeBlock)((((int)currentGameTimeBlock) + 1) % (System.Enum.GetValues (typeof(GameTimeBlock)).Length)); 
			TimePasses (5.0d);
		}
		//Advance to the next enum value

	}

	public void TimePasses(double hours) {
		gameDT = gameDT.AddHours (hours);
		timePassesEvent.Invoke (currentGameTimeBlock, gameDT.DayOfWeek);
		UpdateTimeString ();
	}

	private void UpdateTimeString() {
			timeText.text = "";
	}

	public void RegisterTimeEventListener(params UnityAction<GameTimeBlock, System.DayOfWeek>[] calls) {
		UnityAction<GameTimeBlock, System.DayOfWeek> call = calls [0];
		for (int i = 1; i < calls.Length; i++) {
			call += calls [i];
		}
		timePassesEvent.AddListener (call);
	}

	private void InitializeTimePassesButtons() {
		foreach (Button b in timePassesButtons) {
			b.GetComponent<IListenedTo> ().RegisterListener (() => TimePasses ());
		}
	}

	void Awake() {
		timePassesEvent = new TimeEvent ();
	}

	// Use this for initialization
	void Start () {
		
		gameDT = new System.DateTime (2014, 2, 3, 4, 0, 0);
		currentGameTimeBlock = GameTimeBlock.Dawn;
		UpdateTimeString ();
		timePassing = false;
		InitializeTimePassesButtons ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
