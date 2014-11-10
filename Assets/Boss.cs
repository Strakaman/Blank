using UnityEngine;
using System.Collections;

public class Boss : Enemy {
	public GameObject[] protectObjects;
	private int numObjects;

	void Start() {
		enemyStart ();
	}

	void Update() {
		enemyUpdate ();
		checkNullObjects ();
		if (numObjects != 0) {
			health = maxHealth;
			GetComponent<SpriteRenderer>().material = Default;
		}
	}

	void checkNullObjects() {
		numObjects = protectObjects.Length;
		for (int i = 0; i < protectObjects.Length; i++) {
			if (protectObjects[i] == null) {
				numObjects--;
			}
		}
	}
}