using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	private Vector3 Player;	
	private Vector2 Playerdirection;
	private float Xdif;
	private float Ydif;
	public float speed;
	
	void Update () {
		Player = GameObject.FindGameObjectWithTag ("Player").transform.position;

		Xdif = Player.x - transform.position.x;
		Ydif = Player.y - transform.position.y;

		Playerdirection = new Vector2 (Xdif, Ydif);

		rigidbody2D.velocity = (Playerdirection.normalized * speed);
	}
}
