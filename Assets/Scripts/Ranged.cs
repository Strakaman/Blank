using UnityEngine;
using System.Collections;

public class Ranged : Enemy {
	public GameObject refBullet;
	public int projectileSpeed = 4;
	private Vector2 Playerdirection;
	private float Xdif;
	private float Ydif;
	private Vector3 playerTransform; 
	public bool playerInSight;    
	public float COOLDOWNTIME = 1f;
	private float timeSinceLastFired = 0f;
		
	// Update is called once per frame
	void Update () {
		enemyUpdate ();
		playerTransform = player.transform.position;
		if (playerInSight == true && canFire () && !stunned) {
			//Debug.Log("stun: " + stunned);
						//Debug.Log("IN SIGHT!");
			rangedAttack ();
		}
	}

	private bool canFire()
	{
		if (timeSinceLastFired > COOLDOWNTIME) 
		{
			timeSinceLastFired = 0f;
			return true;
		}
		else
		{
			timeSinceLastFired += Time.deltaTime;
			return false;
		}
	}

	public void setPlayerInSightTrue() {
		playerInSight = true;
	}
	public void setPlayerInSightFalse() {
		playerInSight = false;
	}

	private void rangedAttack()
	{
		if (refBullet == null) return;
		Vector3 clonePosition = enemy.transform.position;
		Vector3 cloneVelocity = new Vector3(0,0,0);
		Quaternion cloneOrientation = Quaternion.Euler(0,0,0); 
		//GameObject clonedesu = createSpellObject(direction, bulletToClone, clonePosition, cloneVelocity, cloneOrientation);
		GameObject clonedesu = Utilities.cloneObject(direction, refBullet, clonePosition, cloneVelocity, cloneOrientation);
		projectileTrajectory (clonedesu);
		//Debug.Log(cloneVelocity);
		Physics2D.IgnoreCollision (clonedesu.collider2D, collider2D);
		Destroy (clonedesu,2);
	}

	Vector3 projectileTrajectory (GameObject clone)
	{
		Xdif = playerTransform.x - clone.transform.position.x;
		Ydif = playerTransform.y - clone.transform.position.y;
		Playerdirection = new Vector2 (Xdif, Ydif);
		clone.rigidbody2D.velocity = (Playerdirection.normalized * projectileSpeed);
		return refBullet.rigidbody2D.velocity;
	}
}
