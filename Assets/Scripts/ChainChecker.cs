using UnityEngine;
using System.Collections;

public class ChainChecker : MonoBehaviour {

	private bool checkEnemyInRadius;
	public GameObject parent;
	// Use this for initialization
	void Start () {
		checkEnemyInRadius = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*void OnTriggerStay2D(Collider2D collInfo) {
		//Debug.Log (Utilities.hasMatchingTag("Enemy",collInfo.gameObject));
		if (checkEnemyInRadius == true && Utilities.hasMatchingTag("Enemy", collInfo.gameObject) && collInfo != colTarget) {
			//Debug.Log ("CHAIN!");
			
			//gameObject.collider2D.enabled = true;
			target = collInfo.gameObject;
			projectileTrajectory(target);
			Destroy (gameObject, .5f);
		}
	}*/
}
