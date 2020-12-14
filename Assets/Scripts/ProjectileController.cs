using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {

	public int damage = 2;


	void OnCollisionEnter(Collision col) {

		if (col.gameObject.tag == "Player") {
			PlayerController player = col.gameObject.GetComponent<PlayerController>();
			if (player.recentlyHit == false) {
			    player.TakeDamage (damage);
			}
		}
		if (col.gameObject.tag != "Projectile") {
			Destroy (gameObject);
		}
	}
}
