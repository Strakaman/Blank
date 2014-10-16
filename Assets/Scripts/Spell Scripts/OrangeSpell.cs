using UnityEngine;
using System.Collections;

public class OrangeSpell : Spell
{
	GameObject redRefObject;
	//stupid constructor synthax required to make subclass derive from base class properly
	
	public void initializeSpell(string nameDesu, string descriptionDesu, int manaCostDesu, int projSpeedDesu, float animDuration) 
	{
		base.initializeSpell(nameDesu, descriptionDesu, manaCostDesu, animDuration);
		redRefObject = GameObject.FindGameObjectWithTag("OrangeSpellObject");
		projectileSpeed = projSpeedDesu;
	}
	
	public override void cast(Direction dir)
	{
		/*
		 * if manaCost < currentMana 
		 *  subtract mana
		 *  create projectile
		 * else
		 *  display on GUI (not enough mana)
		 */
		createProjectile (dir, redRefObject);
	}
	
}