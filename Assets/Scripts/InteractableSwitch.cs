//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System.Collections;
using UnityEngine;

public class InteractableSwitch : Interactable
{

	public bool isActive;
	public Animator animator;
	
	public void Start ()
	{
			animator = (Animator)GetComponent ("Animator");
		animator.SetBool ("isActive", isActive);
	}

	public override void interact ()
	{
		isActive = !isActive;
		//Debug.Log ("is active hmm: " + isActive);
		animator.SetBool ("isActive", isActive);
		//for every child object associated with this switch, change it's status too	
		Utilities.flipStatusInChildren(transform);
	}

}

