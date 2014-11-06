
using UnityEngine;
using System.Collections;

public class BlueSpell : Spell
{
	GameObject blueRefObject; //used so that spell knows what object to clone when it casts

	//method created to set instance variables, can't use constructor because superclass is ScriptableObject
	public override void initializeSpell(string nameDesu, string descriptionDesu, int manaCostDesu, float animDuration, int thechargeManaCost, int thechargeTimeRequired) 
	{
		base.initializeSpell(nameDesu, descriptionDesu, manaCostDesu, animDuration, thechargeManaCost, thechargeTimeRequired);
		blueRefObject = GameObject.FindGameObjectWithTag("BlueSpellObject");
		projectileSpeed = 0;
	}

	//implement here whatever casting this spell is supposed to do
	public override void cast(Direction dir)
	{
		createProjectile (dir, blueRefObject);
	}

	public override void castCharge(Direction dir)
	{
		createProjectile (dir, blueRefObject);
	}
}
