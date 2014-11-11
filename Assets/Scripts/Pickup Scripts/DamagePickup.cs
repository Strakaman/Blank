using UnityEngine;
using System.Collections;

public class DamagePickup : Pickupable {
	// Use this for initialization
	
	//when they player touches this object, destroy it and increase player's health
	public override void OnTriggerEnter2D(Collider2D whatICollidedWith)
	{
		if (Utilities.hasMatchingTag("Player",whatICollidedWith.gameObject))
		{
			//Debug.Log ("Player health increased by: " + increaseHealth + " to: " + PlayerInfo.getHealth());
			PlayerInfo.PowerUp();
			Utilities.TellPlayer("Temporary Power Up! Spells do 2x damage!");
			Destroy(this.gameObject);
		}
	}
}
