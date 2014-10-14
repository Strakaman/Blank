using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {
	public string Level;
	// Use this for initialization
	void OnCollisionEnter2D(Collision2D coll) {
		if (Utilities.hasMatchingTag("Player",coll.gameObject)) {
			Application.LoadLevel (Level);
		}
	}
}
