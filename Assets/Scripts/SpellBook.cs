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
		
		public static void add (Spell sp)
		{
				playerSpells.Add (sp);

		}



}


