using UnityEngine;
using System.Collections;

public abstract class Spell: ScriptableObject
{
	protected string spellName;
	protected string description;
	protected GameObject player; //get transform for casting purposes
	protected int manaCost;
	protected bool isUnlocked = false;
	protected int projectileSpeed;

	public virtual void initializeSpell(string n, string d, int m)
	{
		spellName = n;
		description = d;
		manaCost = m;
		player = GameObject.FindGameObjectWithTag("Player");
	}

	public void unlockSpell()
	{
		isUnlocked = true;
	}

	public bool isSpellUnlocked()
	{
		return isUnlocked;
	}

	public void createProjectile(Direction direction ,GameObject bulletToClone)
	{
		GameObject clonedesu = (GameObject)Instantiate (bulletToClone, player.transform.position, player.transform.rotation);
		Physics2D.IgnoreCollision (clonedesu.collider2D, player.collider2D);
		if (direction == Direction.down) {
			clonedesu.rigidbody2D.velocity = new Vector3 (0, -projectileSpeed, 0);
		}
		else if (direction == Direction.up) {
			clonedesu.rigidbody2D.velocity = new Vector3 (0, projectileSpeed, 0);
		}
		else if (direction == Direction.left) {
			clonedesu.rigidbody2D.velocity = new Vector3 (-projectileSpeed, 0, 0);
		}
		else if (direction == Direction.right) {
			clonedesu.rigidbody2D.velocity = new Vector3 (projectileSpeed, 0, 0);
		}
		Destroy (clonedesu, 2);

	}
	///subclass should call this before calling execute,
	/// I think the player should be found in the constructor but
	/// just in case it isn't find it here.
	public void getPlayer() {
		if (player == null)
		{
			player = GameObject.FindGameObjectWithTag("Player");
		}
	}

	//most spells are going to need to know what direction to fire 
	public abstract void cast(Direction dir);
}