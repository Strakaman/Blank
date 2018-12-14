using UnityEngine;
using System.Collections;

public enum SoundType
{
	pickup = 1
};
public class SoundEffectPlayer : MonoBehaviour {

	public AudioClip pickupSound;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void playSoundEffect(SoundType typeofSound)
	{
		if (typeofSound.Equals(SoundType.pickup))
		{
			GetComponent<AudioSource>().clip = pickupSound;
			GetComponent<AudioSource>().Play();
		}
	}

}
