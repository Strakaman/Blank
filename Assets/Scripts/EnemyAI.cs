using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	public float chaseTime = 2;
	public float speed;
	private Vector2 Playerdirection;
	private float Xdif;
	private float Ydif;
	private bool playerInSight;                      // Whether or not the player is currently sighted.
	private GameObject player;                      // Reference to the player.
	private Vector3 playerTransform;                      // Reference to the player's transform.

	void Start() {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update () {
		//Player = GameObject.FindGameObjectWithTag ("Player").transform.position;
		//rigidbody2D.velocity = (Playerdirection.normalized * speed);
		playerTransform = player.transform.position;
		if (playerInSight) {
			//Debug.Log ("Chasing");
			Chasing();
		}
	}

	public void setPlayerInSightTrue() {
		playerInSight = true;
	}
	public void setPlayerInSightFalse() {
		playerInSight = false;
	}

	void Chasing ()
	{
		Xdif = playerTransform.x - transform.position.x;
		Ydif = playerTransform.y - transform.position.y;
		
		Playerdirection = new Vector2 (Xdif, Ydif);
		rigidbody2D.velocity = (Playerdirection.normalized * speed);
	}
}
