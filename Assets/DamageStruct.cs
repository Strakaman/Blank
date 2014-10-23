using UnityEngine;
public struct DamageStruct
		{
			public DamageStruct(int d, GameObject o)
	{
		damage = d;
		coll = o;
	}
			public int damage;
			public GameObject coll;
		}


