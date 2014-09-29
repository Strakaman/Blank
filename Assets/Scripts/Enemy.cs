using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour{
	private int health = 100;
	private int damage = 10;
	private Animator animator;
	private string enemyName;
	private int redResis;
	public Direction direction = 0; //0 is down, 1 is up, 2 is left, 3 is right

	// Use this for initialization
	void Start () {
		animator = (Animator)GetComponent ("Animator");
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("Health is: " + health);
		if (health <= 0) {
			Destroy(gameObject, 1);
			gameObject.collider2D.enabled = false;
		}
		if (Time.timeScale!=0) 
		{
			SpriteAnimation ();
		}
	}

	
	//Checks and sets the animation state for the player
	void SpriteAnimation ()
	{
		SetDirection ();
		setWalk ();
		setIdle ();
	}
	
	
	
	//Sets the direction for the player
	void SetDirection ()
	{
		if (rigidbody2D.velocity.x < -1) {
			direction = Direction.left;
		} else if (rigidbody2D.velocity.x > 1) {
			direction = Direction.right;
		} else if (rigidbody2D.velocity.y < 0) {
			direction = Direction.down;
		} else if (rigidbody2D.velocity.y > 0) {
			direction = Direction.up;
		}
	}
	
	//Sets the walking animation for the player if they are moving
	void setWalk ()
	{
		if (rigidbody2D.velocity.x != 0 || rigidbody2D.velocity.y != 0) {
			if (direction == Direction.down) {
				SetBools (true, false, false, false);
			} else if (direction == Direction.up) {
				SetBools (false, true, false, false);
			} else if (direction == Direction.left) {
				SetBools (false, false, true, false);
			} else if (direction == Direction.right) {
				SetBools (false, false, false, true);
			}
		}
	}
	
	//Sets the idle animation for the player if velocities are 0
	void setIdle ()
	{
		if (rigidbody2D.velocity.x == 0 && rigidbody2D.velocity.y == 0) {
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
	
	public void SetBools (bool down, bool up, bool left, bool right)
	{
		animator.SetBool ("Down", down);
		animator.SetBool ("Top", up);
		animator.SetBool ("Left", left);
		animator.SetBool ("Right", right);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		//Debug.Log ("COLLIDING");
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
