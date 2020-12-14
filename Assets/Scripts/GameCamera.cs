using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

	int direction;
	[HideInInspector]
	public GameObject player;
	private Transform target;
	private Transform left;
	private Transform right;
	private Transform stationary;

	public float maxheight = 20f;
	public float minheight = -10f;

	public float verticalOffset = 0f;

	float distanceX;
	float distanceY;

	private Vector3 targetPos;
	public float speed = 12f;
	public float smoothing = 1f;


	void Start (){
		direction = 0;
	}

	public void SetPlayer(GameObject player) {
		this.player = player;
		left = player.transform.Find ("LeftCamPoint");
		right = player.transform.Find ("RightCamPoint");
		stationary = player.transform.Find ("StationaryCamPoint");
		transform.position = new Vector3 (stationary.position.x, stationary.position.y + verticalOffset, transform.position.z);
	}

	void Update () {
	
		//gets character direction
		if(Input.GetAxisRaw("Horizontal") < 0)
		{
			direction = -1;
		}
		if(Input.GetAxisRaw("Horizontal") > 0)
		{
			direction = 1;
		}
		if(Input.GetAxisRaw("Horizontal") == 0) 
		{
			direction = 0;
		}
		//sets ideal camera location depending on character direction
		if (direction == -1) {
			target = left;
			distanceX = target.position.x;
			distanceY = target.position.y;
		}
		else if (direction == 1){
			target = right;
			distanceX = target.position.x;
			distanceY = target.position.y;
		}
		else {
			target = stationary;
			distanceX = target.position.x;
			distanceY = target.position.y;
		}

		//stops camera from going too high or low
		if (target.position.y <= minheight) {
			distanceY = minheight;
				}
		if (target.position.y >= maxheight) {
			distanceY = maxheight;
		}
		//move camera to ideal location, faster at larger distance
		//while (distanceX > 1){
		//speed = 10;
		targetPos = new Vector3 (distanceX, distanceY + verticalOffset, transform.position.z);

		transform.position = Vector3.Lerp (transform.position, targetPos, smoothing * Time.deltaTime);

		if (transform.position.y > target.position.y + verticalOffset*1.5f) {
			float fixedY = target.position.y + verticalOffset*1.5f;

			transform.position = new Vector3(transform.position.x, fixedY, transform.position.z);
		}
		//}
		//slow camera as it nears ideal location, may be implimented or not
		//while (distanceX <= 1){
		//	speed = 5;
		//	targetPos = new Vector3 (distanceX, distanceY, distanceZ);
		//	transform.position = Vector3.MoveTowards (transform.position, targetPos, speed * Time.deltaTime);
		//}
	}
}
