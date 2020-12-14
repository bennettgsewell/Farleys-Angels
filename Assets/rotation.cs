using UnityEngine;
using System.Collections;



public class rotation : MonoBehaviour {

	//set turn speed to negative to make it rotate the other way
	public float turnspeed;

	void Update () {

		float speed = Time.deltaTime;

		transform.Rotate (0, speed * turnspeed, 0);

	}
}