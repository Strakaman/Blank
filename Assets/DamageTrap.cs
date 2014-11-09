using UnityEngine;
using System.Collections;

public class DamageTrap : Switchable {
	public int damage;
	public bool isOn;
	public Animator animator;

	public override void flipStatus()
	{
		isOn = !isOn;
		updateStatus();
	}

	/** 
	 * called by a pressure plate that can have multiple triggers
	 * If you want the door to be locked,   pass in false
	 * If you want the door to be unlocked, pass in true
	 */
	public override void setStatus(bool activeStatus)
	{
		isOn = !activeStatus; 
		updateStatus();
	}
	
	/**
	 * If the door is locked,  reenable it's collider and update the animation
	 * If the door is unlocked, disable it's collider and update the animation
	 */ 
	void updateStatus()
	{
		collider2D.enabled = isOn;
		gameObject.GetComponent<SpriteRenderer> ().enabled = isOn;
	}

	void OnCollisionStay2D(Collision2D coll) {
		if (Utilities.hasMatchingTag("Player",coll.gameObject)) {
			DamageStruct laserStruct = new DamageStruct (damage, collider2D.gameObject, 1000, 0.25f); 
			//struct used to pass more than one parameter through send message, which only lets you pass one object as a parameter
			coll.gameObject.SendMessage("callDamage", laserStruct);
		}
	}
}
