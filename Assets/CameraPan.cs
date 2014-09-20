using UnityEngine;
using System.Collections;

public class CameraPan : MonoBehaviour {
	public GameObject player;
	public int numInvoke = 0;
	public int i;
	public int j;

	// Update is called once per frame


	void FixedUpdate () {
		Vector3 trim = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z);
		Vector3 pos = Camera.main.WorldToViewportPoint(trim);
		
		if (pos.x < 0.0) {
			//transform.position = Vector3.Lerp(transform.position, pan, Time.smoothDeltaTimes);
			//transform.position = Vector3.MoveTowards(transform.position, pan, 1);
			i = -10;
			j = 0;
			InvokeRepeating("pan", 0.25f, 0.25F);
			numInvoke = 0;
		}
		if (1.0 < pos.x) {
			i = 10;
			j = 0;
			InvokeRepeating("pan", 0.25f, 0.25F);
			numInvoke = 0;
		}
		if (pos.y < 0.0) {
			i = 0;
			j = -10;
			InvokeRepeating("pan", 0.25f, 0.25F);
			numInvoke = 0;
		}
		if (1.0 < pos.y) {
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
