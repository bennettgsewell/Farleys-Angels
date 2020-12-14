using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour {

	[HideInInspector]
	public Vector3 firingTarget;

	public GameObject bulletPrefab;



	void Update() {
		transform.LookAt (firingTarget, Vector3.left);
	}

	public void Shoot() {
		Debug.Log ("FIRING AT ASSHOLE THIEF LADY");
		Vector3 bulletSpawnPosition = transform.forward;
		bulletSpawnPosition.z = 0;

		Instantiate (bulletPrefab, transform.position, transform.rotation);
	}

}
