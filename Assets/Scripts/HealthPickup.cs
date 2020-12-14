using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour {

	public float rotationAmount = 1;

	void OnTriggerEnter(Collider c) {
		if (c.gameObject.layer == LayerMask.NameToLayer("Player")) {
			c.GetComponent<PlayerController>().Heal();
			Debug.Log ("Healing player.");
			Destroy (gameObject);
		}
	}

//	void Update() {
//		transform.Rotate(new Vector3(0, 0, rotationAmount));
//	}
}
