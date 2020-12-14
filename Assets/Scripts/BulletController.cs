using UnityEngine;
using System.Collections;

public class BulletController : ProjectileController {

	public float speed = 5f;

	public float maxLife = 10f;
	private float lifeTime = 0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * speed * Time.deltaTime);
		lifeTime += Time.deltaTime;
		if (lifeTime >= maxLife) {
			Destroy (gameObject);
		}
	}
}
