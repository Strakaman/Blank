using UnityEngine;
using System.Collections;

public class ManaPickup : Pickupable {
	public int increaseManaAmount;

	//when they player touches this object, destroy it and increase player's mana
	public override void OnTriggerEnter2D(Collider2D whatICollidedWith)
	{
		if (Utilities.hasMatchingTag("Player",whatICollidedWith.gameObject))
		{
			//Debug.Log ("Player mana increased by: " + increaseManaAmount + " to: " + PlayerInfo.getMana());
			PlayerInfo.changeMana(increaseManaAmount);
			Utilities.TellPlayer("MP Restored!");
			Destroy(this.gameObject);
		}
	}
}
