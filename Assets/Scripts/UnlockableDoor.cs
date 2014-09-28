using UnityEngine;
using System.Collections;

public class UnlockableDoor : MonoBehaviour {

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
	void flipStatus()
	{
		isLocked = !isLocked;
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
