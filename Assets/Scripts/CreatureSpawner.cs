using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreatureSpawner : Enemy {
	public GameObject enemyToSpawn; //object spawned by the creature spawner
	int maxEnemiesToSpawn = 3; //there will only ever be this many enemies on screen created by the spawner
	float spawnDelayTime = 3; //time in between spawning objects
	float timeSinceLastSpawn; //used to keep track of how much time has elapsed since last spawn
	List<Object> enemyList = new List<Object>(); 
	//used to keep track of how many spawned objects there are so that we don't create over the max

	void Start()
	{
		enemyStart(); //default enemy start code
		Spawn2RevengeofTheSpawner(); //spawn an enemy initially i guess
		timeSinceLastSpawn = 0; //initialize value
	}

	void Spawn2RevengeofTheSpawner()
	{
		//when object is created, add it to the list to keep track
		enemyList.Add(Instantiate(enemyToSpawn,new Vector3(transform.position.x,transform.position.y-1,0),Quaternion.Euler(0,0,0)));
	}
	
	void Update()
	{
		//delete later
		enemyUpdate(); //default enemy update code
		CheckIfShouldSpawn(); //see if it's time to spawn again
	}	

	/**
	 * If enough time has elapsed since last spawn, 
	 * and we haven't hit the max spawn count,
	 * spawn another object
	 */ 
	void CheckIfShouldSpawn()
	{
		timeSinceLastSpawn += Time.deltaTime;
		if (timeSinceLastSpawn >= spawnDelayTime)
		{
			UpdateEnemyList();
			if (enemyList.Count < maxEnemiesToSpawn)
			{
				if(enemyToSpawn != null) //to make sure there is a reference object
				{
						Spawn2RevengeofTheSpawner();
				}
			}
			timeSinceLastSpawn = 0;
		}
	}

	/**
	 * Since objects spawned can be destroyed, we need a way to remove them from the list
	 * so that they don't count towards the max limit
	 */ 
	void UpdateEnemyList()
	{
		/**
		 * using while loop here because for and foreach loops
		 * always increment the iterator and if we remove, we odn't want that
		 */
		int i=0;
		while (i < enemyList.Count) {
			if (enemyList[i] == null){
				enemyList.RemoveAt(i);
				}
			else {
				i++;
			}
		}
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
