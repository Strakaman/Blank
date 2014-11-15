using UnityEngine;
using System.Collections;

public class CreatureSpawner : Enemy {
	public GameObject enemyToSpawn;


	void Start()
	{
		enemyStart();
		Spawn2RevengeofTheSpawner();
	}

	void Spawn2RevengeofTheSpawner()
	{
		Instantiate(enemyToSpawn,new Vector3(transform.position.x,transform.position.y-1,0),Quaternion.Euler(0,0,0));
	}
	

	
	void OnCollisionStay2D(Collision2D collInfo)
	{	
		//should not actually damage player so leave this blank
	}

	
	//Sets the walking animation for the player if they are moving
	new void setWalk ()
	{
		//* since animator does not have these bools, do nothing*/
	}
	
	//Sets the idle animation for the player if velocities are 0
	new void setIdle ()
	{
		//* since animator does not have these bools, do nothing*/
	}
	
	//Method to set the animation for all four directions
	new void SetBools (bool down, bool up, bool left, bool right)
	{
		//* since animator does not have these bools, do nothing*/
	}
	
	new void callDamage(DamageStruct dstruct)
	{
		damageProperties(dstruct.coll.gameObject, dstruct.damage, 0, dstruct.hitDelay);
		//adjusted so spawner never takes the knockback
		GetComponent<SpriteRenderer>().material = Hit;
	}

}
