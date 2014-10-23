using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour{
	private int health = 100;
	private int damage = 10;
	private Animator animator;
	private string enemyName;
	private int redResis;
	public Direction direction = 0; //0 is down, 1 is up, 2 is left, 3 is right
	private SpriteRenderer healthbar;
	private Vector3 healthscale;
	private float hitTime;
	private float slowTime;
	private float stunTime;
	public Material Default;
	public Material Hit;
	public Material Stun;
	public Material Slow;
	protected bool stunned = false;
	private bool slowed = false; 
	private bool isHit = false;
	
	// Use this for initialization
	void Start () {
		animator = (Animator)GetComponent ("Animator");
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

	void HealthUpdate() {
		healthbar.transform.localScale = new Vector3 (healthscale.x * health * 0.01f, 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("Health is: " + health);
		if (health <= 0) {
			rigidbody2D.velocity = new Vector2(0, 0);
			animator.SetBool("Death", true);
			Destroy(gameObject, 1);
			gameObject.collider2D.enabled = false;
		}
		if (Time.timeScale!=0) 
		{
			SpriteAnimation ();
		}
		if (health > 100) {
			health = 100;
		}
		HealthUpdate();

		if (hitTime + 0.1f < Time.time) {
			GetComponent<SpriteRenderer>().material = Default;
		}
		if (stunned == true) {
			GetComponent<SpriteRenderer>().material = Stun;
			//GetComponent<SpriteRenderer>().material = Default;
			rigidbody2D.velocity = new Vector2(0,0);
			Invoke("setStunFalse", 3f);
		}
		if (slowed == true) {
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

	public Direction getDirection() {
		return direction;
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

	void OnTriggerStay2D(Collider2D coll) {
		if (Utilities.hasMatchingTag("BlueSpellObject",coll.gameObject)) {
			//Debug.Log("I  blue myself");
			slowed = true;	
			GetComponent<SpriteRenderer>().material = Slow;;
		}
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
