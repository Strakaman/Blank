using UnityEngine;
using System.Collections;

public class SpeedPickup : Pickupable {
	// Use this for initialization
	
	//when they player touches this object, destroy it and increase player's health
	public override void OnTriggerEnter2D(Collider2D whatICollidedWith)
	{
		if (Utilities.hasMatchingTag("Player",whatICollidedWith.gameObject))
		{
			PlayerInfo.SpeedUp();
			Utilities.TellPlayer("Temporary Speed Boost!");
			Utilities.playSound(mySound); //sound type inherited from interface
			Destroy(this.gameObject);
		}
	}
}
