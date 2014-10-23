using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour
{
//public GameObject refBullet;   //dont think we need anymore
//public GameObject refBullet2; //dont think we need anymore
	public Direction direction = 0; //0 is down, 1 is up, 2 is left, 3 is right
	public Animator animator;
	public float speed;
	public float bulSpeed;
	public int currSpell = 0; //index used to determine player's current spell, mucho importante
	private Vector3 s; //box collider size to help with raycasting
	private Vector3 c; //box collider center to help with raycasting
	//private bool isPaused = false; //no Idea what this is for, delete?
	private float hittime;
	public Material Default;
	public Material Hit;
	private bool canCharge = true;//charging enabled by defualt
	private float chargeTimeRequired = 1f;
	private float amountChargedSoFar = 0;
	private float chargeStartTime = 0;

// Use this for initialization
	void Start ()
	{
		PlayerInfo.SetState(PState.normal);
		PlayerInfo.setCurrSpell(SpellBook.playerSpells[currSpell]);
			animator = (Animator)GetComponent ("Animator");
			BoxCollider2D zollider = GetComponent<BoxCollider2D> (); //get attached collider, store size and center
			s = zollider.size;
			c = zollider.center;
			
	}

// Update is called once per frame
	void Update ()
	{
		if (Time.timeScale!=0) 
		{
			PlayerInfo.changeMana (1);
			CheckInputs ();
			SpriteAnimation ();
		}
		if (hittime + 0.1f < Time.time) {
			GetComponent<SpriteRenderer>().material = Default;
		}
		if (PlayerInfo.GetState().Equals(PState.grabbing)) {

		}
	}

//Checks and sets the animation state for the player
	void SpriteAnimation ()
	{
			setWalk ();
			setIdle ();
	}

	bool isStrafing ()
	{
			if (Input.GetButton ("Strafe") || PlayerInfo.GetState().Equals(PState.grabbing)) {
					return true;
			} else {
					return false;
			}
	}

//Sets the direction for the player
	void SetDirection ()
	{
			if (rigidbody2D.velocity.x < 0) {
					direction = Direction.left;
			} else if (rigidbody2D.velocity.x > 0) {
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

/**
* Used for all player button inputs...I guess...
*/
	void CheckInputs ()
	{
				//Consider modifying the same vector everytime instead of creating a new one, performance win?
				rigidbody2D.velocity = new Vector2 (Input.GetAxis ("Horizontal") * speed* PlayerInfo.GetSpeedModifier() * PlayerInfo.GetGrabModifier(), 
		                                    		Input.GetAxis ("Vertical") * speed * PlayerInfo.GetSpeedModifier() * PlayerInfo.GetGrabModifier());
				if (Input.GetButtonDown ("Spell Cycle Up")) {
						changeSpell (true);
				} else if (Input.GetButtonDown ("Spell Cycle Down")) {
						changeSpell (false);
				}

				if (isStrafing () == false) {
						SetDirection ();
				}


		if (Input.GetButtonDown ("Use Spell") && PlayerInfo.GetState().Equals(PState.normal)) {
						if (SpellBook.playerSpells [currSpell] == null) {
		
								changeSpell (true);
						}
						Spell datSpell = SpellBook.playerSpells [currSpell];
						if (datSpell.hasEnoughMana ()) {
								datSpell.subMana ();
								datSpell.cast (direction);
								animator.SetBool ("Attack", true);
								Invoke ("stopAttackAnim", 0.5f);
						} else {
								Debug.Log ("You're out of mana kupo"); //used as placeholder until some method to communicate to player is implemented.
						}
						if (canCharge)
							chargeStartTime = Time.time;
				}
	
			//Charged spell
		if (canCharge && Input.GetButton ("Use Spell") && PlayerInfo.GetState().Equals(PState.normal)) {
				amountChargedSoFar = Time.time - chargeStartTime;
			}

		if (canCharge && Input.GetButtonUp("Use Spell") && PlayerInfo.GetState().Equals(PState.normal)){
				if (amountChargedSoFar > chargeTimeRequired){
					if (SpellBook.playerSpells [currSpell] == null) {
						changeSpell (true);
					}
					Spell datSpell = SpellBook.playerSpells [currSpell];
					
					//Charged spells have lower cost than regular spells.
					if (amountChargedSoFar > 5.0f) amountChargedSoFar = 5.0f; //maximum charge is 5 seconds
					int newCost = (int)Mathf.Ceil(datSpell.getCost()*0.75f);
					
					int oldCost = datSpell.getCost();
					datSpell.setCost(newCost);
					bool logged = false;

					if (PlayerInfo.getMana() >= datSpell.getCost()*5*amountChargedSoFar){
						Invoke("startAttackAnim", 0f);
						for (int i = 0; i<amountChargedSoFar*5-1; i++){
							Invoke("startAttackAnim", 0.1f * i);
						if (PlayerInfo.getMana() <= 0 && !logged) Debug.Log ("Out of mana during charged spell.");
						}
					}
					else if (PlayerInfo.getMana() >= datSpell.getCost()){
						Invoke ("startAttackAnim", 0f);
					}
					else {}
					Invoke ("stopChargeAttack", amountChargedSoFar);
					datSpell.setCost(oldCost);
				}
				
				chargeStartTime = 0;
				amountChargedSoFar = 0;
			}


			if (Input.GetButtonDown ("Interact")) {
					GameObject whatCanThouInteractWith;
					if (facingInteractableObject (out whatCanThouInteractWith)) {
							//Debug.Log ("You're facing an interactable object with the name " + whatCanThouInteractWith.name + ", aren't you?");
				whatCanThouInteractWith.SendMessage("interact",gameObject);		
			}
			}

			if (Input.GetButton ("KUSH")) {
				audio.Play();
			}

			/*	if (Input.GetButtonDown ("Bounce Spell")) {
		BounceSpell ();
		animator.SetBool ("Attack", true);
		Invoke("stopAttackAnim", 0.5f);
}*/
	}

	void stopAttackAnim ()
	{
		animator.SetBool ("Attack", false);
	}

	/*IEnumerator chargeAttack(){
		InvokeRepeating("startAttackAnim", 0, 0.2f);
		yield return new WaitForSeconds((int)amountChargedSoFar);
		Invoke ("stopAttackAnim", 0f);
	}*/

	void startAttackAnim ()
	{
		Spell datSpell = SpellBook.playerSpells [currSpell];
		datSpell.cast (direction);		animator.SetBool ("Attack", true); datSpell.subMana();
	}
	void stopChargeAttack(){
		CancelInvoke("startAttackAnim");
	}

	void changeSpell (bool prevFalsenextTrue)
	{
			int numOfSpells = SpellBook.playerSpells.Count;
			if (prevFalsenextTrue) {
					currSpell++;
					if (currSpell >= numOfSpells) {
							currSpell = 0;
					}
					if (SpellBook.playerSpells [currSpell].isSpellUnlocked () == false) {
							changeSpell (true);
					}
			} else {
					currSpell--;	
					if (currSpell < 0) {
							currSpell = numOfSpells - 1;
					}
					if (SpellBook.playerSpells [currSpell].isSpellUnlocked () == false) {

							changeSpell (false);			
					}
			}
		PlayerInfo.setCurrSpell(SpellBook.playerSpells[currSpell]);
	}

	bool facingInteractableObject (out GameObject potentialInteractableICollidedWith)
	{
			Vector2 p = transform.position; //get current player position to cast ray from
			Vector3 castDirection; //set the raycast direction to vertical or horizontal based on direction player is facing
			int xAxisDir = 0;
			int yAxisDir = 0;
			if (direction == Direction.up) {
					yAxisDir = 1;		
			} else if (direction == Direction.down) {
					yAxisDir = -1;
			} else if (direction == Direction.right) {
					xAxisDir = 1;
			} else if (direction == Direction.left) {
					xAxisDir = -1;
			}
			castDirection = new Vector3 (xAxisDir, yAxisDir);
			//Debug.Log ("X Axis Dir: " + xAxisDir + " Y Axis Dir: " + yAxisDir);
			/*it all goes downhill from here, if this code looks awful, that's because it is.
* I tried to make a ray cast that would calculate properly whether you were moving in the x or y direction
* positively or negatively. The code works but I can't even look at it and explain it. 
* Got the idea from the end code at this vid: https://www.youtube.com/watch?v=d3HEFiDFApI
*/
			for (int i = 0; i < 3; i++) {
					float x = (p.x + c.x + (s.x / 2)) - (s.x / 2 * i * (yAxisDir * yAxisDir)) + ((s.x / 2 * (xAxisDir - 1)) * (xAxisDir * xAxisDir));
					float y = (p.y + c.y + (s.y / 2)) - (s.y / 2 * i * (xAxisDir * xAxisDir)) + ((s.y / 2 * (yAxisDir - 1)) * (yAxisDir * yAxisDir));
					Ray ray = new Ray (new Vector3 (x, y, 0), castDirection);
					//Debug.DrawRay(ray.origin,ray.direction);
					RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, .25f, 9);
					if (hit && hit.collider && Utilities.hasMatchingTag("Interactable Object",hit.collider.gameObject)) {
							potentialInteractableICollidedWith = hit.collider.gameObject;
							return true;
					}
			}
			potentialInteractableICollidedWith = null;
			return false;
	}

	void OnCollisionStay2D(Collision2D collInfo)
	{

		if (Utilities.hasMatchingTag("Enemy",collInfo.gameObject))
		{
			damageProperties(collInfo.gameObject, -10, 1000, 0.5f);
		}
	}

	void OnCollisionEnter2D(Collision2D collInfo) {
	//	if (Utilities.hasMatchingTag("EnemyProjectile",collInfo.gameObject))
	//	{
	//		damageProperties(collInfo, -20, 1000, 0.1f);
	//	}
	}

	void callDamage(DamageStruct dstruct)
	{
		damageProperties(dstruct.coll.gameObject, -dstruct.damage, dstruct.knockback, dstruct.hitDelay);
	}

	void damageProperties(GameObject collInfo, int damage, int knockback, float hitdelay) {
		if (PlayerInfo.GetDefenseModifier() == 0)
		{
			return;
		}
		GetComponent<SpriteRenderer>().material = Hit;
		if ((hittime + hitdelay < Time.time)/*&& PlayerInfo.g*/) {
			hittime = Time.time;
			PlayerInfo.changeHealth(damage);
			float verticalPush = collInfo.gameObject.transform.position.y - transform.position.y;
			float horizontalPush = collInfo.gameObject.transform.position.x - transform.position.x;
			rigidbody2D.AddForce(new Vector2(-horizontalPush, -verticalPush) * knockback);
		}
	}
/*void createProjectile (GameObject bulletToClone)
{
GameObject clonedesu = (GameObject)Instantiate (bulletToClone, transform.position, transform.rotation);
Physics2D.IgnoreCollision (clonedesu.collider2D, collider2D);
if (direction == Direction.down) {
		clonedesu.rigidbody2D.velocity = new Vector3 (0, -bulSpeed, 0);
} else if (direction == Direction.up) {
		//clonedesu.transform.rotation = Quaternion.LookRotation(transform.position);
		clonedesu.rigidbody2D.velocity = new Vector3 (0, bulSpeed, 0);
} else if (direction == Direction.left) {
		//clonedesu.transform.rotation = Quaternion.LookRotation(transform.position);
		clonedesu.rigidbody2D.velocity = new Vector3 (-bulSpeed, 0, 0);
} else if (direction == Direction.right) {
		//clonedesu.transform.rotation = Quaternion.LookRotation(transform.position);
		clonedesu.rigidbody2D.velocity = new Vector3 (bulSpeed, 0, 0);
}
Destroy (clonedesu, 2);
}*/
/**
* Offensive spell for player 
*/
/*void FireSpell ()
{
//SpellBook.playerSpells [0].cast (direction);
createProjectile (refBullet);
}

void BounceSpell ()
{
createProjectile (refBullet2);
}*/
}
