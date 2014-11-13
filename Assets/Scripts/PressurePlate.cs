using UnityEngine;
using System.Collections;

public class PressurePlate : MonoBehaviour
{
	private bool isActive = false;
	public Animator animator;
	public GameObject lastEntered; //used to check if weighted object was destroyed
	// Use this for initialization
	void Start ()
	{
			animator = (Animator)GetComponent ("Animator");
	}

	void Update()
	{
		if ((isActive) && (lastEntered == null)) 
		{ //weighted object that was on pressure plate was destroyed, which doesn't trigger on exit, 
			//so let's pseudo trigger it ourselves
			isActive = false;
			TriggerChildren(false); 
		}

	}

	void updateAnimation ()
	{
			animator.SetBool ("isActive", isActive);
	}

	void OnTriggerStay2D (Collider2D trigInfo)
	{
		if (Utilities.hasMatchingTag("Weighted",trigInfo.gameObject)) {
			isActive = true; //weighted object has entered so update status and childre
			TriggerChildren (true);
			lastEntered = trigInfo.gameObject; 
			//store the last entered object because if is destroyed, onTriggerExit
			//will never be fired, so we need a pseudo onTriggerExit
			}
	}

	void OnTriggerExit2D (Collider2D trigInfo)
	{
		if (Utilities.hasMatchingTag("Weighted",trigInfo.gameObject)) {
			isActive = false;	//weighted object left so update status and children
			TriggerChildren (false);
			}
	}

	void TriggerChildren (bool trigOnTruetrigOffFalse)
	{

			animator.SetBool ("isActive", trigOnTruetrigOffFalse);
			Utilities.setStatusInChildren(transform, trigOnTruetrigOffFalse); 
			//will trigger all switchable objects in children
	}
}
