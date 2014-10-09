using UnityEngine;
using System.Collections;

public class SavePoint : Interactable
{
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public override void interact (GameObject player)
		{
				PlayerInfo.resetPlayer ();
				Utilities.saveGame ();
		}
}
