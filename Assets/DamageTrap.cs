using UnityEngine;
using System.Collections;

public class DamageTrap : MonoBehaviour {
	public int damage;

	void OnCollisionStay2D(Collision2D coll) {
		if (Utilities.hasMatchingTag("Player",coll.gameObject)) {
			DamageStruct laserStruct = new DamageStruct (damage, collider2D.gameObject, 1000, 0.25f); 
			//struct used to pass more than one parameter through send message, which only lets you pass one object as a parameter
			coll.gameObject.SendMessage("callDamage", laserStruct);
		}
	}
}
