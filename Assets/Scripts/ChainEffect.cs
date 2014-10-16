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
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D collInfo) {
		//Debug.Log(collInfo.gameObject.name);	
		if (Utilities.hasMatchingTag("Enemy",collInfo.gameObject)) {
			checkEnemyInRadius = true;
			gameObject.collider2D.enabled = false;
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
