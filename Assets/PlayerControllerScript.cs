using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour {
	public GameObject refBullet;
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
			GameObject clonedesu = (GameObject)Instantiate(refBullet, transform.position+ new Vector3(0,.5,0),transform.rotation);

			BroadcastMessage("ApplyVelocity",clonedesu);
		}

		transform.Translate(new Vector3(Input.GetAxis("Horizontal")*.05f,0,0));
	}
}
