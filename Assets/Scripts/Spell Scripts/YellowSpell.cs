using UnityEngine;
using System.Collections;


public class YellowSpell : Spell
{
	GameObject yellowRefObject; //used so that spell knows what object to clone when it casts
	
	//method created to set instance variables, can't use constructor because superclass is ScriptableObject
	public void initializeSpell(string nameDesu, string descriptionDesu, int manaCostDesu, int projSpeedDesu, float animDuration) 
	{
		base.initializeSpell(nameDesu, descriptionDesu, manaCostDesu,animDuration);
		yellowRefObject = GameObject.FindGameObjectWithTag("YellowSpellObject");
		projectileSpeed = projSpeedDesu;
	}
	
	//implement here whatever casting this spell is supposed to do
	public override void cast(Direction dir)
	{
		createProjectile (dir, yellowRefObject);
	}

	public override void castCharge(Direction dir)
	{
		
	}
}

