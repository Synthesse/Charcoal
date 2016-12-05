using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgramStateManager : MonoBehaviour {

	private bool isStreaming;
	public GameObject StreamUI;
	public GameObject DesktopPanel;
	public Button StartStreaming;
	public Button EndStreaming;
	public GameObject streamChatController;

	public void ToggleStreaming() {
		if (isStreaming) {
			StreamUI.SetActive (false);
			streamChatController.SetActive (false);
			DesktopPanel.SetActive (true);
			isStreaming = false;
		} else {
			DesktopPanel.SetActive (false);
			StreamUI.SetActive (true);
			streamChatController.SetActive (true);
			isStreaming = true;
		}
	}

	// Use this for initialization
	void Start () {
		StartStreaming.GetComponent<IListenedTo>().RegisterListener(() => ToggleStreaming());
		EndStreaming.GetComponent<IListenedTo>().RegisterListener(() => ToggleStreaming());

		isStreaming = false;
		StreamUI.SetActive (false);
		streamChatController.SetActive (false);
	}
}
