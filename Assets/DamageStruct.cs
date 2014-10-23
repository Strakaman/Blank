using UnityEngine;
public class DamageStruct : MonoBehaviour
		{
			public DamageStruct(int d, Collider2D c)
	{
		damage = d;
		coll = c;
	}
			public int damage;
			public Collider2D coll;
		}


