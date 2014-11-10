using UnityEngine;
using System.Collections;

public class Boss : Enemy {
	public GameObject[] protectObjects;
	private int numObjects;
	public float berserkSpeed = 4;

	void Start() {
		enemyStart ();
	}

	void Update() {
		enemyUpdate ();
		bossUpdate ();
	}

	void bossUpdate() {
		checkNullObjects (protectObjects);
		if (numObjects != 0) {
			health = maxHealth;
			if (!stunned && !slowed) {
				GetComponent<SpriteRenderer> ().material = Default;
			}
		} else {
			GetComponent<EnemyAIOld>().speed = berserkSpeed;
			
		}
	}

	//Checks to see how many objects are not null in an array. 
	public void checkNullObjects(GameObject[] array) {
		numObjects = array.Length;
		for (int i = 0; i < array.Length; i++) {
			if (array[i] == null) {
				numObjects--;
			}
		}
	}
}