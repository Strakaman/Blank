using UnityEngine;
using System.Collections;

public class BulletDesu : MonoBehaviour {
	public Animator animator;
	public int damage; //how much damage they do
	public float animationTime; //used to determine how long after collision before destroying self since that should be based on animation time
	public string whoToDamage; //Set to Player or Enemy in Inspector
	public int projectileKnockback; //for knockback
	public float hitDelay; //for a delay between when another hit would register  
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
			animator.SetBool ("Does Collide", true);} //not all objects that this script is attached to have animations
		gameObject.collider2D.enabled = false; //once it hits one object it should no longer be able to hit another object
		if (Utilities.hasMatchingTag(whoToDamage,collInfo.gameObject))
		{
			DamageStruct thisisntastructanymore = new DamageStruct(damage,collider2D.gameObject,100,.1f);
			//struct used to pass more than one parameter through send message, which only lets you pass one object as a parameter
			collInfo.gameObject.SendMessage("callDamage",thisisntastructanymore);
		}
		rigidbody2D.velocity = new Vector2(0,0);
		Destroy(gameObject,animationTime); //allows for enough time to play explosion animation before destroying itself
	}
}
