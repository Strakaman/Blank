using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour {
	public GameObject refBullet;
	public GameObject refBullet2;
	public int direction = 0; //0 is down, 1 is up, 2 is left, 3 is right
	public Animator animator;
	public const float speed = 10;
	// Use this for initialization
	void Start () {
		animator = (Animator)GetComponent("Animator");
	}
	
	// Update is called once per frame
	void Update () {
		CheckInputs();
		SpriteAnimation ();
	}

	void SpriteAnimation() {
		if (rigidbody2D.velocity.x != 0 || rigidbody2D.velocity.y != 0) {
			animator.SetBool ("doWalk", true);
			CheckDirection();
		} else {
		animator.SetBool ("doWalk", false);
		}
	}
	void CheckDirection() {
		if (rigidbody2D.velocity.x < 0) {
			direction = 2;
		}
		if (rigidbody2D.velocity.x > 0) {
			direction = 3;
		}
		if (rigidbody2D.velocity.y < 0) {
			direction = 0;
		}
		if (rigidbody2D.velocity.y > 0) {
			direction = 1;
		}
	}

	/**
	 * Used for all player button inputs...I guess...
	 */
	void CheckInputs()
	{
		//transform.Translate(new Vector3(Input.GetAxis("Horizontal")*.05f,0,0)); //O: better to create horizontal vector once?
		//transform.Translate(new Vector3(0,Input.GetAxis("Vertical")*.05f,0));   //O: better to create horizontal vector once?
		rigidbody2D.velocity = new Vector2(Input.GetAxis ("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
		if (Input.GetButtonDown("Fire Spell"))
		{
			FireSpell();
		}
		if (Input.GetButtonDown("Bounce Spell"))
		{
			BounceSpell();
		}
	}

		void createProjectile(GameObject refBullet) {
		GameObject clonedesu = (GameObject)Instantiate(refBullet, transform.position, transform.rotation);
		//Physics.IgnoreCollision(refBullet2.collider, collider); //can't find Player01 collider. 
		if (direction == 0) {
			clonedesu.rigidbody2D.velocity = new Vector3(0,-speed,0);
		}
		if (direction == 1) {
			clonedesu.rigidbody2D.velocity = new Vector3(0,speed,0);
		}
		if (direction == 2) {
			clonedesu.rigidbody2D.velocity = new Vector3(-speed,0,0);
		}
		if (direction == 3) {
			clonedesu.rigidbody2D.velocity = new Vector3(speed,0,0);
		}
		Destroy(clonedesu, 3);
	}

	/**
	 * Offensive spell for player 
	 */
	void FireSpell()
	{
		createProjectile (refBullet);
	}



	void BounceSpell() {
		createProjectile (refBullet2);
	}
}
