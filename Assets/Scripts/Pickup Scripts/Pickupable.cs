using UnityEngine;
using System.Collections;

public abstract class Pickupable : MonoBehaviour {

	public abstract void OnTriggerEnter2D(Collider2D whatICollidedWith);
}
