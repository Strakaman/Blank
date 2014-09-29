using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {
	public GameObject refBullet;
	public bool shoot;
	public Direction direction;
	public int projectileSpeed = 10;
	public const float COOLDOWNTIME = .25f;
	private float timeSinceLastFired = 0f;
	// Use this for initialization
	void Start () {
		refBullet = GameObject.FindGameObjectWithTag("EnemyProjectile");
	}
	
	// Update is called once per frame
	void Update () {
	if ((shoot) &&(canFire()))
		{
			fireBullet();
		}
	}

	//used kind of like a cooldown because shooting a bullet every frame is excessive, trust me
	bool canFire()
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
	void fireBullet()
	{
		if (refBullet == null) return;
		Vector3 clonePosition = new Vector3(0,0,0) ;
		Vector3 cloneVelocity = new Vector3(0,0,0);
		Quaternion cloneOrientation = Quaternion.Euler(0,0,0); 
		if (direction == Direction.down) {
			clonePosition = transform.position + new Vector3(0,-1,0);
			cloneVelocity = new Vector3 (0, -projectileSpeed, 0);
			cloneOrientation = Quaternion.Euler(0, 0, 270);
		}
		else if (direction == Direction.up) {
			clonePosition = transform.position + new Vector3(0,1,0);
			cloneVelocity = new Vector3 (0, projectileSpeed, 0);
			cloneOrientation = Quaternion.Euler(0, 0, 90);
		}
		else if (direction == Direction.left) {
			clonePosition = transform.position + new Vector3(-1, 0, 0);
			cloneVelocity = new Vector3 (-projectileSpeed, 0, 0);
			cloneOrientation = Quaternion.Euler(0, 0, 180);
		}
		else if (direction == Direction.right) {
			clonePosition = refBullet.transform.position + new Vector3(1,0,0);
			cloneVelocity = new Vector3 (projectileSpeed, 0, 0);
			cloneOrientation = Quaternion.Euler(0, 0, 0);
		}
		//GameObject clonedesu = createSpellObject(direction, bulletToClone, clonePosition, cloneVelocity, cloneOrientation);
		GameObject clonedesu = Utilities.cloneObject(direction, refBullet, clonePosition, cloneVelocity, cloneOrientation);
		//Debug.Log(cloneVelocity);
		Physics2D.IgnoreCollision (clonedesu.collider2D, collider2D);
		Destroy (clonedesu,1);
	}
}
