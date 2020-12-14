using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour {


	//Player Handling

	public GUISkin skin;

	public float gravity = 3f;
	public float speed = 3f;
	public float acceleration = 10f;
	public float jumpHeight = 12f;
	public float wallSlideMultiplyer = 0.5f;
	public float maxFuel = 2f;
	public int maxHealth = 10;

	private float currentSpeed = 0f;
	private float verticalSpeed = 0f;
	private float targetSpeed = 0f;
	private Vector2 amountToMove;
	private float currentFuel = 0f;
	private int currentHealth;

	[HideInInspector]
	public bool recentlyHit = false;
	private float recentlyHitTimer = 0f;
	public float hitFadeTime = 1f;
	public Material hitFadeMaterial;
	public Material standardMaterial;

	public float slideTime = 0.5f;
	public float slideSpeed = 5f;
	private float slideCounter = 0f;
	private bool isSliding = false;
	private float slideDirection = 0f;

	private PlayerPhysics physics;

	private Animator charAnimator;
	private Transform graphics;

	private ParticleSystem slideParticle;


	void Start () {
		physics = GetComponent<PlayerPhysics> ();
		charAnimator = transform.Find ("Yana_1").gameObject.GetComponent<Animator> ();
		graphics = transform.Find ("Yana_1");
		currentHealth = maxHealth;
		slideParticle = transform.Find ("Yana_1").Find ("DashParticle").GetComponent<ParticleSystem>();
	}
	
	void Update () {

		//HITSTUN
		if (recentlyHit == true) {
			recentlyHitTimer += Time.deltaTime;
			if (recentlyHitTimer >= hitFadeTime) {
				recentlyHit = false;
				GetComponent<Renderer>().material = standardMaterial;
				recentlyHitTimer = 0f;
			}
		}


		if (physics.movementStopped) {
			targetSpeed = 0;
			currentSpeed = 0;
		}

		if (isSliding == false && physics.grounded) {
			targetSpeed = Input.GetAxisRaw ("Horizontal") * speed;
		} else if (isSliding == true) {
			Debug.Log ("SLIDE IN PROGRESS");
			slideCounter += Time.deltaTime;
			targetSpeed = slideSpeed * slideDirection;
			if (slideCounter >= slideTime) {
				isSliding = false;
				Debug.Log ("ABORTING SLIDE LIKE AN UNWANTED BABY");
				//CHANGE BACK HITBOX & GRAPHIC SCALE
				transform.localScale = new Vector3(1, 2, 1);
			}
		} else if (physics.grounded == false) {
			isSliding = false;
			transform.localScale = new Vector3(1, 2, 1);
			if (currentSpeed > speed) targetSpeed = currentSpeed *+ Input.GetAxisRaw("Horizontal");
			else targetSpeed = Input.GetAxisRaw("Horizontal") * speed;

		}
		currentSpeed = IncrementTowards (currentSpeed, targetSpeed);


		//JUMPING
		if (physics.grounded) {
			amountToMove.y = -gravity * Time.deltaTime;
			if (Input.GetButtonDown ("Jump")) {
				amountToMove.y = jumpHeight;

			//SLIDING
			} else if (Input.GetButtonDown("Slide") && isSliding == false) {
				//CHANGE HITBOX & GRAPHIC SCALE
				Debug.Log ("SLIDING");
				transform.localScale = new Vector3(1, 2, 1);
				slideDirection = Mathf.Sign (Input.GetAxisRaw("Horizontal"));
				currentSpeed = slideSpeed * slideDirection;
				slideCounter = 0f;
				slideParticle.Play ();
				isSliding = true;

			}
		
		//WALLJUMPING
		} else if (physics.againstLeftWall) {
			amountToMove.y -= gravity * Time.deltaTime * wallSlideMultiplyer;
			if (Input.GetButtonDown ("Jump")) {
				amountToMove.y = jumpHeight;
				currentSpeed = speed;
			}
		} else if (physics.againstRightWall) {
			amountToMove.y -= gravity * Time.deltaTime * wallSlideMultiplyer;
			if (Input.GetButtonDown ("Jump")) {
				amountToMove.y = jumpHeight;
				currentSpeed = -speed;
			}
		} else {
			amountToMove.y -= gravity * Time.deltaTime;

			if (currentFuel > 0 && Input.GetButtonDown ("Jump")) {
				amountToMove.y = jumpHeight;
				currentFuel -= 1;
			}
		}

		//SLIDING


		amountToMove.x = currentSpeed;
		verticalSpeed = amountToMove.y;

		physics.Move (amountToMove * Time.deltaTime);

		//ANIMATION

		if (physics.grounded) {
			charAnimator.SetBool(Animator.StringToHash("Jumping"), false);
			if (Mathf.Abs(currentSpeed) > 1f) {
				charAnimator.SetBool(Animator.StringToHash("Running"), true);

				float fixedSpeed = (Mathf.Abs (currentSpeed) / speed) * 11f;
				if (fixedSpeed < 3f) fixedSpeed = 3f;

				charAnimator.SetFloat (Animator.StringToHash("RunSpeed"), fixedSpeed);
				if (Mathf.Sign(currentSpeed) == 1f) {
					graphics.localScale = new Vector3 (1f, 0.5f, 1f);
					slideParticle.transform.localRotation = Quaternion.Euler (new Vector3 (0, 180, 0));
				} else {
					graphics.localScale = new Vector3 (1f, 0.5f, -1f);
					slideParticle.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, 0));
				}
			} else {
				charAnimator.SetBool(Animator.StringToHash("Running"), false);

			}

		} else {
			slideParticle.Stop ();
			charAnimator.SetBool(Animator.StringToHash("Running"), false);
			charAnimator.SetBool(Animator.StringToHash("Jumping"), true);
			charAnimator.SetFloat(Animator.StringToHash("JumpingForwardBackward"), (currentSpeed / speed) * graphics.localScale.z);
			charAnimator.SetFloat(Animator.StringToHash("JumpingUpwardsDownwards"), verticalSpeed/ speed);

		}
	}

	public float GetCurrentSpeed() {
		return currentSpeed;
	}

	public float GetVerticalSpeed() {
		return verticalSpeed;
	}

	private float IncrementTowards(float current, float target) {
		if (current == target) {
			return current;
		} else {
			float dir = Mathf.Sign (target - current);
			current += acceleration * Time.deltaTime * dir;
			return (dir == Mathf.Sign (target-current))? current : target;
		}
	}

	public int GetCurrentHealth() {

		return currentHealth;
	}

	public void Refuel() {
		currentFuel = maxFuel;
	}

	public void Heal() {
		currentHealth = maxHealth;
	}

	public void TakeDamage(int damage) {
		currentHealth -= damage;
		recentlyHit = true;
		GetComponent<Renderer>().material = hitFadeMaterial;

		if (currentHealth <= 0) {
			GameManager.gameManager.Respawn ();
		}
	}

	void OnGUI() {
		GUI.skin = skin;
		GUI.Label (new Rect (Screen.width / 2 + 100, Screen.height - 70, 200, 50), "Fuel: " + currentFuel);
	}
}
