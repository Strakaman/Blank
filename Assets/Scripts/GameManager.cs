using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public GameObject healthPickup;
	public GameObject manaPickup;

	// Use this for initialization
	void Start () {
		//keep constructor but swap out current array for spells.add when implemented
		RedSpell spellObj = (RedSpell)ScriptableObject.CreateInstance ("RedSpell");
		spellObj.initializeSpell ("Red Rings of Death", "Has a tendency to burn things", 25, 10);
		spellObj.unlockSpell ();
		SpellBook.add (spellObj);
		BlueSpell spellObj2 = (BlueSpell)ScriptableObject.CreateInstance ("BlueSpell");
		spellObj2.initializeSpell ("Blue Balls", "Bounce like a basketball", 25, 10);
		spellObj2.unlockSpell ();
		SpellBook.add (spellObj2);
	}

	void DropHealthPickup(Vector3 locationToDrop)
	{
		Instantiate (healthPickup, locationToDrop, new Quaternion());
	}

	void DropManaPickup(Vector3 locationToDrop)
	{
		Instantiate (manaPickup, locationToDrop, new Quaternion());
	}

	// Update is called once per frame
	void Update () {

	
	}
}
