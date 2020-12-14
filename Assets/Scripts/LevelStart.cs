using UnityEngine;
using System.Collections;

public class LevelStart : MonoBehaviour {

	public GUISkin skin;

	private bool showReady = true;
	private bool showGo = false;

	private float timer = 0f;

	public float readyTime = 2f;
	public float goTime = 4f;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer >= goTime) {
			showGo = false;
			showReady = false;
		}
		else if (timer >= readyTime) {
			showReady = false;
			showGo = true;
			GameManager.gameManager.currentPlayer.GetComponent<PlayerController>().enabled = true;
		} else {
			GameManager.gameManager.currentPlayer.GetComponent<PlayerController>().enabled = false;
		}
	}

	void OnGUI() {
		GUI.skin = skin;
		if (showReady) {
			GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "Ready");
		}

		if (showGo) {
			GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "Go!");
		}
		
	}
}
