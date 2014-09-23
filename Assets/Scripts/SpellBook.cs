using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
* List of spells accessed through the game manager. Used as player's list of spells. 
* Ever spell the player will used in the game should be added to this list, whether locked by default or unlicked
*/ 
public static class SpellBook
{
	public static List<Spell> playerSpells = new List<Spell> ();
	public const string REDSPELLNAME = "Red Rings of Death";
	public const string YELLOWSPELLNAME = "Yellow Spark";

	//used to add spell to spell book. Should only be called by game manager
	public static void add (Spell sp)
	{
			playerSpells.Add (sp);

	}

	//used to look up a spell by name, not the most efficient but whatevs
	//only reason i could think of to look up a spell directly is to unlock it. 
	//should call this passing in one of the constant strings listed aboce
	public static Spell findSpellbyName (string nameLookup)
	{
			foreach (Spell s in playerSpells) {
					if (s.getName ().Equals (nameLookup)) {
							return s;
					}
			}
			return null;
	}

	//return spell size
	public static int size ()
	{

			return playerSpells.Count;
	}

	//returns the number of spells unlocked. Maybe useful calculating for display purposes
	public static int countUnlockedSpells ()
	{
			int counter = 0;
			foreach (Spell s in playerSpells) {
					if (s.isSpellUnlocked ()) {
							counter++;
					}
			}
			return counter;
	}
}


