using UnityEngine;
using System.Collections;

public class GMController : ProjectileController {

	public float speed = 4f;
	public float turningSpeed = 1f;

	public float maxLife = 10f;
	private float lifeTime = 0f;

	private Transform target;

	// Use this for initialization
	void Start () {
		target = GameManager.gameManager.currentPlayer.transform;
	}
	
	// Update is called once per frame
	void Update () {

		Quaternion oldRotation = transform.rotation;

		Vector3 modifiedPosition = target.position;
		modifiedPosition.x = 0f;
		modifiedPosition.y = 0f;

		Quaternion newRotation = Quaternion.LookRotation (transform.position - modifiedPosition, Vector3.forward);

		transform.rotation = Quaternion.Slerp (oldRotation, newRotation, turningSpeed * Time.deltaTime);
		transform.Translate (Vector3.forward * speed * Time.deltaTime);
		lifeTime += Time.deltaTime;
		if (lifeTime >= maxLife) {
			Destroy (gameObject);
		}
	}
}