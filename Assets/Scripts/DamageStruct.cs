using UnityEngine;
public struct DamageStruct
{
		public DamageStruct (int d, GameObject o, int kb, float hd)
		{
				damage = d;
				coll = o;
				knockback = kb;
				hitDelay = hd;

		}
		public int damage;
		public GameObject coll;
		public int knockback;
		public float hitDelay;
}


