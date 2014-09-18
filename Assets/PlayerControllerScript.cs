using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour
{
	public GameObject refBullet;
	public GameObject refBullet2;
	public int direction = 0; //0 is down, 1 is up, 2 is left, 3 is right
	public Animator animator;
	public const float speed = 10;
	// Use this for initialization
	void Start ()
	{
			animator = (Animator)GetComponent ("Animator");
	}

	// Update is called once per frame
	void Update ()
	{
			CheckInputs ();
			SpriteAnimation ();
	}

	void SpriteAnimation ()
	{
			if (rigidbody2D.velocity.x != 0 || rigidbody2D.velocity.y != 0) {
					animator.SetBool ("doWalk", true);
					CheckDirection ();
			} else {
					animator.SetBool ("doWalk", false);
			}
	}

	void CheckDirection ()
	{
			if (rigidbody2D.velocity.x < 0) {
					direction = 2;
			}
			else if (rigidbody2D.velocity.x > 0) {
					direction = 3;
			}
			else if (rigidbody2D.velocity.y < 0) {
					direction = 0;
			}
			else{
					direction = 1;
			}
	}

	/**
 * Used for all player button inputs...I guess...
 */
	void CheckInputs ()
	{
			//Consider modifying the same vector everytime instead of creating a new one, performance win?
			rigidbody2D.velocity = new Vector2 (Input.GetAxis ("Horizontal") * speed, Input.GetAxis ("Vertical") * speed);
			if (Input.GetButtonDown ("Fire Spell")) {
					FireSpell ();
			}
			if (Input.GetButtonDown ("Bounce Spell")) {
					BounceSpell ();
			}
	}

	void createProjectile (GameObject bulletToClone)
	{
			GameObject clonedesu = (GameObject)Instantiate (bulletToClone, transform.position, transform.rotation);
			Physics2D.IgnoreCollision (clonedesu.collider2D, collider2D);
			if (direction == 0) {
					clonedesu.rigidbody2D.velocity = new Vector3 (0, -speed, 0);
			}
			else if (direction == 1) {
					clonedesu.rigidbody2D.velocity = new Vector3 (0, speed, 0);
			}
			else if (direction == 2) {
					clonedesu.rigidbody2D.velocity = new Vector3 (-speed, 0, 0);
			}
			else if (direction == 3) {
					clonedesu.rigidbody2D.velocity = new Vector3 (speed, 0, 0);
			}
			Destroy (clonedesu, 3);
	}

	/**
 * Offensive spell for player 
 */
	void FireSpell ()
	{
			createProjectile (refBullet);
	}

	void BounceSpell ()
	{
			createProjectile (refBullet2);
	}
}
