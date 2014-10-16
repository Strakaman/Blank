using UnityEngine;
using System.Collections;

public class BulletDesu : MonoBehaviour {
	public Animator animator;
	// Use this for initialization
	void Start () {
		animator = (Animator)GetComponent ("Animator");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter2D(Collision2D collInfo) {
		//Debug.Log(collInfo.gameObject.name);	
		animator.SetBool ("Does Collide", true);
		gameObject.collider2D.enabled = false;
		rigidbody2D.velocity = new Vector2(0,0);
		Destroy(gameObject,.5f);
	}
}