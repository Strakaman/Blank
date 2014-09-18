using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour
{
	public GameObject refBullet;
	public GameObject refBullet2;
	public Direction direction=0; //0 is down, 1 is up, 2 is left, 3 is right
	public Animator animator;
	public const float speed = 10;

	public enum Direction {
		down = 0,
		up = 1,
		left = 2,
		right = 3
	};

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

	void CheckDirection ()
	{
		if (rigidbody2D.velocity.x < 0) {
			direction = Direction.left;
		}
		else if (rigidbody2D.velocity.x > 0) {
			direction = Direction.right;
		}
		else if (rigidbody2D.velocity.y < 0) {
			direction = Direction.down;
		}
		else if (rigidbody2D.velocity.y > 0) {
			direction = Direction.up;
		}
		Debug.Log ("Direction: " + direction);
	}

	void SpriteAnimation() {
		/*if (rigidbody2D.velocity.x != 0 || rigidbody2D.velocity.y != 0) {
			animator.SetBool ("doWalk", true);
		} else {
		animator.SetBool ("doWalk", false);
		}*/
		if (rigidbody2D.velocity.x != 0 || rigidbody2D.velocity.y != 0) {
			if (direction == Direction.down) {
				SetBools(true,false,false,false);
			} else if (direction == Direction.up) {
				SetBools(false,true,false,false);
			} else if (direction == Direction.left) {
				SetBools(false,false,true,false);
			} else if (direction == Direction.right) {
				SetBools(false,false,false,true);
			}
		}
		else {
			if (direction == Direction.down) {
				animator.SetBool ("Down", false);
			} else if (direction == Direction.up) {
				animator.SetBool ("Top", false);
			} else if (direction == Direction.left) {
				animator.SetBool ("Left", false);
			} else if (direction == Direction.right) {
				animator.SetBool ("Right", false);
			}
		}
	}

	public void SetBools(bool down, bool up, bool left, bool right)
	{
		animator.SetBool ("Down", down);
		animator.SetBool ("Top", up);
		animator.SetBool ("Left", left);
		animator.SetBool ("Right", right);
	}

	/**
 * Used for all player button inputs...I guess...
 */
	void CheckInputs ()
	{
			//Consider modifying the same vector everytime instead of creating a new one, performance win?
			rigidbody2D.velocity = new Vector2 (Input.GetAxis ("Horizontal") * speed, Input.GetAxis ("Vertical") * speed);
		CheckDirection ();
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
		Physics2D.IgnoreCollision (clonedesu.collider2D, clonedesu.collider2D);
		if (direction == Direction.down) {
					clonedesu.rigidbody2D.velocity = new Vector3 (0, -speed, 0);
			}
		else if (direction == Direction.up) {
					clonedesu.rigidbody2D.velocity = new Vector3 (0, speed, 0);
			}
		else if (direction == Direction.left) {
					clonedesu.rigidbody2D.velocity = new Vector3 (-speed, 0, 0);
			}
		else if (direction == Direction.right) {
					clonedesu.rigidbody2D.velocity = new Vector3 (speed, 0, 0);
			}
			Destroy (clonedesu, 2);
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
