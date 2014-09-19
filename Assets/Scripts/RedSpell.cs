using UnityEngine;
using System.Collections;

public class RedSpell : Spell
{
	GameObject redRefObject;
	//stupid constructor synthax required to make subclass derive from base class properly
	public RedSpell(string nameDesu, string descriptionDesu, int manaCostDesu): 
		base(nameDesu, descriptionDesu, manaCostDesu) 
	{
		redRefObject = GameObject.FindGameObjectWithTag("RedSpellObject");
	}

	public override void execute(Direction dir)
	{
		/*
		 * if manaCost < currentMana 
		 *  subtract mana
		 *  create projectile
		 * else
		 *  display on GUI (not enough mana)
		 */
		createProjectile (redRefObject);
	}

}