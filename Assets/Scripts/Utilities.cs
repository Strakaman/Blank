using UnityEngine;
using System.Collections;
using System.IO;
using System;

public static class Utilities
{
		public static void saveGame ()
		{
				//Debug.Log (Application.dataPath);
				try {
						String savePath = Application.dataPath + "/test.blanksav";
						StreamWriter saver = new StreamWriter (savePath, false);
						saver.WriteLine ("Save Data");
						saver.WriteLine (Application.loadedLevelName);
						String delimiter = "";
						foreach (Spell s in SpellBook.playerSpells) {
								saver.Write (delimiter + s.isSpellUnlocked ());
								delimiter = "||";
						} 		
						saver.Write ("\n");
						GameObject playaPlaya = GameObject.FindGameObjectWithTag ("Player");
						Vector3 playaPos = playaPlaya.transform.position;
						saver.WriteLine (playaPos.x + delimiter + playaPos.y + delimiter + playaPos.z);
						Debug.Log ("Game Saved (sorta) to " + savePath + ".");
						saver.Close ();
				} catch (Exception e) {
						//TODO: handle exception
						Debug.Log ("Error reading save file: " + e.Message);
				}
		}

		public static void loadGame ()
		{

		}

		/**
* Used to instantiate any object at the passed in position
* Pass in a velocity vector if the object should move
* Pass in a orientation vector so that the sprite can be rotated accordingly
*/ 
		public static GameObject cloneObject (Direction direction, GameObject bulletToClone, Vector3 placetoCreate, Vector3 velocity, Quaternion orientation)
		{
				GameObject clonedesu = (GameObject)ScriptableObject.Instantiate (bulletToClone, placetoCreate, orientation);
				if (clonedesu.rigidbody2D) {
						clonedesu.rigidbody2D.velocity = velocity;
				}
				return clonedesu;
		}
}

