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
		Debug.Log("hsahshashasanfsjnlfaslkjnvsdlkjnvdslnbzfz,k,mn");
		Instantiate(enemyToSpawn,new Vector3(transform.position.x,transform.position.y,0),Quaternion.Euler(0,0,0));
	}
	
	
}
