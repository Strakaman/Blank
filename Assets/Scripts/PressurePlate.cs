using UnityEngine;
using System.Collections;

public class PressurePlate : MonoBehaviour
{
	public bool isActive = false;
	public Animator animator;
	// Use this for initialization
	void Start ()
	{
			animator = (Animator)GetComponent ("Animator");
	}

	void updateAnimation ()
	{
			animator.SetBool ("isActive", isActive);
	}

	void OnTriggerStay2D (Collider2D trigInfo)
	{
		if (Utilities.hasMatchingTag("Weighted",trigInfo.gameObject)) {
			isActive = true;
			TriggerChildren (true);
			}
	}

	void OnTriggerExit2D (Collider2D trigInfo)
	{
		if (Utilities.hasMatchingTag("Weighted",trigInfo.gameObject)) {
			isActive = false;	
			TriggerChildren (false);
			}
	}

	void TriggerChildren (bool trigOnTruetrigOffFalse)
	{

			animator.SetBool ("isActive", trigOnTruetrigOffFalse);
			Utilities.setStatusInChildren(transform, trigOnTruetrigOffFalse);
	}
}
