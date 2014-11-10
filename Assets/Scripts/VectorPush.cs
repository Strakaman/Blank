using UnityEngine;
using System.Collections;

public class VectorPush : MonoBehaviour {
	public float pushDistance; //Distance of how far you want blcok push you
	public Direction direction = Direction.right; // Default direction
	private GameObject collInfo; //Player collider
	private Vector3 pushDirection; //Direction of force push

	void Start() {
		Utilities.rotateObject (direction, gameObject);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		//Check if colliding object is Player
		if (Utilities.hasMatchingTag("Player", coll.gameObject)) {
			coll.gameObject.transform.position = transform.position;
			collInfo = coll.gameObject;
			//Set push direction relative to block direction. 
			if (direction == Direction.up) {
				pushDirection = new Vector3(0, 1, 0);
			} else if (direction == Direction.down) {
				pushDirection = new Vector3(0, -1, 0);
			} else if (direction == Direction.right) {
				pushDirection = new Vector3(1, 0, 0);
			} else if (direction == Direction.left) {
				pushDirection = new Vector3(-1, 0, 0);
			}
			//Smoothly translate the collider object 
			for (int i = 0; i < 10; i++) {
				Invoke("push", i * 0.01f);
			}
		}
	}

	//Method to invoke in order to translate the object to desired position
	void push() {
		collInfo.gameObject.transform.position = Vector3.Lerp(collInfo.gameObject.transform.position,transform.position + (pushDirection * pushDistance), 0.1f	); 
	}
}
