using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {


	// Use this for initialization
	void Start () {
		//keep constructor but swap out current array for spells.add when implemented
		SpellBook.playerSpells[0] = new RedSpell("Red Rings of Death", "Has a tendency to burn things", 25,10);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
