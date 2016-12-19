using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScheduleClockGroup : MonoBehaviour {

	// Need some sort of preinitialized array to track locations for snap-to clock grid
	public Schedule schedule;
	public List<ClockToken> clocksInContainer;
	public ScheduleActivities containerActivity;
	// Need something to deal with specific activities

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
