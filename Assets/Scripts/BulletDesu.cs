﻿using UnityEngine;
using System.Collections;

public class BulletDesu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ApplyVelocity()
	{
		rigidbody2D.velocity = new Vector3(0,2,0);
		Debug.Log(rigidbody2D.velocity);
	}


	
}