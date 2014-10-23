using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
	bool isIceBlock;
	bool destroyThis = false;
	// Use this for initialization
	void Start () {
		if (Utilities.hasMatchingTag("Ice Block",gameObject))
		{
			isIceBlock = true;
		}
		else
		{
			isIceBlock = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (destroyThis == true)
		{
			collider2D.enabled = false;
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D collInfo)
	{	//when a blue spell touches water, it turns to ice
		//if ((isIceBlock)&&(collInfo.gameObject.tag.Equals("RedSpellObject")))
			if ((isIceBlock)&&(Utilities.hasMatchingTag("Melter",collInfo.gameObject)))
		{
			GameObject water = GameObject.FindGameObjectWithTag("Water"); //if we add animation, change this to invoke on helper method based on animation length
			Utilities.cloneObject(Direction.down, water, gameObject.transform.position, new Vector3(0,0,0), Quaternion.Euler(0,0,0));
			//play animation?
			destroyThis = true; //added boolean because the object was getting destroyed before the OnCollision method of the other objectwas occuring
			//collider2D.enabled = false; 
			//Destroy(gameObject);
		}
	}
}
