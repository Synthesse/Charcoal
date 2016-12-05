using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ViewStateManager : MonoBehaviour {

	public GameObject Outside;
	public GameObject Room;
	public GameObject Computer;
	public Button OutsideToRoom;
	public Button RoomToComputer;
	public Button RoomToOutside;
	public Button ComputerStream;
	public Button ComputerWeb;
	public Button ComputerToRoom;
	public ViewStates currentViewState;

	public void ViewStateTransition(ViewStates view) {
		DeactivateView ();
		ActivateView (view);
	}

	private void DeactivateView() {
		if (currentViewState == ViewStates.Computer) {
			Computer.SetActive (false);
		} else if (currentViewState == ViewStates.Room) {
			Room.SetActive (false);
		} else if (currentViewState == ViewStates.Outside) {
			Outside.SetActive (false);
		}
	}

	private void ActivateView(ViewStates view) {
		if (view == ViewStates.Computer) {
			Computer.SetActive (true);
		} else if (view == ViewStates.Room) {
			Room.SetActive (true);
		} else if (view == ViewStates.Outside) {
			Outside.SetActive (true);
		}
		currentViewState = view;
	}

	void Awake() {
		
	}

	// Use this for initialization
	void Start () {
//		OutsideToRoom.GetComponent<IListenedTo>().RegisterListener(() => ViewStateTransition(ViewStates.Room));
		RoomToComputer.GetComponent<IListenedTo>().RegisterListener(() => ViewStateTransition(ViewStates.Computer));
//		RoomToOutside.GetComponent<IListenedTo>().RegisterListener(() => ViewStateTransition(ViewStates.Outside));
//		ComputerToRoom.GetComponent<IListenedTo>().RegisterListener(() => ViewStateTransition(ViewStates.Room));

		Room.SetActive (true);
		Outside.SetActive (false);
		Computer.SetActive (false);
		currentViewState = ViewStates.Room;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
