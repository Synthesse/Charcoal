using UnityEngine;
using System.Collections;

public enum GamePhase
{
	NarrativePreStream,
	NarrativePostStream,
	Planning,
	Streaming,
	None
}

public enum GameTimeBlock
{
	Morning, //Roughly 8AM to 1PM
	Afternoon, //Roughly 1PM to 6PM
	Evening, //Roughly 6PM to 11PM
	Night, //Roughly 11PM to 4AM
	Dawn //Roughly 4AM to 8AM
}

public enum ScheduleActivities
{
	Open,
	Sleep,
	Class,
	Stream,
	Relax,
	Socialize
}