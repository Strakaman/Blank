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
	void OnCollisionEnter2D() {
		if (GameObject.FindGameObjectWithTag("RedSpellObject")) {
						animator.SetBool ("Does Collide", true);
				}
	}
}
