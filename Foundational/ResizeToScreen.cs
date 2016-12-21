using UnityEngine;
using System.Collections;

public class ResizeToScreen : MonoBehaviour {

	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		spriteRenderer = this.gameObject.GetComponent<SpriteRenderer> ();
		float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
		float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

		transform.localScale = new Vector3 (worldScreenWidth / spriteRenderer.sprite.bounds.size.x, worldScreenHeight / spriteRenderer.sprite.bounds.size.y, 1);
	}

}
