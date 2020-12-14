using UnityEngine;
using System.Collections;

public class LevelEnd : MonoBehaviour {

	public GUISkin skin;
	private float timer = 0f;

	private float endTimer = 0f;
	public float endDuration = 4f;

	public int nextLevel = 0;

	private bool endingLevel = false;

	private bool showEndTime = false;


	void OnTriggerEnter(Collider col) {
		if (col.tag == "Player") {
			endingLevel = true;
		}
	}
	// Update is called once per frame
	void Update () {
		if (endingLevel) {
			showEndTime = true;
			endTimer += Time.deltaTime;
			if (endTimer >= endDuration) {
				Application.LoadLevel(nextLevel);
			}
			GameManager.gameManager.currentAirship.GetComponent<AirshipController>().enabled = false;
			GameManager.gameManager.currentPlayer.GetComponent<PlayerController>().enabled = false;
			GameManager.gameManager.currentPlayer.GetComponent<PlayerController>().recentlyHit = true;

		} else {
			timer += Time.deltaTime;

		}
	}

	void OnGUI() {
		if (showEndTime) {
			GUI.skin = skin;
			GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "Level Complete\nTime: " + timer.ToString ("#.00") + " seconds");
		}

	}
}
