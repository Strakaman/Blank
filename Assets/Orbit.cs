﻿using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour {
	public Transform target;
	public int degrees = 1;
	public float radius = 1;
	public int numOrbits = 1;
	public bool orbitDir = true;

	void Start() {
		Vector3 orbit = new Vector3(target.position.x + radius, target.position.y + radius, target.position.z);
		this.transform.position = orbit;
	}

	// Update is called once per frame
	void Update () {
		//this.transform.position = target.transform.position * 2;
		transform.RotateAround (target.transform.position, Vector3.forward, degrees);
	}
}