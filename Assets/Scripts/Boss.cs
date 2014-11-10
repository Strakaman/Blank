using UnityEngine;
using System.Collections;

public class Boss : Enemy {
	public GameObject[] protectObjects;
	private int numObjects;
	public int berserkSpeed = 5;

	void Start() {
		enemyStart ();
	}

	void Update() {
		enemyUpdate ();
		checkNullObjects ();
		if (numObjects != 0) {
			health = maxHealth;
			if (!stunned && !slowed) {
			GetComponent<SpriteRenderer> ().material = Default;
			}
		} else {
			GetComponent<EnemyAIOld>().speed = berserkSpeed;

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