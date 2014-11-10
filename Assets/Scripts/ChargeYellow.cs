using UnityEngine;
using System.Collections;

public class ChargeYellow : MonoBehaviour {

	public int damage; //should be a low number since damage is applied during onTriggerStay
	public string whoToStrike ;//Player or Enemy
	// Use this for initialization
	void Start () {
		if (whoToStrike == null) {
			whoToStrike = "Enemy";
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D (Collider2D collInfo)
	{//if meant to hit player, will hit player, if meant to hit enemy, will hit enemy
		if (Utilities.hasMatchingTag (whoToStrike, collInfo.gameObject)) { 
				DamageStruct thisisntastructanymore = new DamageStruct (damage, collider2D.gameObject, 0, 0.2f); 
				//struct used to pass more than one parameter through send message, which only lets you pass one object as a parameter
				collInfo.gameObject.SendMessage("callDamage",thisisntastructanymore);
				collInfo.gameObject.SendMessage("StunYourself"); 
				//have to manually stun target because this is a trigger and won't cause the OnCollision
				// in the enemy class to occur. IF you try to also move the stun to to the enemy OnTrigger,
				//a lot of bad crap will happen so please don't do that
			}	
	}
}
