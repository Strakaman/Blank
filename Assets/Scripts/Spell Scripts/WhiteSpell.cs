
using UnityEngine;
using System.Collections;

public class WhiteSpell : Spell
{
	GameObject whiteRefObject; //used so that spell knows what object to clone when it casts

	//method created to set instance variables, can't use constructor because superclass is ScriptableObject
	public override void initializeSpell(string nameDesu, string descriptionDesu, int manaCostDesu, float animDuration) 
	{
		base.initializeSpell(nameDesu, descriptionDesu, manaCostDesu, animDuration);
		whiteRefObject = GameObject.FindGameObjectWithTag("WhiteSpellObject");
		projectileSpeed = 0;
	}

	//implement here whatever casting this spell is supposed to do
	public override void cast(Direction dir)
	{
		if (player == null){player = GameObject.FindGameObjectWithTag("Player");}
		Vector3 clonePosition = new Vector3(0,0,0) ;
		Vector3 cloneVelocity = new Vector3(0,0,0);
		Quaternion cloneOrientation = Quaternion.Euler(0,0,0); 
	 	if (dir == Direction.left) {
			clonePosition = player.transform.position + new Vector3(+1, 0, 0);
		}
		else{
			clonePosition = player.transform.position + new Vector3(-1, 0, 0);
		}
		GameObject clonedesu = Utilities.cloneObject(dir, whiteRefObject, clonePosition, cloneVelocity, cloneOrientation);
		clonedesu.SendMessage("BeastMode");
		Destroy (clonedesu,animationDuration);
	}
}
