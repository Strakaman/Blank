using UnityEngine;
using System.Collections;

public class PatrolScript : MonoBehaviour {
	public Direction direction;
	public float startTimer = 0;
	public float maxTimer = 100;
	public float speed = 3;

    private Rigidbody2D m_rigidbody2D;

    private void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }
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


	}

    private void FixedUpdate()
    {
        //Up and Down
        if (direction == Direction.up)
        {
            m_rigidbody2D.velocity = new Vector2(0, speed);
        }
        else if (direction == Direction.down)
        {
            m_rigidbody2D.velocity = new Vector2(0, -speed);
        }

        //Left and Right
        if (direction == Direction.right)
        {
            m_rigidbody2D.velocity = new Vector2(speed, 0);
        }
        else if (direction == Direction.left)
        {
            m_rigidbody2D.velocity = new Vector2(-speed, 0);
        }
    }
}
