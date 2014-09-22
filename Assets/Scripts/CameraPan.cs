using UnityEngine;
using System.Collections;

public class CameraPan : MonoBehaviour {
	public GameObject player;
	public int numInvoke = 0;
	public int i;
	public int j;

	// Update is called once per frame
	void FixedUpdate () {
		//cameraPan ();
		followPlayer();
	}

	void followPlayer() {
		Vector3 playerCam = new Vector3 (player.transform.position.x, player.transform.position.y, -10);
		transform.position = playerCam;
	}

	void cameraPan() {
		Vector3 pos = Camera.main.WorldToViewportPoint(player.transform.position);
		
		if (pos.x < 0.0) {
			i = -10;
			j = 0;
			numInvoke = 3;
			InvokeRepeating("pan", 0.25f, 0.25F);
			numInvoke = 0;
		}
		if (1.0 < pos.x) {
			i = 10;
			j = 0;
			numInvoke = 3;
			InvokeRepeating("pan", 0.25f, 0.25F);
			numInvoke = 0;
		}
		if (pos.y < -0.2) {
			i = 0;
			j = -10;
			InvokeRepeating("pan", 0.25f, 0.25F);
			numInvoke = 0;
		}
		if (1.2 < pos.y) {
			i = 0;
			j = +10;
			InvokeRepeating("pan", 0.25f, 0.25F);
			numInvoke = 0;	
		}
	}

	void pan() {
		if (numInvoke < 20) {
				numInvoke++;
				Vector3 pan = new Vector3 (transform.position.x + i, transform.position.y + j, -10);
				transform.position = Vector3.MoveTowards (transform.position, pan, .5f);
		}
	}
}
