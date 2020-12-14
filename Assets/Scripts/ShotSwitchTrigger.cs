using UnityEngine;
using System.Collections;

public class ShotSwitchTrigger : MonoBehaviour {

	public int firingPattern;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Player") {
			Debug.Log ("TRIGGER ENTERED");
			AirshipController ship = GameManager.gameManager.currentAirship.GetComponent<AirshipController>();
			ship.currentFiringPattern = firingPattern;
		}
	}
}
