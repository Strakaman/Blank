using UnityEngine;
using System.Collections;

public abstract class Spell
{
	string name;
	string description;
	GameObject player; //get transform for casting purposes
	int manaCost;

	public void CreateProjectile()
	{


	}

	public abstract void execute(int dir)
	{
	

	}
}
