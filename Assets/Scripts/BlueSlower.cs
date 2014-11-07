using UnityEngine;
using System.Collections;

public class BlueSlower : MonoBehaviour
{
	public string whoToSlow; //Player or Enemy
	public int damage; //should be a low number since damage is applied during onTriggerStay
	public bool chargedVersion; //charged version will also do damage yaay

	// Use this for initialization
	void Start ()
	{
		if (whoToSlow == null) {
				whoToSlow = "Enemy";
		}
	}

	// Update is called once per frame
	void Update ()
	{

	}

	void OnTriggerStay2D (Collider2D collInfo)
	{//if meant to hit player, will hit player, if meant to hit enemy, will hit enemy
		if (Utilities.hasMatchingTag (whoToSlow, collInfo.gameObject)) { 
				if (chargedVersion) { //charged version does damage but normal version does not
						DamageStruct thisisntastructanymore = new DamageStruct (damage, collider2D.gameObject, 0, 0); 
						//struct used to pass more than one parameter through send message, which only lets you pass one object as a parameter
						collInfo.gameObject.SendMessage("callDamage",thisisntastructanymore);
				}
				collInfo.gameObject.SendMessage ("SlowYourself"); //call method in object that should get slowed and they will slow themselves
		}
	}
}
