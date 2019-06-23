using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour{
	public static bool globalPlayerInSight = false; //If true, all enemies in scene will chase player. 
	public bool playerInSight; // Whether or not the player is currently sighted.
	public float health = 100; //Initial health
	public float maxHealth = 100; //Max health
	public int collisionDamDealt = 10; //Damage enemy deals to player
	public int knockbackDealt = 1000; //Knockback enemy deals to player
	public float hitDelay = 0.5f; //Delay before enemy can deal damage between each attack
	protected Animator animator;
	public string enemyName;
	public Direction direction = 0; //0 is down, 1 is up, 2 is left, 3 is right
	protected SpriteRenderer healthBar; //Health bar sprite to be used
	protected Vector3 healthVector; //Vector of health bar
	protected float healthScale; //Scale health bar to size of health container.
	protected float hitTime;
	public float slowTime = 5f; //Slow duration
	public float stunTime = 3f; //Stun duration
	public Material Default;
	public Material Hit;
	public Material Stun;
	public Material Slow;
	protected bool stunned = false;
	protected bool slowed = false; 
	protected bool isHit = false;
	protected bool alreadyStunned = false;
	protected bool alreadySlowed = false;
	protected bool isDying = false;
	protected GameObject enemy;
	protected GameObject player;
    protected SpriteRenderer m_SpriteRenderer;
	public AudioClip deathSound;
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
		if (globalPlayerInSight) {
			playerInSight = true;
		}
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
			damageProperties(coll.gameObject, 5, 0, 0.1f);
			stunned = true;
		}
	}

	void OnCollisionStay2D(Collision2D collInfo)
	{	
		if (Utilities.hasMatchingTag ("Player", collInfo.gameObject)) {
				DamageStruct damagePlayerStruct = new DamageStruct (collisionDamDealt, GetComponent<Collider2D>().gameObject, knockbackDealt, hitDelay);
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
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
	}


	protected void HealthUpdate() {
		if (health > maxHealth) {
			health = maxHealth;
		}
		if (health <= 0) {
			health = 0;
		}
		healthScale = health / maxHealth;
		healthBar.transform.localScale = new Vector3 (healthVector.x * healthScale, 1, 1);
	}

	protected void checkIsDead() {
		//Debug.Log("Health is: " + health);
		if (!isDying) { //check to see if they are already in dying state
			if (health <= 0) {
				GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
				isDying = true;
				if (animator) {
					animator.SetBool("Death", true);
				}
				if (GetComponent<AudioSource>())
				{
					GetComponent<AudioSource>().clip = deathSound;
					GetComponent<AudioSource>().Play();
				}
				Destroy(gameObject, 1);
				gameObject.GetComponent<Collider2D>().enabled = false;
			}
		}
	}
	protected void playerMaterial() {
		/*
		 * call to update after getting hit so that we go back to default material,
		 * but if the enemy is stunned or slowed, we shouldn't go back to the old sprite
		 */ 
		if ((hitTime + 0.1f < Time.time)&&(!alreadyStunned)&&(!alreadySlowed)) {
			m_SpriteRenderer.material = Default;
		}
		
		if (slowed == true) {
            m_SpriteRenderer.material = Slow;
			if (!alreadySlowed) {
				alreadySlowed = true;
				GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x/2, GetComponent<Rigidbody2D>().velocity.y/2);	
				Invoke ("setSlowFalse", slowTime);
			}
		}
		
		if (stunned == true)  {
            m_SpriteRenderer.material = Stun;   
			if(!alreadyStunned){
				alreadyStunned = true;
				//GetComponent<SpriteRenderer>().material = Default;
				GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
				Invoke("setStunFalse", stunTime);
			}
		}
		
		if (hitTime +0.1f >= Time.time)
		{
            m_SpriteRenderer.material = Hit;
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
		if (GetComponent<Rigidbody2D>().velocity.x < -1) {
			direction = Direction.left;
		} else if (GetComponent<Rigidbody2D>().velocity.x > 1) {
			direction = Direction.right;
		} else if (GetComponent<Rigidbody2D>().velocity.y < 0) {
			direction = Direction.down;
		} else if (GetComponent<Rigidbody2D>().velocity.y > 0) {
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
		if (GetComponent<Rigidbody2D>().velocity.x != 0 || GetComponent<Rigidbody2D>().velocity.y != 0) {
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
		if (GetComponent<Rigidbody2D>().velocity.x == 0 && GetComponent<Rigidbody2D>().velocity.y == 0) {
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
        m_SpriteRenderer.material = Hit;
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
			GetComponent<Rigidbody2D>().AddForce(new Vector2(-horizontalPush, -verticalPush) * knockback);
		}
	}

	//Sets the damage for which the enemy will take. 
	protected void takeDamage(int damage) {
		playerInSight = true;
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

	public void setPlayerInSightTrue() {
		playerInSight = true;
	}
	public void setPlayerInSightFalse() {
		playerInSight = false;
	}

	public bool getPlayerInSight() {
		return playerInSight;
	}


}
