﻿using UnityEngine;
using System.Collections;

public class UnlockableDoor : Switchable {

	public bool isLocked;
	public Animator animator;
	// Use this for initialization
	void Start () {
		animator = (Animator)GetComponent ("Animator");
		animator.SetBool ("isLocked", isLocked);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	/*called by interactable switch to change the status of the door */
	public override void flipStatus()
	{
		isLocked = !isLocked;
		updateStatus();
	}

	/** 
	 * called by a pressure plate that can have multiple triggers
	 * If you want the door to be locked,   pass in false
	 * If you want the door to be unlocked, pass in true
	 */
	public override void setStatus(bool activeStatus)
	{
		isLocked = !activeStatus; 
		updateStatus();
	}

	/**
	 * If the door is locked,  reenable it's collider and update the animation
	 * If the door is unlocked, disable it's collider and update the animation
	 */ 
	void updateStatus()
	{
		animator.SetBool ("isLocked", isLocked);
		collider2D.enabled = isLocked;
	}

}
