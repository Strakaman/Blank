using UnityEngine;
using System.Collections;

public class OrbitTarget : MonoBehaviour {

	public GameObject target;
	
	// Update is called once per frame
	void Update () {
		if (target == null) {
			Destroy(gameObject);
		}
		gameObject.transform.position = target.transform.position;
	}
}
