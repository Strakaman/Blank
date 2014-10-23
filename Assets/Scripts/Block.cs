using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
	bool isIceBlock;
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
	
	}

	void OnCollisionEnter2D(Collision2D collInfo)
	{	//when a blue spell touches water, it turns to ice
		//if ((isIceBlock)&&(collInfo.gameObject.tag.Equals("RedSpellObject")))
			if ((isIceBlock)&&(Utilities.hasMatchingTag("Melter",collInfo.gameObject)))
		{
			collider2D.enabled = false;
			GameObject water = GameObject.FindGameObjectWithTag("Water"); //if we add animation, change this to invoke on helper method based on animation length
			//Debug.Log(water.name);
			Utilities.cloneObject(Direction.down, water, gameObject.transform.position, new Vector3(0,0,0), Quaternion.Euler(0,0,0));
			//play animation?
			Destroy(gameObject);
		}
	}
}
