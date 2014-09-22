
using UnityEngine;
using System.Collections;

public class BlueSpell : Spell
{
	GameObject blueRefObject; //used so that spell knows what object to clone when it casts

	//method created to set instance variables, can't use constructor because superclass is ScriptableObject
	public void initializeSpell(string nameDesu, string descriptionDesu, int manaCostDesu, int projSpeedDesu) 
	{
		base.initializeSpell(nameDesu, descriptionDesu, manaCostDesu);
		blueRefObject = GameObject.FindGameObjectWithTag("BlueSpellObject");
		projectileSpeed = projSpeedDesu;
	}

	//implement here whatever casting this spell is supposed to do
	public override void cast(Direction dir)
	{
		createProjectile (dir, blueRefObject);
	}
}
