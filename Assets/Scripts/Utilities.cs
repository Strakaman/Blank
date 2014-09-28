using UnityEngine;
using System.Collections;

public static class Utilities
{
	public static void saveGame()
	{

	}

	public static void loadGame()
	{

	}

	/**
	 * Used to instantiate any object at the passed in position
	 * Pass in a velocity vector if the object should move
	 * Pass in a orientation vector so that the sprite can be rotated accordingly
	 */ 
	public static GameObject cloneObject(Direction direction, GameObject bulletToClone, Vector3 placetoCreate, Vector3 velocity, Quaternion orientation)
	{
		GameObject clonedesu = (GameObject)ScriptableObject.Instantiate (bulletToClone, placetoCreate, orientation);
		if (clonedesu.rigidbody2D) {clonedesu.rigidbody2D.velocity = velocity;}
		return clonedesu;
	}
}

