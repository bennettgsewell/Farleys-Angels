using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject player;
	[HideInInspector]
	public GameCamera cam;
	public GameObject airship;

	public static GameManager gameManager;

	public float playerMinHeight = -30f;

	public GameObject currentPlayer;
	public GameObject currentAirship;

	// Use this for initialization
	void Start () {
		SpawnPlayer();
		SpawnAirship();
		cam = GetComponent<GameCamera> ();
		cam.SetPlayer(currentPlayer);
		currentAirship.GetComponent<AirshipController>().SetPlayer(currentPlayer);
		gameManager = this;
	}
	
	private void SpawnPlayer() {
		currentPlayer = Instantiate (player, Vector3.zero, Quaternion.identity) as GameObject;
	}

	private void SpawnAirship() {
		Vector3 spawnPoint = Vector3.zero;
		spawnPoint.x = -70;
		spawnPoint.y = 20;
		spawnPoint.z = 0;
		currentAirship = Instantiate (airship, spawnPoint, Quaternion.identity) as GameObject;
	}

	public void Respawn() {
		/*
		Destroy (currentPlayer);
		Destroy (currentAirship);
		SpawnPlayer ();
		SpawnAirship ();
		cam.SetPlayer (currentPlayer);
		currentAirship.GetComponent<AirshipController>().SetPlayer(currentPlayer);
		*/

		Application.LoadLevel (Application.loadedLevelName);

	}

	void Update() {
		if (currentPlayer.transform.position.y < playerMinHeight) {
			Debug.Log ("Attempting to respawn");
			Respawn();
		}
	}
}
