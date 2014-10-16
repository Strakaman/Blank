using UnityEngine;
using System.Collections;

public class AggroNavi : MonoBehaviour {
	public GameObject player;
	private bool inBeastMode = false; 
	//boolean allows reference object to stay put but when the player actually casts,
	//the new object will follow the player and constantly be in attack mode
	public GameObject refBullet;
	protected GameObject enemy;
	protected int projectileSpeed = 10;
	protected Vector2 shotDirection;
	private float Xdif;
	private float Ydif;
	private Vector3 enemyPosition;  
	private const float COOLDOWNTIME = 5f;
	private const int ROUNDSPERBURST = 5;
	private const float BURSTDELAY = .2f;
	private float timeSinceLastFired = 0f;
	Quaternion cloneOrientation = Quaternion.Euler(0,0,0);

	// Use this for initialization
	void Start () {
		if (player == null)
		{
			player = GameObject.FindGameObjectWithTag("Player");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (inBeastMode)
		{
			transform.position = (player.transform.position - (new Vector3(-1,0,0)));
			timeSinceLastFired += Time.deltaTime;
		}
	}

	//used to tell casted version of object to actually attack
	void BeastMode()
	{
		inBeastMode = true;
	}

	void shotsFired ()
	{
		Xdif = transform.position.x - enemyPosition.x;
		Ydif = transform.position.y - enemyPosition.y;
		shotDirection = new Vector2 (Xdif, Ydif);
		Vector2 cloneVelocity = (shotDirection.normalized * projectileSpeed);
		GameObject clonedesu = Utilities.cloneObject(Direction.down, refBullet, transform.position, cloneVelocity, cloneOrientation);
		Physics2D.IgnoreCollision (clonedesu.collider2D, collider2D);
		Destroy (clonedesu,2);
	}


	void OnTriggerStay2D(Collision2D collInfo)
	{
		if (Utilities.hasMatchingTag("Enemy",collInfo.gameObject))
		{
			if (timeSinceLastFired >= COOLDOWNTIME)
			{
				timeSinceLastFired = 0f;
				enemyPosition = collInfo.transform.position;
				for (int i=0; i < ROUNDSPERBURST; i++)
				{
					Invoke("shotsFired",BURSTDELAY*i);
				}
			}
		}
	}


}
