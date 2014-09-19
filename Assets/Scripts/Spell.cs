using UnityEngine;
using System.Collections;

public abstract class Spell
{
	string name;
	string description;
	GameObject player; //get transform for casting purposes
	int manaCost;
	private bool isUnlocked = false;

	public Spell(string n, string d, int m)
	{
		name = n;
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

	public void createProjectile(GameObject bulletToClone)
	{


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
	public abstract void execute(Direction dir);
}