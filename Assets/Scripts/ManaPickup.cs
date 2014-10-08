using UnityEngine;
using System.Collections;

public class ManaPickup : MonoBehaviour {
	public int increaseManaAmount;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	//when they player touches this object, destroy it and increase player's mana
	void OnTriggerEnter2D(Collider2D whatICollidedWith)
	{
		if (Utilities.hasMatchingTag("Player",whatICollidedWith.gameObject))
		{
			//Debug.Log ("Player mana increased by: " + increaseManaAmount + " to: " + PlayerInfo.getMana());
			PlayerInfo.changeMana(increaseManaAmount);
			Destroy(this.gameObject);
		}
	}
}
