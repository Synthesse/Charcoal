using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StreamChatController : MonoBehaviour {

	public GameObject clickableChatPrefab;
	public GameObject nonclickableChatPrefab;
	public GameObject streamChatWindow;
	public GameObject responseContainer;
	public Button redResponse;
	public Button blueResponse;
	public Button greenReponse;
	public Dropdown altUX;

	private GameObject highlitChatClone;
	private int chatLineLimit = 10;
	private float nextChatTime;
	private List<GameObject> chatMessages;
	private Vector3 defaultPosition;

	private GameObject CreateChat() {
		GameObject toInstantiate;
		string msgString;
		if (Random.value > 0.5f) {
			toInstantiate = clickableChatPrefab;
			msgString = "Click Me";
		} else {
			toInstantiate = nonclickableChatPrefab;
			msgString = "Dont Click";
		}

		GameObject instance = Instantiate (toInstantiate);
		string newMsg = "";
		for (int i = 0; i <= Random.Range(1,5); i++) {
			if (i > 0)
				newMsg += "\n";
			newMsg += msgString;

		}
		instance.GetComponentInChildren<Text> ().text = newMsg;
		instance.transform.SetParent (streamChatWindow.transform);
		if (msgString == "Click Me") {
			instance.GetComponent<IListenedTo>().RegisterListener(() => ShowResponses(instance));
		}
		return instance;
	}

	private void ColorChat(GameObject chatObj, Color color) {
		chatObj.GetComponent<Image> ().color = color;
		chatObj.GetComponent<Button> ().interactable = false;
	}

	private void ShowResponses(GameObject chatObj) {
		responseContainer.SetActive (true);
		if (altUX.value == 1) {
			responseContainer.transform.position = chatObj.transform.position + new Vector3 (-100, 0, 0);
		} else if (altUX.value == 2) {
			responseContainer.transform.position = streamChatWindow.transform.position + new Vector3 (-100, 200, 0);
		} else {
			responseContainer.transform.position = defaultPosition;
		}

		DestroyChatClone ();
		if (altUX.value == 2 || altUX.value == 3) {
			highlitChatClone = Instantiate (chatObj);
			highlitChatClone.transform.SetParent (streamChatWindow.transform.parent.transform.parent.transform);
			highlitChatClone.GetComponent<RectTransform> ().sizeDelta = chatObj.GetComponent<RectTransform> ().sizeDelta;
			highlitChatClone.transform.position = responseContainer.transform.position + new Vector3 (100, 0, 0);
			highlitChatClone.GetComponent<Button> ().interactable = false;
		}

		redResponse.GetComponent<IListenedTo> ().UnregisterAllListeners();
		blueResponse.GetComponent<IListenedTo> ().UnregisterAllListeners();
		greenReponse.GetComponent<IListenedTo> ().UnregisterAllListeners();

		redResponse.GetComponent<IListenedTo> ().RegisterListener(() => ColorChat(chatObj, new Color(1f, 0.6f, 0.6f)), () => HideResponses());
		blueResponse.GetComponent<IListenedTo> ().RegisterListener(() => ColorChat(chatObj, new Color(0.6f, 0.6f, 1f)), () => HideResponses());
		greenReponse.GetComponent<IListenedTo> ().RegisterListener(() => ColorChat(chatObj, new Color(0.6f, 1f, 0.6f)), () => HideResponses());
	}

	public void ResetChat() {
		HideResponses ();
	}

	private void HideResponses() {
		responseContainer.SetActive (false);
		DestroyChatClone ();
	}

	private void DestroyChatClone() {
		Destroy (highlitChatClone);
	}

	// Use this for initialization
	void Start () {
		chatMessages = new List<GameObject> ();
		nextChatTime = 0f;
		defaultPosition = responseContainer.transform.position;
		HideResponses ();

		altUX.GetComponent<DropdownEventController>().RegisterListener ((int i) => DestroyChatClone ());
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= nextChatTime) {
			nextChatTime = Time.time + (Random.value * 4f +1f);
			if (chatMessages.Count > chatLineLimit) {
				GameObject chatToRemove = chatMessages [0];
				chatMessages.RemoveAt (0);
				Destroy (chatToRemove);
			}
			GameObject chatObj = CreateChat ();
			chatMessages.Add (chatObj);
		}
	}
}
