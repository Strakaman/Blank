using UnityEngine;
using System.Collections;

public class ChainEffect : MonoBehaviour {
	private bool checkEnemyInRadius;
	public GameObject ChainChecker;
	private GameObject colTarget;
	private GameObject target;
	private Vector2 targetDirection;
	private float Xdif;
	private float Ydif;
	private int projectileSpeed = 10;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D collInfo) {
		//Debug.Log(collInfo.gameObject.name);	
		if (Utilities.hasMatchingTag("Enemy",collInfo.gameObject)) {
			//Debug.Log("actually triggered");

			checkEnemyInRadius = true;
			//Physics2D.IgnoreCollision(gameObject.collider2D,collInfo.gameObject.collider2D);
			//Debug.Log(gameObject.collider2D.isTrigger);
			//gameObject.collider2D.enabled = false;
			colTarget = collInfo.gameObject;
			Destroy (gameObject, .5f);
			//Debug.Log("True");
		} else {
			Destroy (gameObject, .5f);
		}
	}

	void OnTriggerStay2D(Collider2D collInfo) {
		//Debug.Log (Utilities.hasMatchingTag("Enemy",collInfo.gameObject));
		if (checkEnemyInRadius == true && Utilities.hasMatchingTag("Enemy", collInfo.gameObject) && collInfo != colTarget) {
			//Debug.Log ("CHAIN!");

			//gameObject.collider2D.enabled = true;
			target = collInfo.gameObject;
			projectileTrajectory(target);
			Destroy (gameObject, .5f);
		}
	}

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
