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
	{
		if (trigInfo.gameObject.tag.Equals("BlueSpellObject"))
		    {
			Debug.Log ("Turn to ice. too tired to actually code now");
		}
	}
}
