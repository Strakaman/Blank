using UnityEngine;
using System.Collections;

public class EnemyGroup : MonoBehaviour {
	
	private int numOfChildEnemies;
	private int numofNonChildren;
	private bool hasTriggeredAlready = false;
	// Use this for initialization
	void Start () {
		numOfChildEnemies = 0;
		numofNonChildren = 0;
		foreach (Transform child in transform)
		{
			if (child.CompareTag("Enemy"))
			{
				numOfChildEnemies++;
			}
		}
		numofNonChildren = transform.childCount - numOfChildEnemies;
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasTriggeredAlready)
		{
			if (transform.childCount == numofNonChildren)
			{
				hasTriggeredAlready = true;
				Utilities.flipStatusInChildren(transform);
			}
		}
	}
}
