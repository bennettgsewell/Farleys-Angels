using UnityEngine;
using System.Collections;

public class Parallaxer : MonoBehaviour {

	public bool keepOriginalLocation = false;

	public bool looping = true;
	public bool reverseScale = false;
	public bool verticalParallaxing = true;
	public float verticalParallaxingMultiplyer = 0.5f;
	public bool verticalLooping = false;

	private float parallaxScale;
	public float smoothing = 1f;

	private Transform cam;
	private Vector3 previousCamPos;


	public int offsetX = 2;
	public int offsetY = 4;
	private bool hasALeftBuddy = false;
	private bool hasARightBuddy = false;

	private bool hasATopBuddy = false;
	private bool hasABottomBuddy = false;
	private float objectWidth = 0f;
	private float objectHeight = 0f;




	// Use this for initialization
	void Start () {
		cam = Camera.main.transform;
		parallaxScale = transform.position.z * -1;
		previousCamPos = cam.position;
		objectWidth = transform.localScale.x;
		objectHeight = transform.localScale.y;

		if (keepOriginalLocation == true) {
			float startOffsetX = (transform.position.x - cam.position.x) * Mathf.Sqrt(parallaxScale);
			float startOffsetY = (transform.position.y - cam.position.y) * parallaxScale * verticalParallaxingMultiplyer;
			float targetPosX = transform.position.x + startOffsetX;
			float targetPosY = transform.position.x + startOffsetY;

			if (verticalParallaxing == false) {
				targetPosY = transform.position.y;
			}
			transform.position = new Vector3 (targetPosX, targetPosY, transform.position.z);
			Debug.Log (transform.position);
		}

	}
	
	// Update is called once per frame
	void Update () {
		float parallaxX = (previousCamPos.x - cam.position.x) * parallaxScale;
		float parallaxY = (previousCamPos.y - cam.position.y) * parallaxScale * verticalParallaxingMultiplyer;
		float backgroundTargetPosX = transform.position.x + parallaxX;
		float backgroundTargetPosY = transform.position.y + parallaxY;
		if (verticalParallaxing == false) {
			backgroundTargetPosY = transform.position.y;
		}
		Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgroundTargetPosY, transform.position.z);
		transform.position = Vector3.Lerp (transform.position, backgroundTargetPos, smoothing * Time.deltaTime);
		previousCamPos = cam.transform.position;

		if (looping == true) {
			if (hasALeftBuddy == false || hasARightBuddy == false) {
				float camHorizontalExtend = cam.GetComponent<Camera>().orthographicSize * Screen.width/Screen.height;

				float edgeVisiblePositionLeft = (transform.position.x - objectWidth/2) + camHorizontalExtend;
				float edgeVisiblePositionRight = (transform.position.x + objectWidth/2) - camHorizontalExtend;
				if (cam.position.x >= edgeVisiblePositionRight - offsetX && hasARightBuddy == false) {
					MakeNewBuddy (1);
					hasARightBuddy = true;
				}
				else if (cam.position.x <= edgeVisiblePositionLeft + offsetX && hasALeftBuddy == false) {
					MakeNewBuddy (-1);
					hasALeftBuddy = true;
				}

			}
		}

		if (verticalLooping == true) {
			if (hasATopBuddy == false || hasABottomBuddy == false) {
				float camVerticalExtend = cam.GetComponent<Camera>().orthographicSize * Screen.height/Screen.width;

				float edgeVisiblePositionTop = (transform.position.y + objectHeight/2) - camVerticalExtend;
				float edgeVisiblePositionBottom = (transform.position.y - objectHeight/2) + camVerticalExtend;
				if (cam.position.y >= edgeVisiblePositionTop - offsetY && hasATopBuddy == false) {
					MakeNewBuddyVertical(1);
					hasATopBuddy = true;
				}
				else if (cam.position.y <= edgeVisiblePositionBottom + offsetY && hasABottomBuddy == false) {
					MakeNewBuddyVertical(-1);
					hasABottomBuddy = true;
				}
			}
		}
	}

	void MakeNewBuddy (int direction) {
		Vector3 newPosition = new Vector3 (transform.position.x + objectWidth * direction, transform.position.y, transform.position.z);
		Transform newBuddy = Instantiate (transform, newPosition, transform.rotation) as Transform;

		if (reverseScale == true) {
			newBuddy.localScale = new Vector3 (newBuddy.localScale.x*-1, newBuddy.localScale.y, newBuddy.localScale.z);
		}

		newBuddy.parent = transform.parent;

		if (direction > 0) {
			newBuddy.GetComponent<Parallaxer>().hasALeftBuddy = true;
		} else {
			newBuddy.GetComponent<Parallaxer>().hasARightBuddy = true;
		}
	}

	void MakeNewBuddyVertical (int direction) {
		Vector3 newPosition = new Vector3 (transform.position.x, transform.position.y + objectHeight * direction, transform.position.z);
		Transform newBuddy = Instantiate (transform, newPosition, transform.rotation) as Transform;
		
		if (reverseScale == true) {
			newBuddy.localScale = new Vector3 (newBuddy.localScale.x, newBuddy.localScale.y*-1, newBuddy.localScale.z);
		}
		
		newBuddy.parent = transform.parent;
		
		if (direction > 0) {
			newBuddy.GetComponent<Parallaxer>().hasABottomBuddy = true;
		} else {
			newBuddy.GetComponent<Parallaxer>().hasATopBuddy = true;
		}
	}

}
