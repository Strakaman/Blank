using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour{
	public float health = 100; //Initial health
	public float maxHealth = 100; //Max health
	public int collisionDamDealt = 10; //Damage enemy deals to player
	public int knockbackDealt = 1000; //Knockback enemy deals to player
	public float hitDelay = 0.5f; //Delay before enemy can deal damage between each attack
	protected Animator animator;
	public string enemyName;
	public Direction direction = 0; //0 is down, 1 is up, 2 is left, 3 is right
	protected SpriteRenderer healthBar;
	protected Vector3 healthVector;
	protected float healthScale;
	protected float hitTime;
	public float slowTime;
	public float stunTime;
	public Material Default;
	public Material Hit;
	public Material Stun;
	public Material Slow;
	protected bool stunned = false;
	protected bool slowed = false; 
	protected bool isHit = false;
	protected bool alreadyStunned = false;
	protected bool alreadySlowed = false;
	protected GameObject enemy;
	protected GameObject player;
	
	// Use this for initialization
	void Start () {
		enemyStart ();
	}

	/*In order to protect Start in classes that inherit Enemy. Allows use of Start in classes that inherit Enemy.
	* Be sure to call in Start for every class that inherits enemy and if making changes to Start.
	*/
	protected void enemyStart() {
		getAnimator ();
		setObjects ();
	}

	// Update is called once per frame
	void Update () {
		enemyUpdate ();
	}

	/*In order to protect Update in classes that inherit Enemy. Allows use of Update in classes that inherit Enemy.
	 * Be sure to call in Update for every class that inherits Enemy and if making changes to Update.
	 */
	protected void enemyUpdate() {
		checkIsDead ();
		SpriteAnimation ();
		HealthUpdate();
		playerMaterial ();
	}

	void OnCollisionEnter2D(Collision2D coll) {
		//If yellow spell object collides with enemy, stun the enemy.
		if (Utilities.hasMatchingTag("YellowSpellObject",coll.gameObject)) {
			damageProperties(coll.gameObject, 0, 0, 0.1f);
			stunned = true;
		}
	}

	void OnCollisionStay2D(Collision2D collInfo)
	{	
		if (Utilities.hasMatchingTag ("Player", collInfo.gameObject)) {
				DamageStruct damagePlayerStruct = new DamageStruct (collisionDamDealt, collider2D.gameObject, knockbackDealt, hitDelay);
				//struct used to pass more than one parameter through send message, which only lets you pass one object as a parameter
				collInfo.gameObject.SendMessage ("callDamage", damagePlayerStruct);
		}
	}

	void OnTriggerStay2D(Collider2D coll) {
		/*if (Utilities.hasMatchingTag("BlueSpellObject",coll.gameObject)) {
			//Debug.Log("I  blue myself");
			slowed = true;	
			GetComponent<SpriteRenderer>().material = Slow;;}*/ //deprecated once slowing was moved to BlueSlower class
	}

	void getAnimator() {
		animator = (Animator)GetComponent ("Animator");
		foreach (Transform child in transform) {
			foreach (Transform grandChild in child) {
				GameObject hpbar = grandChild.gameObject;
				if (hpbar.CompareTag ("HealthBar")) {
					healthBar = hpbar.GetComponent<SpriteRenderer> ();
					healthVector = healthBar.transform.localScale;
					healthScale = health / maxHealth;
					break;
				}
			}
		}
	}
	
	void setObjects() {
		player = GameObject.FindGameObjectWithTag ("Player");
		enemy = gameObject;
	}


	protected void HealthUpdate() {
		if (health > maxHealth) {
			health = maxHealth;
		}
		healthScale = health / maxHealth;
		healthBar.transform.localScale = new Vector3 (healthVector.x * healthScale, 1, 1);
	}

	protected void checkIsDead() {
		//Debug.Log("Health is: " + health);
		if (health <= 0) {
			rigidbody2D.velocity = new Vector2(0, 0);
			if (animator) {
				animator.SetBool("Death", true);
			}
			Destroy(gameObject, 1);
			gameObject.collider2D.enabled = false;
		}
	}
	protected void playerMaterial() {
		/*
		 * call to update after getting hit so that we go back to default material,
		 * but if the enemy is stunned or slowed, we shouldn't go back to the old sprite
		 */ 
		if ((hitTime + 0.1f < Time.time)&&(!alreadyStunned)&&(!alreadySlowed)) {
			GetComponent<SpriteRenderer>().material = Default;
		}
		
		if (slowed == true) {
			GetComponent<SpriteRenderer>().material = Slow;
			if (!alreadySlowed) {
				alreadySlowed = true;
				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x/2, rigidbody2D.velocity.y/2);	
				Invoke ("setSlowFalse", 5f);
			}
		}
		
		if (stunned == true)  {
			GetComponent<SpriteRenderer>().material = Stun;   
			if(!alreadyStunned){
				alreadyStunned = true;
				//GetComponent<SpriteRenderer>().material = Default;
				rigidbody2D.velocity = new Vector2(0,0);
				Invoke("setStunFalse", 3f);
			}
		}
		
		if (hitTime +0.1f >= Time.time)
		{
			GetComponent<SpriteRenderer>().material = Hit;
		}
	}

	
	//Checks and sets the animation state for the player
	protected void SpriteAnimation ()
	{
		if (Time.timeScale!=0) 
		{
			if (animator) {
				SetDirection ();
				setWalk ();
				setIdle ();
			}
		}
	}

	//Sets the direction for the player
	protected void SetDirection ()
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

	//returns direction of Player
	public Direction getDirection() {
		return direction;
	}
	
	//Sets the walking animation for the player if they are moving
	protected void setWalk ()
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
	protected void setIdle ()
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

	//Method to set the animation for all four directions
	public void SetBools (bool down, bool up, bool left, bool right)
	{
		animator.SetBool ("Down", down);
		animator.SetBool ("Top", up);
		animator.SetBool ("Left", left);
		animator.SetBool ("Right", right);
	}

	protected void callDamage(DamageStruct dstruct)
	{
		damageProperties(dstruct.coll.gameObject, dstruct.damage, dstruct.knockback, dstruct.hitDelay);
		GetComponent<SpriteRenderer>().material = Hit;
	}

	//Method to set the four parameters when damaging something: object to collide with, amount of damage
	//amount of knockback, and delay before being able to take damage again. 
	protected void damageProperties(GameObject collInfoObj, int damage, int knockback, float hitdelay) {
		if (hitTime + hitdelay < Time.time) {
			hitTime = Time.time;
			takeDamage(damage);
			isHit = true;
			float verticalPush = collInfoObj.transform.position.y - transform.position.y;
			float horizontalPush = collInfoObj.transform.position.x - transform.position.x;
			rigidbody2D.AddForce(new Vector2(-horizontalPush, -verticalPush) * knockback);
		}
	}

	//Sets the damage for which the enemy will take. 
	protected void takeDamage(int damage) {
		if (gameObject.GetComponent<EnemyAIOld> ()) {
			gameObject.GetComponent<EnemyAIOld> ().setPlayerInSightTrue ();
		}
		health -= damage*PlayerInfo.GetPowerModifier();
	}

	public bool isHitTrue() {
		return isHit;
	}
	public void isHitFalse() {
		isHit = false;
	}

	protected bool withinRange() {
		return false;
	}
	
	protected void setStunFalse() {
		stunned = false;
		alreadyStunned = false;
	}

	protected void StunYourself() {
		stunned = true;
	}
	
	protected void SlowYourself()
	{
		slowed = true;
	}
	
	protected void setSlowFalse() {
		slowed = false;
		alreadySlowed = false;
	}

	public bool isStunned() {
		return stunned;
	}
}
