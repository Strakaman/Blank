using UnityEngine;
using System.Collections;

public static class Utilities
{
	public static void save()
	{

	}

	public static void load()
	{

	}

	public static GameObject cloneObject(Direction direction, GameObject bulletToClone, Vector3 placetoCreate, Vector3 velocity, Quaternion orientation)
	{
		GameObject clonedesu = (GameObject)ScriptableObject.Instantiate (bulletToClone, placetoCreate, orientation);
		if (clonedesu.rigidbody2D) {clonedesu.rigidbody2D.velocity = velocity;}
		return clonedesu;
	}
}

