using UnityEngine;
using System.Collections;
using System.IO;
using System;

public static class Utilities
{
		static bool debugModeOrNot;

		public static void saveGame ()
		{
				//Debug.Log (Application.dataPath);
				try {
						String savePath = Application.dataPath + "/test.blanksav";
						StreamWriter saver = new StreamWriter (savePath, false);
						saver.WriteLine ("Save data stores player's last scene, list of unlocked spells, and last position");
						saver.WriteLine (Application.loadedLevelName);
						String delimiter = "";
						foreach (Spell s in SpellBook.playerSpells) {
								saver.Write (delimiter + s.isSpellUnlocked ());
								delimiter = "|";
						} 		
						saver.Write ("\n");
						GameObject playaPlaya = GameObject.FindGameObjectWithTag ("Player");
						Vector3 playaPos = playaPlaya.transform.position;
						saver.WriteLine (playaPos.x + delimiter + playaPos.y + delimiter + playaPos.z);
						Debug.Log ("Game Saved (sorta) to " + savePath + ".");
						saver.Close ();
				} catch (Exception e) {
						//TODO: handle exception
						Debug.Log ("Error writing save file: " + e.Message);
				}
		}

		public static void loadGame ()
		{
				try {
						String savePath = Application.dataPath + "/test.blanksav";
						StreamReader loader = new StreamReader (savePath, false);
						loader.ReadLine ();
						String currLevelName = loader.ReadLine ();
						intializeSpellBook (debugModeOrNot);
						String[] unlockBools = loader.ReadLine ().Split ('|');
						int length = unlockBools.Length;
						int i;
						bool testBool;
						for (i = 0; i < length; i++) {
								Boolean.TryParse (unlockBools [i], out testBool);				
								if (testBool) {
										SpellBook.playerSpells [i].unlockSpell ();
								}
						}
						String[] coordinates = loader.ReadLine ().Split ('|');
						float x, y, z;
						float.TryParse (coordinates [0], out x);
						float.TryParse (coordinates [1], out y);
						float.TryParse (coordinates [2], out z);
						Vector3 playaPos = new Vector3(x,y,z);
						loader.Close(); 
						Application.LoadLevel(currLevelName);
						//TODO: move plaver to position of loading
				} catch (Exception e) {
						//TODO: handle exception
						Debug.Log ("Error reading save file: " + e.Message);
				}
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

		public static void intializeSpellBook (bool debugMode)
		{
				debugModeOrNot = debugMode;
				if (SpellBook.size () > 0) {
						return;
				} //just in case I'm trying to add to a spell book that's already there.
				RedSpell spellObj = (RedSpell)ScriptableObject.CreateInstance ("RedSpell");
				spellObj.initializeSpell (SpellBook.REDSPELLNAME, "Has a tendency to burn things", 30, 10, 2);
				spellObj.unlockSpell ();
				SpellBook.add (spellObj);
				YellowSpell spellObj2 = (YellowSpell)ScriptableObject.CreateInstance ("YellowSpell");
				spellObj2.initializeSpell (SpellBook.YELLOWSPELLNAME, "Charges things", 30, 10, 2);
				SpellBook.add (spellObj2);
				BlueSpell spellObj3 = (BlueSpell)ScriptableObject.CreateInstance ("BlueSpell");
				spellObj3.initializeSpell (SpellBook.BLUESPELLNAME, "Useful on water", 100, 1);
				SpellBook.add (spellObj3);
				if (debugMode) { //can switch flag at the top of game manager so everything acts in a debug mode to make testing easier
						spellObj2.unlockSpell ();
						spellObj3.unlockSpell ();
				}

		}
}

