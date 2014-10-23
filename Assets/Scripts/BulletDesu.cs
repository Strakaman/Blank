using UnityEngine;
using System.Collections;

public class BulletDesu : MonoBehaviour {
	public Animator animator;
	public int damage;
	public float animationTime;
	public string whoToDamage;
	// Use this for initialization
	void Start () {
		animator = (Animator)GetComponent ("Animator");
		if (whoToDamage == null) {whoToDamage = "Player";}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter2D(Collision2D collInfo) {
		//Debug.Log(collInfo.gameObject.name);		
		if (animator) {
			animator.SetBool ("Does Collide", true);}
		gameObject.collider2D.enabled = false;
		if (Utilities.hasMatchingTag(whoToDamage,collInfo.gameObject))
		{
			DamageStruct thisisntastructanymore = new DamageStruct(damage,collider2D.gameObject);
			collInfo.gameObject.SendMessage("callDamage",thisisntastructanymore);
		}
		rigidbody2D.velocity = new Vector2(0,0);
		Destroy(gameObject,animationTime);
	}
}
