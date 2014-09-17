using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour {
	public GameObject refBullet;
	public GameObject refBullet2;

	public Animator animator;
	public const float speed = 10;
	// Use this for initialization
	void Start () {
		animator = (Animator)GetComponent("Animator");
	}
	
	// Update is called once per frame
	void Update () {
		CheckInputs();
		SpriteAnimation ();
	}

	void SpriteAnimation() {
		if (rigidbody2D.velocity.x != 0 || rigidbody2D.velocity.y != 0) {
			animator.SetBool ("doWalk", true);
		} else {
		animator.SetBool ("doWalk", false);
		}
	}
	/**
	 * Used for all player button inputs...I guess...
	 */
	void CheckInputs()
	{
		//transform.Translate(new Vector3(Input.GetAxis("Horizontal")*.05f,0,0)); //O: better to create horizontal vector once?
		//transform.Translate(new Vector3(0,Input.GetAxis("Vertical")*.05f,0));   //O: better to create horizontal vector once?
		rigidbody2D.velocity = new Vector2(Input.GetAxis ("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
		if (Input.GetButtonDown("Fire Spell"))
		{
			FireSpell();
		}
		if (Input.GetButtonDown("Bounce Spell"))
		{
			BounceSpell();
		}
	}

	/**
	 * Offensive spell for player 
	 */
	void FireSpell()
	{
		GameObject clonedesu = (GameObject)Instantiate(refBullet, transform.position,transform.rotation);
		clonedesu.rigidbody2D.velocity = new Vector3(speed,0,0);
		Destroy(clonedesu, 3);
	}

	void BounceSpell() {
		GameObject clonedesu = (GameObject)Instantiate(refBullet2, transform.position,transform.rotation);
		clonedesu.rigidbody2D.velocity = new Vector3(speed,0,0);
		Destroy(clonedesu, 3);
	}
}
