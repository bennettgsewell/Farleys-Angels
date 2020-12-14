using UnityEngine;
using System.Collections;

public class FuelPickup : MonoBehaviour {

	public float rotationAmount = 1;


	void OnTriggerEnter(Collider c) {
		if (c.gameObject.layer == LayerMask.NameToLayer("Player")) {
			c.GetComponent<PlayerController>().Refuel();
			Destroy(gameObject);
		}
	}

//	void Update() {
//		transform.Rotate(new Vector3(0, 0, rotationAmount));
//	}
}
