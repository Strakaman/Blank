using UnityEngine;
using System.Collections;

public abstract class Spell: ScriptableObject
{
	protected string spellName; //for display purposes
	protected string description; //for display purposes
	protected GameObject player; //get transform for casting purposes
	protected int manaCost; //can't cast if you don't have enough mana
	protected bool isUnlocked = false; //spell is locked by default
	protected int projectileSpeed; //for spell that shoot something out

	//use as fake constructor
	public virtual void initializeSpell(string n, string d, int m)
	{
		spellName = n;
		description = d;
		manaCost = m;
		player = GameObject.FindGameObjectWithTag("Player");
	}

	//unlocks spell so it can be reached when cycling through spells
	public void unlockSpell()
	{
		isUnlocked = true;
	}

	//tells if spell is unlocked, used to determine when cycling through spells whether or not to skip past this one.
	public bool isSpellUnlocked()
	{
		return isUnlocked;
	}

	//for spells that shoot something. uses player direction to figure out which way to fire.
	public void createProjectile(Direction direction, GameObject bulletToClone)
	{
		GameObject clonedesu = createSpellObject(direction, bulletToClone);
		if (direction == Direction.down) {
			clonedesu.transform.rotation = Quaternion.Euler(0, 0, 270);
			clonedesu.rigidbody2D.velocity = new Vector3 (0, -projectileSpeed, 0);
		}
		else if (direction == Direction.up) {
			clonedesu.transform.rotation = Quaternion.Euler(0, 0, 90);
			clonedesu.rigidbody2D.velocity = new Vector3 (0, projectileSpeed, 0);
		}
		else if (direction == Direction.left) {
			clonedesu.transform.rotation = Quaternion.Euler(0, 0, 180);
			clonedesu.rigidbody2D.velocity = new Vector3 (-projectileSpeed, 0, 0);
		}
		else if (direction == Direction.right) {
			clonedesu.transform.rotation = Quaternion.Euler(0, 0, 0);
			clonedesu.rigidbody2D.velocity = new Vector3 (projectileSpeed, 0, 0);
		}
		Destroy (clonedesu, 2);
	}

	public GameObject createSpellObject(Direction direction, GameObject bulletToClone)
	{
		GameObject clonedesu = (GameObject)Instantiate (bulletToClone, player.transform.position, player.transform.rotation);
		Physics2D.IgnoreCollision (clonedesu.collider2D, player.collider2D);
		return clonedesu;
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
		PlayerInfo.changeMana(manaCost*-1);	
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
}