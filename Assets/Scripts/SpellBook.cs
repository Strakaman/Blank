using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class SpellBook
{
		public static List<Spell> playerSpells = new List<Spell> ();
		
		public static void add (Spell sp)
		{
				playerSpells.Add (sp);

		}



}


