using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
		public static GameManager gM;
		public GameObject healthPickup;
		public GameObject manaPickup;
		public bool debugMode = true;

		void Start() {
		}

		// Use this for initialization
		void Awake ()
		{
				if (!gM) {
						gM = this;
						DontDestroyOnLoad (gameObject);
				} else {
						Destroy (gameObject);
				}
				Utilities.intializeSpellBook (debugMode);
		}

		/*pass in the x,y,and z coordinates of the place where you want the pickup to be dropped
	pickup needs to exist in unity and be a child prefab of the game manager prefab
	Ex: when maybe a monster dies or something, pass in the position of the transform of the monster*/
		void DropHealthPickup (Vector3 locationToDrop)
		{
				Instantiate (healthPickup, locationToDrop, new Quaternion ());
		}

		/* same as health above*/
		void DropManaPickup (Vector3 locationToDrop)
		{
				Instantiate (manaPickup, locationToDrop, new Quaternion ());
		}

		// Update is called once per frame
		void Update ()
		{

	
		}
}
