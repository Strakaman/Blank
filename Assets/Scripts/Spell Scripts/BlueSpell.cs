
using UnityEngine;
using System.Collections;

public class BlueSpell : Spell
{
	GameObject blueRefObject; //used so that spell knows what object to clone when it casts
	GameObject chargedblueRefObject; //used so that spell knows what object to clone when it casts

	//method created to set instance variables, can't use constructor because superclass is ScriptableObject
	public override void initializeSpell(string nameDesu, string descriptionDesu, int manaCostDesu, float animDuration, int thechargeManaCost, int thechargeTimeRequired) 
	{
		base.initializeSpell(nameDesu, descriptionDesu, manaCostDesu, animDuration, thechargeManaCost, thechargeTimeRequired);
		blueRefObject = GameObject.FindGameObjectWithTag("BlueSpellObject");
		chargedblueRefObject = GameObject.FindGameObjectWithTag("ChargedBlueSpellObject");
		projectileSpeed = 0;
	}

	//implement here whatever casting this spell is supposed to do
	public override void cast(Direction dir)
	{
		createProjectile (dir, blueRefObject);
	}

	public override void castCharge(Direction dir)
	{
		if (player == null){player = GameObject.FindGameObjectWithTag("Player");}
		Vector3 clonePosition = new Vector3(0,0,0) ;
		Vector3 cloneVelocity = new Vector3(0,0,0);
		Quaternion cloneOrientation = Quaternion.Euler(0,0,0); 
		if (dir == Direction.down) {
			clonePosition = player.transform.position + new Vector3(0,-2.5f,0);
		}
		else if (dir == Direction.up) {
			clonePosition = player.transform.position + new Vector3(0,2.5f,0);
		}
		else if (dir == Direction.left) {
			clonePosition = player.transform.position + new Vector3(-2.5f, 0, 0);
		}
		else if (dir == Direction.right) {
			clonePosition = player.transform.position + new Vector3(2.5f,0,0);
		}
		//GameObject clonedesu = createSpellObject(direction, bulletToClone, clonePosition, cloneVelocity, cloneOrientation);
		//Debug.Log(cloneVelocity);
		GameObject clonedesu = Utilities.cloneObject(dir, chargedblueRefObject, clonePosition, cloneVelocity, cloneOrientation);
		Physics2D.IgnoreCollision (clonedesu.collider2D, player.collider2D);
		Destroy (clonedesu,animationDuration*2);
	}
}
