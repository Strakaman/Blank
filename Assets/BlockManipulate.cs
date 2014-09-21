using UnityEngine;
using System.Collections;

public class BlockManipulate : MonoBehaviour {


	// Update is called once per frame
	void OnCollisionEnter2D (Collision2D coll) {
				while (coll.gameObject.tag == "Player") {
				Debug.Log("Colliding");
						if (Input.GetButtonDown("Grab")) {
						Debug.Log("Grabbing");
								transform.parent = coll.gameObject.transform;
						}
				}
		}
}
