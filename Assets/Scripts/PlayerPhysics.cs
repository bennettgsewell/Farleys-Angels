using UnityEngine;
using System.Collections;


[RequireComponent(typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour {

	public LayerMask collisionMask; 

	public float wallSickyness = 0.1f;
	private float stickTimer = 0.0f;

	[HideInInspector]
	public bool grounded;

	[HideInInspector]
	public bool againstRightWall;

	[HideInInspector]
	public bool againstLeftWall;

	[HideInInspector]
	public bool movementStopped;


	private BoxCollider collider;
	private Vector3 size;
	private Vector3 center;

	private float skin = 0.005f;

	Ray ray;
	RaycastHit hit;

	void Start() {
		collider = GetComponent<BoxCollider> ();
		size = transform.localScale;
		center = collider.center;
	}

	public void Move (Vector2 moveAmount) {

		float deltaX = moveAmount.x;
		float deltaY = moveAmount.y;
		Vector2 position = transform.position;

		grounded = false;

		//check for up/down collisions
		for (int i = 0; i < 3; i++) {
			float dir = Mathf.Sign(deltaY);
			float x = (position.x + center.x - (size.x * 1.1f)/2) + (size.x * 1.1f)/2 * i; //Left, center, right positions of collider
			float y = position.y + center.y + size.y/2 * dir; //Bottom of collider

			ray = new Ray(new Vector2(x,y), new Vector2 (0, dir));
			Debug.DrawRay(new Vector3 (x, y, 0), Vector3.down);
			if (Physics.Raycast(ray, out hit, Mathf.Abs(deltaY) + skin, collisionMask)) {



				float dist = Vector3.Distance (ray.origin, hit.point);

				if (dist > skin) {
					deltaY = dist * dir - skin * dir;
				} else {
					deltaY = 0;
				}
				grounded = true;
				break;
			}
		}

		//check for left/right collisions

		movementStopped = false;

		if (againstLeftWall == true) {
			stickTimer += Time.deltaTime;
			if (stickTimer >= wallSickyness) {
				againstLeftWall = false;
			}
		}

		if (againstRightWall == true) {
			stickTimer += Time.deltaTime;
			if (stickTimer >= wallSickyness) {
				againstRightWall = false;
			}
		}

		for (int i = 0; i < 3; i++) {
			float dir = Mathf.Sign(deltaX);
			float x = position.x + center.x + size.x/2 * dir; //Left, center, right positions of collider
			float y = position.y + center.y - size.y/2 + size.y/2 * i; //Bottom of collider
			
			ray = new Ray(new Vector2(x,y), new Vector2 (dir, 0));
			if (Physics.Raycast(ray, out hit, Mathf.Abs(deltaX) + skin, collisionMask)) {
				float dist = Vector3.Distance (ray.origin, hit.point);
				if (dist > skin) {
					deltaX = dist * dir - skin * dir;
				} else {
					deltaX = 0;
				}
				movementStopped = true;

				if (dir < 0) {
					againstLeftWall = true;
				} else if (dir > 0) {
					againstRightWall = true;
				}
				stickTimer = 0f;
				break;
			}
		}

		Vector2 finalTransform = new Vector2(deltaX, deltaY);

		transform.Translate (finalTransform);
	}
}
