using UnityEngine;
using System.Collections;

public abstract class Pickupable : MonoBehaviour {

	protected SoundType mySound = SoundType.pickup;
	public abstract void OnTriggerEnter2D(Collider2D whatICollidedWith);
}
