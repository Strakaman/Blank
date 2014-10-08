using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D trigInfo)
	{	//when a blue spell touches water, it turns to ice
		if (Utilities.hasMatchingTag("BlueSpellObject",trigInfo.gameObject))
		    {
			GameObject ice = GameObject.FindGameObjectWithTag("Ice Block"); //if we add animation, change this to invoke on helper method based on animation length
			Utilities.cloneObject(Direction.down, ice, gameObject.transform.position, new Vector3(0,0,0), Quaternion.Euler(0,0,0));
			//play animation?
			Destroy(gameObject);
		}
	}
}
