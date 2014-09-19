using UnityEngine;
using System.Collections;

public class CompanionMovement : MonoBehaviour {
	public Transform target;
	public int degrees = 1;

	void Start() {
		Vector3 orbit = new Vector3(target.position.x + 1, target.position.y + 1, target.position.z);
		this.transform.position = orbit;
	}

	// Update is called once per frame
	void Update () {
		//this.transform.position = target.transform.position * 2;
		transform.RotateAround (target.transform.position, Vector3.forward, degrees);
	}
}
