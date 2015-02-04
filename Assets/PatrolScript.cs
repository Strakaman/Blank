using UnityEngine;
using System.Collections;

public class PatrolScript : MonoBehaviour {
	public Direction direction;
	public float startTimer = 0;
	public float maxTimer = 100;
	public float speed = 3;
	
	// Update is called once per frame
	void Update () {

		startTimer += Time.deltaTime;
		if (startTimer >= maxTimer) {
			startTimer = 0;
			if (direction == Direction.up) {
				direction = Direction.down;
				return;
			} else if (direction == Direction.down){
				direction = Direction.up;
				return;
			}
			if (direction == Direction.right) {
				direction = Direction.left;
				return;
			} else if (direction == Direction.left){
				direction = Direction.right;
				return;
			}
		}

		//Up and Down
		if (direction == Direction.up) {
			rigidbody2D.velocity = new Vector2(0, speed);
		}
		if (direction == Direction.down) {
			rigidbody2D.velocity = new Vector2(0, -speed);
		}

		//Left and Right
		if (direction == Direction.right) {
			rigidbody2D.velocity = new Vector2(speed, 0);
		}
		if (direction == Direction.left) {
			rigidbody2D.velocity = new Vector2(-speed, 0);
		}
	}
}
