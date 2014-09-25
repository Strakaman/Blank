using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public GameObject healthPickup;
	public GameObject manaPickup;

	// Use this for initialization
	void Awake () {
		//keep constructor but swap out current array for spells.add when implemented
		RedSpell spellObj = (RedSpell)ScriptableObject.CreateInstance ("RedSpell");
		spellObj.initializeSpell (SpellBook.REDSPELLNAME, "Has a tendency to burn things", 30, 10);
		spellObj.unlockSpell ();
		SpellBook.add (spellObj);
		YellowSpell spellObj2 = (YellowSpell)ScriptableObject.CreateInstance ("YellowSpell");
		spellObj2.initializeSpell (SpellBook.YELLOWSPELLNAME, "Charges things", 30, 10);
		spellObj2.unlockSpell ();
		SpellBook.add (spellObj2);
	}

	/*pass in the x,y,and z coordinates of the place where you want the pickup to be dropped
	pickup needs to exist in unity and be a child prefab of the game manager prefab
	Ex: when maybe a monster dies or something, pass in the position of the transform of the monster*/
	void DropHealthPickup(Vector3 locationToDrop)
	{
		Instantiate (healthPickup, locationToDrop, new Quaternion());
	}

	/* same as health above*/
	void DropManaPickup(Vector3 locationToDrop)
	{
		Instantiate (manaPickup, locationToDrop, new Quaternion());
	}

	// Update is called once per frame
	void Update () {

	
	}
}
