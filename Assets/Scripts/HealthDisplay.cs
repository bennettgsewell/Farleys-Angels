using UnityEngine;
using System.Collections;

public class HealthDisplay : MonoBehaviour {

	public GameObject[] hearts;
	

	void Start() {


	}

	// Update is called once per frame
	void Update () {
		PlayerController player = GameManager.gameManager.currentPlayer.GetComponent <PlayerController> ();
		int health = player.GetCurrentHealth();

		for (int i = 0; i < hearts.Length; i++) {
			if (i < health) {
				hearts[i].SetActive (true);
			} else {
				hearts[i].SetActive(false);
			}

		}
	}
}