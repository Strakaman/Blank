using UnityEngine;
using System.Collections;

public class NPCSpellUnlocker : NPCScript {
	public string spellToUnlock;
	bool followUpsCompleted = false;
	// Use this for initialization

	public override void FollowUps()
	{
		if (!followUpsCompleted)
		{
			if (spellToUnlock == null) { return;}
			spellToUnlock = spellToUnlock.ToUpper();
			string realSpellName="";
			if (spellToUnlock.Contains("BLUE"))
			{
				realSpellName = SpellBook.BLUESPELLNAME;
			}
			else if (spellToUnlock.Contains("YELLOW"))
			{
				realSpellName = SpellBook.YELLOWSPELLNAME;
			}
			else if (spellToUnlock.Contains("WHITE"))
			{
				realSpellName = SpellBook.WHITESPELLNAME;
			}
			SpellBook.findSpellbyName(realSpellName).unlockSpell();
			Utilities.TellPlayer("New Spell Unlocked!");
			followUpsCompleted = true;
		}
	}
}
