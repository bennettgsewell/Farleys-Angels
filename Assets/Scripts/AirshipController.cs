using UnityEngine;
using System.Collections;

public class AirshipController : MonoBehaviour {

	private Transform player;

	public TurretController[] turrets;

	public float bulletSpread = 1f;
	public float timeBetweenShots = 5f;
	private float shotTimer = 0f;
	public float timeBetweenGuns = 0.4f;
	private float gunTimer = 0f;
	private int currentGun = 0;
	private bool gunTimerRunning = false;

	public float verticalSpeed = 3f;
	public float verticalAcceleration = 3f;
	public float horizontalSpeed = 15f;
	public float horizontalAcceleration = 6f;

	public float followHeight = 20f;
	public float followLength = -12f;

	private float targetVerticalSpeed = 0f;
	private float currentVerticalSpeed = 0f;
	private float targetHorizontalSpeed = 0f;
	private float currentHorizontalSpeed = 0f;
	private Vector2 amountToMove;
	
	public int currentFiringPattern = 0;



	void Start () {
	
	}
	
	void Update () {

		//MOVEMENT
		targetVerticalSpeed = 0f;
		targetHorizontalSpeed = 0f;
	
		Vector2 targetPosition = Vector2.zero;
		targetPosition.y = player.position.y + followHeight;
		targetPosition.x = player.position.x + followLength;


		Vector2 moveDir = Vector2.zero;
		moveDir.x = Mathf.Sign (targetPosition.x - transform.position.x);
		moveDir.y = Mathf.Sign (targetPosition.y - transform.position.y);


		targetHorizontalSpeed = horizontalSpeed * moveDir.x;
		if (Mathf.Abs(targetHorizontalSpeed) > (Mathf.Abs (targetPosition.x - transform.position.x) * 2)) {
//			targetHorizontalSpeed = Mathf.Abs (targetPosition.x - transform.position.x) * moveDir.x;\
			targetHorizontalSpeed = 0f;
		}
		targetVerticalSpeed = verticalSpeed * moveDir.y;
		if (Mathf.Abs(targetVerticalSpeed) > (Mathf.Abs (targetPosition.y - transform.position.y) * 2)) {
//			targetVerticalSpeed = Mathf.Abs (targetPosition.y - transform.position.y) * moveDir.y;
			targetVerticalSpeed = 0f;
		}

		currentHorizontalSpeed = IncrementTowards (currentHorizontalSpeed, targetHorizontalSpeed, horizontalAcceleration);
		currentVerticalSpeed = IncrementTowards (currentVerticalSpeed, targetVerticalSpeed, verticalAcceleration);

		amountToMove.x = currentHorizontalSpeed;
		amountToMove.y = currentVerticalSpeed;

		Move (amountToMove);


		//TURRET FIRING

		switch(currentFiringPattern) {

		case 0:
			AttackPatternAlpha ();
			break;
		case 1:
			AttackPatternBeta();
			break;
		default:
			break;
		}
	}

	void AttackPatternAlpha() {
		Debug.Log ("PATTERN ALPHA GOGO");
		Vector3 playerPoint = player.position;
		Vector3 predictPoint = playerPoint;
		predictPoint.x += player.GetComponent<PlayerController>().GetCurrentSpeed() + 1f;
//		predictPoint.y += player.GetComponent<PlayerController>().GetVerticalSpeed();

		for (int i = 0; i < turrets.Length; i++) {
			/*
			Vector3 targetPoint = player.position;
			targetPoint.x += (i - turrets.Length * 0.5f) * bulletSpread;
			turrets[i].firingTarget = targetPoint;
			*/
			turrets[i].firingTarget = predictPoint;

		}

		shotTimer += Time.deltaTime;
		if (shotTimer >= timeBetweenShots) {
			shotTimer = 0;
			gunTimerRunning = true;
		}

		if (gunTimerRunning) {
			gunTimer += Time.deltaTime;
			if (gunTimer >= timeBetweenGuns) {
				gunTimer = 0;
				turrets[currentGun].Shoot ();
				currentGun++;
				if (currentGun >= turrets.Length) {
					currentGun = 0;
					gunTimerRunning = false;
				}
			}
		}


	}

	void AttackPatternBeta() {
		Vector3 playerPoint = player.position;
		Vector3 predictPoint = playerPoint;
		predictPoint.x += player.GetComponent<PlayerController>().GetCurrentSpeed() + 1f;
		//		predictPoint.y += player.GetComponent<PlayerController>().GetVerticalSpeed();
		
		for (int i = 0; i < turrets.Length; i++) {
			predictPoint.x += (i - turrets.Length * 0.5f) * bulletSpread;
			turrets[i].firingTarget = predictPoint;
			
		}

		shotTimer += Time.deltaTime * 0.5f;
		if (shotTimer >= timeBetweenShots) {
			shotTimer = 0;
			gunTimerRunning = true;
		}
		
		if (gunTimerRunning) {
			gunTimer += Time.deltaTime;
			if (gunTimer >= timeBetweenGuns) {
				gunTimer = 0;
				turrets[currentGun].Shoot ();
				currentGun++;
				if (currentGun >= turrets.Length) {
					currentGun = 0;
					gunTimerRunning = false;
				}
			}
		}
	}

	void Move(Vector2 moveAmount) {
		transform.Translate (moveAmount * Time.deltaTime);
	}
	
	private float IncrementTowards(float current, float target, float acceleration) {
		if (current == target) {
			return current;
		} else {
			float dir = Mathf.Sign (target - current);
			current += acceleration * Time.deltaTime * dir;
			return (dir == Mathf.Sign (target-current))? current : target;
		}
	}

	public void SetPlayer(GameObject player) {
		this.player = player.transform;
	}


}
