using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {
	public bool playerInSight;                      // Whether or not the player is currently sighted.
	private GameObject player;                      // Reference to the player.
	private Vector3 playerTransform;                      // Reference to the player's transform.
	private CircleCollider2D col;                     // Reference to the sphere collider trigger component.
	public float fieldOfViewAngle = 110f;           // Number of degrees, centred on forward, for the enemy see.
	private Direction direction;
	private Vector3 s; //box collider size to help with raycasting
	private Vector3 c; //box collider center to help with raycasting
	private LayerMask pMask = 1 << 9;

	// Use this for initialization
	void Start () {
		BoxCollider2D zollider = GetComponent<BoxCollider2D> (); //get attached collider, store size and center
		s = zollider.size;
		c = zollider.center;
		col = GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerStay2D (Collider2D other)
	{
		Debug.Log ("TWO COLLIDERS!!!");
		// If the player has entered the trigger sphere...
		if(other.gameObject == player)
		{
			Debug.Log ("Player");
			
			Vector2 p = transform.position; //get current player position to cast ray from
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
				if (hit.collider) {
					Debug.Log(hit.collider.name);
				}
				if (hit && hit.collider.gameObject == player) {
					Debug.Log("PLAYER IS IN SIGHT");
					playerInSight = true;
				}
			}
		}
	}
	
	void OnTriggerExit2D (Collider2D other)
	{
		// If the player leaves the trigger zone...
		if (other.gameObject == player) {
			// ... the player is not in sight.
			playerInSight = false;
			Debug.Log("NOT IN SIGHT");
		}
	}
}
