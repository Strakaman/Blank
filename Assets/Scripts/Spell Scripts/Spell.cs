using UnityEngine;
using System.Collections;

public abstract class Spell: ScriptableObject
{
	protected string spellName; //for display purposes
	protected string description; //for display purposes
	protected GameObject player; //get transform for casting purposes
	protected int manaCost; //can't cast if you don't have enough mana
	protected bool isUnlocked = false; //spell is locked by default
	protected int projectileSpeed=0; //for spell that shoot something out
	protected float animationDuration;

	//use as fake constructor
	public virtual void initializeSpell(string n, string d, int m, float a)
	{
		spellName = n;
		description = d;
		manaCost = m;
		animationDuration = a;
		player = GameObject.FindGameObjectWithTag("Player");
	}

	//unlocks spell so it can be reached when cycling through spells
	public void unlockSpell()
	{
		isUnlocked = true;
	}

	//use to lock spell in case player unlocked a spell and then died
	public void relockSpell()
	{
		isUnlocked = false;
	}

	//tells if spell is unlocked, used to determine when cycling through spells whether or not to skip past this one.
	public bool isSpellUnlocked()
	{
		return isUnlocked;
	}

	//for spells that shoot something. uses player direction to figure out which way to fire.
	public void createProjectile(Direction direction, GameObject bulletToClone)
	{
		if (player == null){player = GameObject.FindGameObjectWithTag("Player");}
		Vector3 clonePosition = new Vector3(0,0,0) ;
		Vector3 cloneVelocity = new Vector3(0,0,0);
		Quaternion cloneOrientation = Quaternion.Euler(0,0,0); 
		if (direction == Direction.down) {
			clonePosition = player.transform.position + new Vector3(0,-1,0);
			cloneVelocity = new Vector3 (0, -projectileSpeed, 0);
			cloneOrientation = Quaternion.Euler(0, 0, 270);
		}
		else if (direction == Direction.up) {
			clonePosition = player.transform.position + new Vector3(0,1,0);
			cloneVelocity = new Vector3 (0, projectileSpeed, 0);
			cloneOrientation = Quaternion.Euler(0, 0, 90);
		}
		else if (direction == Direction.left) {
			clonePosition = player.transform.position + new Vector3(-1, 0, 0);
			cloneVelocity = new Vector3 (-projectileSpeed, 0, 0);
			cloneOrientation = Quaternion.Euler(0, 0, 180);
		}
		else if (direction == Direction.right) {
			clonePosition = player.transform.position + new Vector3(1,0,0);
			cloneVelocity = new Vector3 (projectileSpeed, 0, 0);
			cloneOrientation = Quaternion.Euler(0, 0, 0);
		}
		//GameObject clonedesu = createSpellObject(direction, bulletToClone, clonePosition, cloneVelocity, cloneOrientation);
		//Debug.Log(cloneVelocity);
		GameObject clonedesu = Utilities.cloneObject(direction, bulletToClone, clonePosition, cloneVelocity, cloneOrientation);
		Physics2D.IgnoreCollision (clonedesu.collider2D, player.collider2D);
		Destroy (clonedesu,animationDuration);
	}
		
	/* 	subclass should call this before calling execute,
	   	I think the player should be found in the constructor but
		just in case it isn't, find it using this method. */
	public void getPlayer() {
		if (player == null)
		{
			player = GameObject.FindGameObjectWithTag("Player");
		}
	}
	//checks the player info to see if they have enough mana to cast
	public bool hasEnoughMana()
	{
		if (PlayerInfo.getMana() >= manaCost)
		{
			return true;
		}
		return false;
	}

	//let all Spells use the same subtract mana method to improve code maintenance
	public void subMana()
	{
		PlayerInfo.changeMana(-manaCost);	
	}

	public string getName()
	{
		return spellName;
	}

	public string getDescription()
	{
		return description;
	}
	//most spells are going to need to know what direction to fire
	//each cast method should check to see if they player has enough mana to cast before they actually cast.
	public abstract void cast(Direction dir);

	//every charge is now going to have a charge version, so get hype
	//still need direction knowledge and still check to see if player has enough mana
	public abstract void castCharge(Direction dir);

	//deprecated, use Utilities.cloneObject instead
	/*public GameObject createSpellObject(Direction direction, GameObject bulletToClone, Vector3 placetoCreate, Vector3 velocity, Quaternion orientation)
	{
		GameObject clonedesu = (GameObject)Instantiate (bulletToClone, placetoCreate, orientation);
		clonedesu.rigidbody2D.velocity = velocity;
		Physics2D.IgnoreCollision (clonedesu.collider2D, player.collider2D);
		return clonedesu;
	}*/

	public int getCost (){
		return manaCost;
	}
	public void setCost(int newCost){
		manaCost = newCost;
	}
}