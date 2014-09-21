using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour {
	public int increaseHealth;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D whatICollidedWith)
	{
		if (whatICollidedWith.gameObject.tag == "Player")
		{
			//Debug.Log ("Player health increased by: " + increaseHealth + " to: " + PlayerInfo.getHealth());
			PlayerInfo.changeHealth(increaseHealth);
			Destroy(this.gameObject);
		}
	}
}
