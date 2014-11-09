using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour{
	//Enemy metadata
	public int health = 100;
	public int damage = 10;
	public int maxHealth = 100;
	private Animator animator;
	private string enemyName;
	public Direction direction = 0; //0 is down, 1 is up, 2 is left, 3 is right
	//Health bar
	protected SpriteRenderer healthbar;
	protected Vector3 healthscale;
	//Enemy status effect states
	protected bool stunned = false;
	protected bool slowed = false; 
	protected bool isHit = false;
	//State durations
	protected float hitTime;
	protected float slowTime;
	protected float stunTime;
	//State materials
	public Material Default;
	public Material Hit;
	public Material Stun;
	public Material Slow;

	// Use this for initialization
	void Start () {
		//Get the animator component in the Enemy object
		animator = (Animator)GetComponent ("Animator");

		//Get the health bars in the children of the Enemy object
		foreach (Transform child in transform) {
			foreach (Transform grandChild in child) {
				GameObject hpbar = grandChild.gameObject;
				if (hpbar.CompareTag ("HealthBar")) {
					healthbar = hpbar.GetComponent<SpriteRenderer> ();
					healthscale = healthbar.transform.localScale;
					break;
				}
			}
		}
	}

	//Transforms the health bar relative to the amount of health the enemy has
	void HealthUpdate() {
		healthbar.transform.localScale = new Vector3 (healthscale.x * health * 0.01f, 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("Health is: " + health);

		//If enemy's health is below 0, destroy Enemy object. 
		if (health <= 0) {
			rigidbody2D.velocity = new Vector2(0, 0);
			animator.SetBool("Death", true);
			Destroy(gameObject, 1);
			gameObject.collider2D.enabled = false;
		}

		//Pause stuff?
		if (Time.timeScale!=0) 
		{
			SpriteAnimation ();
		}

		if (health > maxHealth) {
			health = maxHealth;
		}
		HealthUpdate();

		//Set enemy sprite colors to corresponding Enemy status effect states. 
		if (hitTime + 0.1f < Time.time) {
			GetComponent<SpriteRenderer>().material = Default;
		}
		if (stunned == true) {
			GetComponent<SpriteRenderer>().material = Stun;
			rigidbody2D.velocity = new Vector2(0,0);
			Invoke("setStunFalse", 3f);

		}
		if (slowed == true) {
			GetComponent<SpriteRenderer>().material = Slow;
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x/2, rigidbody2D.velocity.y/2);	
			Invoke ("setSlowFalse", 5f);
		}
	}

	
	//Checks and sets the animation state for the player
	void SpriteAnimation ()
	{
		SetDirection ();
		setWalk ();
		setIdle ();
	}
	
	
	
	//Sets the direction for the enemy
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

	public Direction getDirection() {
		return direction;
	}
	
	//Sets the walking animation for the enemy if they are moving
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
	
	//Sets the idle animation for the enemy if velocities are 0
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

	//Method that abstracts setting directions. 
	public void SetBools (bool down, bool up, bool left, bool right)
	{
		animator.SetBool ("Down", down);
		animator.SetBool ("Top", up);
		animator.SetBool ("Left", left);
		animator.SetBool ("Right", right);
	}

	void OnTriggerStay2D(Collider2D coll) {
		/*if (Utilities.hasMatchingTag("BlueSpellObject",coll.gameObject)) {
			//Debug.Log("I  blue myself");
			slowed = true;	
			GetComponent<SpriteRenderer>().material = Slow;;}*/ //deprecated once slowing was moved to BlueSlower class
	}

	void callDamage(DamageStruct dstruct)
	{
		damageProperties(dstruct.coll.gameObject, dstruct.damage, dstruct.knockback, dstruct.hitDelay);
		GetComponent<SpriteRenderer>().material = Hit;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		//Debug.Log ("COLLIDING");
		//if (Utilities.hasMatchingTag("RedSpellObject",coll.gameObject)) {
		//	damageProperties(coll, 25, 100, 0.1f);
		//	GetComponent<SpriteRenderer>().material = Hit;

		//}
		if (Utilities.hasMatchingTag("YellowSpellObject",coll.gameObject)) {
			damageProperties(coll.gameObject, 0, 0, 0.1f);
			stunned = true;
		}
	}
	void setStunFalse() {
		stunned = false;
	}

	void SlowYourself()
	{
		slowed = true;
		GetComponent<SpriteRenderer>().material = Slow;
	}

	void setSlowFalse() {
		slowed = false;
	}

	void damageProperties(GameObject collInfoObj, int damage, int knockback, float hitdelay) {
		if (hitTime + hitdelay < Time.time) {
			hitTime = Time.time;
			takeDamage(damage);
			isHit = true;
			float verticalPush = collInfoObj.transform.position.y - transform.position.y;
			float horizontalPush = collInfoObj.transform.position.x - transform.position.x;
			rigidbody2D.AddForce(new Vector2(-horizontalPush, -verticalPush) * knockback);
		}
	}

	public bool isHitTrue() {
		return isHit;
	}
	public void isHitFalse() {
		isHit = false;
	}

	bool withinRange() {
		return false;
	}

	void takeDamage(int damage) {
		gameObject.GetComponent<EnemyAI> ().setPlayerInSightTrue();
		health -= damage*PlayerInfo.GetPowerModifier();
	}
}
