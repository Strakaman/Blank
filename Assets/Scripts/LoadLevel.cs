using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {

	// Use this for initialization
	void OnCollisionEnter2D(Collision2D coll) {
		if (Utilities.hasMatchingTag("Player",coll.gameObject)) {
			Application.LoadLevel ("Omari Test Scene");
		}
	}
}
