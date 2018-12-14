using UnityEngine;
using System.Collections;

public class BulletDesu : MonoBehaviour {
	public Animator animator;
	public int damage; //how much damage they do
	public float animationTime; //used to determine how long after collision before destroying self since that should be based on animation time
	public string whoToDamage; //Set to Player or Enemy in Inspector
	public int projectileKnockback; //for knockback
	public float hitDelay; //for a delay between when another hit would register  
    private Rigidbody2D m_rigidbody2D;
    private Vector3 startVel;
    //public AudioClip bulletSound;
    // Use this for initialization

    void Start () {
		animator = (Animator)GetComponent ("Animator");
		if (whoToDamage == null) {whoToDamage = "Player";}
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        startVel = m_rigidbody2D.velocity;
    }

    private void FixedUpdate()
    {
        m_rigidbody2D.velocity = startVel; //TODO: never do this again
    }
    void OnCollisionEnter2D(Collision2D collInfo) {
		//Debug.Log(collInfo.gameObject.name);		
		if (animator) {
			animator.SetBool ("Does Collide", true);} //not all objects that this script is attached to have animations
		gameObject.GetComponent<Collider2D>().enabled = false; //once it hits one object it should no longer be able to hit another object
		if (Utilities.hasMatchingTag(whoToDamage,collInfo.gameObject))
		{
			DamageStruct thisisntastructanymore = new DamageStruct(damage,GetComponent<Collider2D>().gameObject,100,.1f);
			//struct used to pass more than one parameter through send message, which only lets you pass one object as a parameter
			collInfo.gameObject.SendMessage("callDamage",thisisntastructanymore);
		}
		startVel = new Vector3(0,0,0);
		Destroy(gameObject,animationTime); //allows for enough time to play explosion animation before destroying itself
	}
}
