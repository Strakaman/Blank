
using UnityEngine;
using System.Collections;

public class BlueSpell : Spell
{
	GameObject blueRefObject;
	//stupid constructor synthax required to make subclass derive from base class properly
	
	public void initializeSpell(string nameDesu, string descriptionDesu, int manaCostDesu, int projSpeedDesu) 
	{
		base.initializeSpell(nameDesu, descriptionDesu, manaCostDesu);
		blueRefObject = GameObject.FindGameObjectWithTag("BlueSpellObject");
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
		createProjectile (dir, blueRefObject);
	}
}
