using UnityEngine;
using System.Collections;

public class ChainEffect : MonoBehaviour {
	public GameObject chainChecker;
	private GameObject target;
	private Vector2 targetDirection;
	private float Xdif;
	private float Ydif;
	private int projectileSpeed = 10;
	private float sourcePitch; //store the Audio source pitch so that we can vary it
	// Use this for initialization
	void Start () {
		if (GetComponent<AudioSource>()) {
			sourcePitch = GetComponent<AudioSource>().pitch; //get the original pitch for some fun math
		}
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D collInfo) {
		//Debug.Log(collInfo.gameObject.name);	
		if (Utilities.hasMatchingTag("Enemy",collInfo.gameObject)) {
			Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(),collInfo.gameObject.GetComponent<Collider2D>()); //so that it can pass through any enemy that it already hit
			//Debug.Log(gameObject.collider2D.isTrigger);
			//gameObject.collider2D.enabled = false;
			chainChecker.SendMessage("BeastMode",collInfo.gameObject); //trigger the child to check if any enemies are in range
			if(GetComponent<AudioSource>())
			{	//Theooooooretically, random number will vary sligthly so we can get a good +/- on the pitch variation
				GetComponent<AudioSource>().pitch = sourcePitch + ((Random.Range(1,10)/40f) - .125f);
				GetComponent<AudioSource>().Play();
			}
			Destroy (gameObject, 1f); //should last longer if it hit at least one enemy
		} 
		else
		{
			Destroy (gameObject, .25f); //destroy regardless
		}
	}

	/*
	 * Based on the the new target's destination and this object's current location,
	 * calculates a direction vector and sends the projectile on it's way!
	 */ 
	void projectileTrajectory (GameObject target)
	{
		//Debug.Log ("CHAIN!");
		Xdif = target.transform.position.x - transform.position.x;
		Ydif = target.transform.position.y - transform.position.y;
		targetDirection = new Vector2 (Xdif, Ydif);
		GetComponent<Rigidbody2D>().velocity = (targetDirection.normalized * projectileSpeed);
		//transform.position = target.transform.position;
	}
}
