﻿using UnityEngine;
using System.Collections;

public class ChargeStation : MonoBehaviour {
	private bool atFullCharge = false;
	private const float chargeNeededforFull=250;
	private float chargeLevel;
	public Animator animator;
	// Use this for initialization
	void Start () {
		chargeLevel = 0;
		animator = (Animator)GetComponent ("Animator");
	}
	
	// Update is called once per frame
	void Update () {
		if(!atFullCharge)
		{
			chargeLevel -= 1;
			if (chargeLevel < 0)
			{
				chargeLevel = 0;
			}
		}
		updateAnimation();
	}
	
	void OnCollisionEnter2D(Collision2D collInfo)
	{
		if (Utilities.hasMatchingTag("YellowSpellObject",collInfo.gameObject))
		{
			if (!atFullCharge)
			{
				chargeLevel += 30;
				if(chargeLevel >= chargeNeededforFull)
				{
					atFullCharge = true;
					Utilities.flipStatusInChildren(transform);
				}
			}
		}
	}

	void updateAnimation()
	{
		animator.SetFloat("chargePercentage",(chargeLevel/chargeNeededforFull)*100);
	}
}
