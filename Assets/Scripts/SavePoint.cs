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

		public override void interact ()
		{
				PlayerInfo.resetPlayer ();
				Utilities.saveGame ();
		}
}
