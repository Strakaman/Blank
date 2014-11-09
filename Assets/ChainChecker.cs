using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChainChecker : MonoBehaviour {

	private bool checkEnemyInRadius;
	public GameObject parent;
	private List<GameObject> alreadyCollided = new List<GameObject> ();
	// Use this for initialization
	void Start () {
		checkEnemyInRadius = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void BeastMode(GameObject colTarget)
	{
			checkEnemyInRadius = true;
			alreadyCollided.Add(colTarget);
	}

	void OnTriggerStay2D(Collider2D collInfo) {
		//Debug.Log (Utilities.hasMatchingTag("Enemy",collInfo.gameObject));
		if (checkEnemyInRadius == true && Utilities.hasMatchingTag("Enemy", collInfo.gameObject) && (!alreadyCollided.Contains(collInfo.gameObject))) {		                                                                                                                                  ))) {
			//Debug.Log ("CHAIN!");
			
			//gameObject.collider2D.enabled = true;
			target = collInfo.gameObject;
			projectileTrajectory(target);
			Destroy (gameObject, .5f);
			}
		}
	}
}
