using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour{
	private int health = 100;
	private int damage = 10;
	private Animator animator;
	private string name;
	private int redResis;

	// Use this for initialization
	void Start () {
		animator = (Animator)GetComponent ("Animator");
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("Health is: " + health);
		if (health <= 0) {

			Destroy(gameObject, 1);
			//gameObject.collider.enabled = false;
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log ("COLLIDING");
		if (coll.gameObject.tag == "RedSpellObject") {
			takeDamage(50);
		}
	}

	void Movement() {
		
	}

	void Attack() {



		dealDamage ();
	}

	bool withinRange() {
		return false;
	}

	void takeDamage(int damage) {
		health -= damage;
	}

	void dealDamage() {
		PlayerInfo.changeHealth(-damage);
	}
}
