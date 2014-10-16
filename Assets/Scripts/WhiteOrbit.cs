using UnityEngine;
using System.Collections;

public class WhiteOrbit : MonoBehaviour {
	public Transform target;
	private int degrees = 5;
	//private float radius = .5f;
	//public bool orbitDir;
	public int startPosition;

	void Start() {
		/*if (startPosition == 0) {
			Vector3 orbit = new Vector3 (target.position.x + radius, target.position.y + radius, target.position.z);
			this.transform.position = orbit;
		} else if (startPosition == 1) {
			Vector3 orbit = new Vector3 (target.position.x + radius, target.position.y + -radius, target.position.z);
			this.transform.position = orbit;
		} else if (startPosition == 2) {
			Vector3 orbit = new Vector3 (target.position.x + -radius, target.position.y + radius, target.position.z);
			this.transform.position = orbit;
		} else if (startPosition == 3) {
			Vector3 orbit = new Vector3 (target.position.x + -radius, target.position.y + -radius, target.position.z);
			this.transform.position = orbit;
		}*/
	}

	// Update is called once per frame
	void Update () {
		//this.transform.position = target.transform.position * 2;
		if (startPosition == 1) {
			transform.RotateAround (target.transform.position, Vector3.down, degrees+1);
		} else if (startPosition == 2) {
			transform.RotateAround (target.transform.position, Vector3.right, degrees-1);
		}	else if (startPosition == 3)  {
			transform.RotateAround (target.transform.position, new Vector3(1,1,0), degrees);
		} else{
			transform.RotateAround (target.transform.position, new Vector3(-1,1,0), degrees);
		}
	}

}
