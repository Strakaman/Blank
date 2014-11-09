using UnityEngine;
using System.Collections;

public class ChainEffect : MonoBehaviour {
	private bool checkEnemyInRadius;
	private GameObject colTarget;
	private GameObject target;
	private Vector2 targetDirection;
	private float Xdif;
	private float Ydif;
	private int projectileSpeed = 10;

	// Use this for initialization
	void Start () {
		checkEnemyInRadius = false;
	}

	//When projectile collides with enemy, check if there is an enemy within range.
	void OnCollisionEnter2D(Collision2D collInfo) {
		//Debug.Log(collInfo.gameObject.name);	
		if (Utilities.hasMatchingTag("Enemy",collInfo.gameObject)) {
			checkEnemyInRadius = true;
			gameObject.collider2D.enabled = false;
			//Physics2D.IgnoreCollision(gameObject.collider2D, collInfo.gameObject.collider2D);
			colTarget = collInfo.gameObject;
			Destroy (gameObject, .5f);
			//Debug.Log("True");
		} else {
			Destroy (gameObject, .5f);
		}
	}

	//While an enemy is within range, set new trajectory towards new enemy
	void OnTriggerStay2D(Collider2D collInfo) {
		//Debug.Log (Utilities.hasMatchingTag("Enemy",collInfo.gameObject));
		if (checkEnemyInRadius == true && Utilities.hasMatchingTag("Enemy", collInfo.gameObject) && collInfo != colTarget) {
			//Debug.Log ("CHAIN!");
			//Invoke("enableCollider()", 0.1f);
			gameObject.collider2D.enabled = true;
			target = collInfo.gameObject;
			projectileTrajectory(target);
			//gameObject.transform.position = target.transform.position;
			Destroy (gameObject, .5f);
		}
	}

	//Trajectory of the projectile towards the target
	void projectileTrajectory (GameObject target)
	{
		//Debug.Log ("CHAIN!");
		Xdif = target.transform.position.x - transform.position.x;
		Ydif = target.transform.position.y - transform.position.y;
		targetDirection = new Vector2 (Xdif, Ydif);
		rigidbody2D.velocity = (targetDirection.normalized * projectileSpeed);
		//transform.position = target.transform.position;
	}
}
