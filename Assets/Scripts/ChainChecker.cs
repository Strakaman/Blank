using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChainChecker : MonoBehaviour {

	private bool checkInRadius; //activates chain effect if a target has been hit
	public GameObject parent; //used to tell parent to calculate new trajectory if a chain has been found
	private List<GameObject> alreadyCollided = new List<GameObject> (); //keep track of every target that projectile has collided with

	// Use this for initialization
	void Start () {
		checkInRadius = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void BeastMode(GameObject colTarget)
	{
		//collider2D.enabled = true; keep around just in case
			checkInRadius = true; //now we can check for a potential chain
			alreadyCollided.Add(colTarget); //update list 
	}

	void OnTriggerStay2D(Collider2D collInfo) {
		GameObject ptnlTarget = collInfo.gameObject; //since we reference it so much, might be better to store in temp variable
		if ((checkInRadius == true) && (Utilities.hasMatchingTag("Enemy", ptnlTarget)) && (!alreadyCollided.Contains(ptnlTarget))) {
			//Debug.Log ("CHAIN!");		
			gameObject.GetComponent<Collider2D>().enabled = true;
			//collider2D.enabled = false; 
			//initially used to turn collider off so that when it comes back on,
			//it will revaluate everything in radius, but now it seems like it's not needed, for now
			parent.SendMessage("projectileTrajectory",ptnlTarget); //tell parent to calculate new trajectory
	}
}
}
