using UnityEngine;
using System.Collections;

public class RedSpell : Spell
{
	GameObject redRefObject;
	GameObject chargedRedRefObject;
	//stupid constructor synthax required to make subclass derive from base class properly

	public void initializeSpell(string nameDesu, string descriptionDesu, int manaCostDesu, int projSpeedDesu, float animDuration, int thechargeManaCost, int thechargeTimeRequired) 
	{
		base.initializeSpell(nameDesu, descriptionDesu, manaCostDesu, animDuration, thechargeManaCost, thechargeTimeRequired);
		redRefObject = GameObject.FindGameObjectWithTag("RedSpellObject");
		chargedRedRefObject = GameObject.FindGameObjectWithTag("ChargedRedSpellObject");
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

	public override void castCharge(Direction dir)
	{
		createProjectile (dir, chargedRedRefObject);
	}
}