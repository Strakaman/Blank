using UnityEngine;
using System.Collections;

public class CompanionMovement : MonoBehaviour {
	public Transform target;
	public int degrees = 1;
	// Update is called once per frame
	void Update () {
		this.transform.position = target.transform.position * 2;
		transform.RotateAround (target.position, Vector3.forward, degrees);
	}
}
