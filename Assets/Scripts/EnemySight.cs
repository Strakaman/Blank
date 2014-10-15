using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {
	//public bool playerInSight;                      // Whether or not the player is currently sighted.
	private GameObject player;                      // Reference to the player.
	private CircleCollider2D col;                     // Reference to the sphere collider trigger component.
	private Direction direction;
	private Vector3 s; //box collider size to help with raycasting
	private Vector3 c; //box collider center to help with raycasting
	private LayerMask pMask = 1 << 9;
	public float chaseTime = 2;
	private GameObject enemy;
	//private bool outOfRange;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		transform.position = gameObject.GetComponentInParent <EnemyAI>().transform.position;
		BoxCollider2D zollider = GetComponentInParent<BoxCollider2D> (); //get attached collider, store size and center
		s = zollider.size;
		c = zollider.center;
		col = GetComponent<CircleCollider2D>();
		//outOfRange = false;
	}
	
	// Update is called once per frame
	void Update () {
		direction = (Direction)gameObject.GetComponentInParent<Enemy>().getDirection();
		/*if (outOfRange = true) {
			Invoke("invokeSetFalse", chaseTime);
			outOfRange = false;
		}*/
	}
	void OnTriggerStay2D (Collider2D other)
	{
		//Debug.Log ("TWO COLLIDERS!!!");
		// If the player has entered the trigger sphere...
		if(other.gameObject == player)
		{
			//Debug.Log ("Player");
			
			Vector2 p = gameObject.GetComponentInParent <EnemyAI>().transform.position; //get current enemy position to cast ray from
			Vector3 castDirection; //set the raycast direction to vertical or horizontal based on direction player is facing
			int xAxisDir = 0;
			int yAxisDir = 0;
			if (direction == Direction.up) {
				yAxisDir = 1;		
			} else if (direction == Direction.down) {
				yAxisDir = -1;
			} else if (direction == Direction.right) {
				xAxisDir = 1;
			} else if (direction == Direction.left) {
				xAxisDir = -1;
			}
			castDirection = new Vector3 (xAxisDir, yAxisDir);
			
			for (int i = 0; i < 3; i++) {
				float x = (p.x + c.x + (s.x / 2)) - (s.x / 2 * i * (yAxisDir * yAxisDir)) + ((s.x / 2 * (xAxisDir - 1)) * (xAxisDir * xAxisDir));
				float y = (p.y + c.y + (s.y / 2)) - (s.y / 2 * i * (xAxisDir * xAxisDir)) + ((s.y / 2 * (yAxisDir - 1)) * (yAxisDir * yAxisDir));
				Ray ray = new Ray (new Vector3 (x, y, 0), castDirection);
				Debug.DrawRay(ray.origin,ray.direction);
				RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, col.radius, pMask);
				if (hit && hit.collider.gameObject == player) {
					//Debug.Log("PLAYER IS IN SIGHT");
					//playerInSight = true;
					gameObject.GetComponentInParent<EnemyAI>().setPlayerInSightTrue();
					if (Utilities.hasMatchingTag("Ranged", gameObject.transform.parent.gameObject)) {
						//Debug.Log("poop");
						gameObject.GetComponentInParent<Ranged>().setPlayerInSightTrue();
					}
				}
			}
		}
		if (other.gameObject.CompareTag ("Enemy")) {
			if (other.gameObject.GetComponentInParent<Enemy>().isHitTrue() == true) {
				gameObject.GetComponentInParent<EnemyAI>().setPlayerInSightTrue();
				other.gameObject.GetComponentInParent<Enemy>().isHitFalse();
				if (Utilities.hasMatchingTag("Ranged",gameObject.transform.parent.gameObject)) {
					gameObject.GetComponentInParent<Ranged>().setPlayerInSightTrue();
				}
			}
		}
	}
	
	void OnTriggerExit2D (Collider2D other)
	{
		// If the player leaves the trigger zone...
		if (other.gameObject == player) {
			// ... the player is not in sight.
			//playerInSight = false;
			//outOfRange = true;
			Invoke("invokeSetFalse", chaseTime);
			//Debug.Log("NOT IN SIGHT");
		}
	}

	void invokeSetFalse() {
		gameObject.GetComponentInParent<EnemyAI>().setPlayerInSightFalse();
		if (Utilities.hasMatchingTag("Ranged",gameObject.transform.parent.gameObject)) {
			gameObject.GetComponentInParent<Ranged>().setPlayerInSightTrue();
		}
	}
}
