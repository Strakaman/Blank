using UnityEngine;
using System.Collections;

public class CameraPan : MonoBehaviour {
	public GameObject player;
	private int numInvoke = 0;
	private int width;
	private int height;
	//private int screenWidth = Screen.width/10;
	//private int screenHeight = Screen.height/10;

	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	// Update is called once per frame
	void FixedUpdate () {
		//cameraPan ();
		//followPlayer();
		followPlayer2();
	}

	void followPlayer() {
		Vector3 playerCam = new Vector3 (player.transform.position.x, player.transform.position.y, -10);
		transform.position = playerCam;
	}

	void followPlayer2() {
		if (player)
		{
			Vector3 point = camera.WorldToViewportPoint(player.transform.position);
			Vector3 delta = player.transform.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
	}

	void cameraPan() {
		Vector3 pos = Camera.main.WorldToViewportPoint(player.transform.position);
		
		if (pos.x < 0.0) {
			Debug.Log ("Screen width: " + Screen.width);
			width = -1;
			height = 0;
			//numInvoke = 3;
			InvokeRepeating("pan", 0.20f, 0.20F);
			numInvoke = 0;
		}
		if (1.0 < pos.x) {
			Debug.Log ("Screen width: " + Screen.width);
			width = 1;
			height = 0;
			//numInvoke = 3;
			InvokeRepeating("pan", 0.20f, 0.20F);
			numInvoke = 0;
		}
		if (pos.y < -0.2) {
			Debug.Log ("Screen height: " + Screen.height);
			width = 0;
			height = -1;
			InvokeRepeating("pan", 0.20f, 0.20F);
			numInvoke = 0;
		}
		if (1.2 < pos.y) {
			Debug.Log ("Screen height: " + Screen.height);
			width = 0;
			height = 1;
			InvokeRepeating("pan", 0.20f, 0.20F);
			numInvoke = 0;	
		}
	}

	void pan() {
		if (numInvoke < 9 && height != 0) {
			numInvoke ++;
				Vector3 pan = new Vector3 (transform.position.x + width, transform.position.y + height, -10);
				transform.position = Vector3.MoveTowards (transform.position, pan, 1);
			//transform.position = 
		}
		if (numInvoke < 16 && width != 0) {
			numInvoke ++;
			Vector3 pan = new Vector3 (transform.position.x + width, transform.position.y + height, -10);
			transform.position = Vector3.MoveTowards (transform.position, pan, 1);

		}
	}
}
