using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		CheckInputs();
	
	}

	/**
	 * Used for all player button inputs...I guess...
	 */
	void CheckInputs()
	{
		if (Input.GetButtonDown("Fire Spell"))
		{
			Debug.Log("Mystical Fire Spell, I summon thee!");
		}
		transform.Translate(new Vector3(Input.GetAxis("Horizontal")*.05f,0,0));
	}
}
