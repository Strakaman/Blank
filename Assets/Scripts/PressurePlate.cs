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
		//if ((trigInfo.CompareTag ("Player")) || (trigInfo.CompareTag ("Ice Block")) ||(trigInfo.CompareTag ("Regular Block"))) {
					//Debug.Log("trigger on due to: " + trigInfo.tag);
					TriggerChildren (true);
			}
	}

	void OnTriggerExit2D (Collider2D trigInfo)
	{
		if (Utilities.hasMatchingTag("Weighted",trigInfo.gameObject)) {
		//if ((trigInfo.CompareTag ("Player")) || (trigInfo.CompareTag ("Ice Block")) ||(trigInfo.CompareTag ("Regular Block"))) {
					//Debug.Log("trigger off due to: " + trigInfo.tag);
					TriggerChildren (false);
			}
	}

	void TriggerChildren (bool trigOnTruetrigOffFalse)
	{
			animator.SetBool ("isActive", trigOnTruetrigOffFalse);
			Utilities.setStatusInChildren(transform, trigOnTruetrigOffFalse);
	}
}
