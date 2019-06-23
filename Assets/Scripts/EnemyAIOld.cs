using UnityEngine;
using System.Collections;

public class EnemyAIOld : MonoBehaviour {
	public float chaseTime = 2;
	public float speed;
	private Vector2 Playerdirection;
	private float Xdif;
	private float Ydif;
	private GameObject player;                      // Reference to the player.
	private Vector3 playerTransform;                      // Reference to the player's transform.
    private Enemy m_Enemy;
    private Rigidbody2D m_Rigidbody;

	void Start() {
		player = GameObject.FindGameObjectWithTag ("Player");
        m_Enemy = GetComponent<Enemy>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		//Player = GameObject.FindGameObjectWithTag ("Player").transform.position;
		//rigidbody2D.velocity = (Playerdirection.normalized * speed);
		playerTransform = player.transform.position;
	}

    void FixedUpdate()
    {
        if (m_Enemy.playerInSight == true)
        {
            //Debug.Log ("Chasing");
            if (m_Enemy.isStunned() == false)
            {
                Chasing();
            }
        }
    }
	
	void Chasing ()
	{
		Xdif = playerTransform.x - transform.position.x;
		Ydif = playerTransform.y - transform.position.y;
		
		Playerdirection = new Vector2 (Xdif, Ydif);
		GetComponent<Rigidbody2D>().velocity = (Playerdirection.normalized * speed);
	}
}
