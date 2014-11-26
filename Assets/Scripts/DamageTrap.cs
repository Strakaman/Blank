using UnityEngine;
using System.Collections;

public class DamageTrap : Switchable {
	public int damage = 10;
	public int knockback = 1000;
	public float hitDelay = 0.25f;
	public bool isOn;
	public Animator animator;
	public bool canStun = false;
	public bool canSlow = false;
	public float slowDur = 5;
	public float stunDur = 1;

	void Start() {
		updateStatus();
	}

	void Update() {
		PlayerInfo.setStunDur(stunDur);
		PlayerInfo.setSlowDur(slowDur);
	}

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
		gameObject.GetComponent<SpriteRenderer> ().enabled = isOn;
		if (collider2D) {
			collider2D.enabled = isOn;
		}
	}

	void OnCollisionStay2D(Collision2D coll) {
		if (Utilities.hasMatchingTag("Player",coll.gameObject)) {
			DamageStruct laserStruct = new DamageStruct (damage, collider2D.gameObject, knockback, hitDelay); 
			//struct used to pass more than one parameter through send message, which only lets you pass one object as a parameter
			coll.gameObject.SendMessage("callDamage", laserStruct);
			if (canStun) {

				PlayerInfo.setStun(true);
			}
			if (canSlow) {

				PlayerInfo.setSlow(true);
			}
		}
	}

	void OnTriggerStay2D(Collider2D coll) {
		if (Utilities.hasMatchingTag("Player",coll.gameObject)) {
			DamageStruct laserStruct = new DamageStruct (damage, collider2D.gameObject, knockback, hitDelay); 
			//struct used to pass more than one parameter through send message, which only lets you pass one object as a parameter
			coll.gameObject.SendMessage("callDamage", laserStruct);
			if (canStun) {
				//PlayerInfo.setStunDur(stunDur);
				PlayerInfo.setStun(true);
			}
			if (canSlow) {
				//PlayerInfo.setSlowDur(slowDur);
				PlayerInfo.setSlow(true);
			}
		}
	}

}
