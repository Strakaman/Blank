using UnityEngine;
using System.Collections;

public static class PlayerInfo 
{
	private const int MAXMANA = 300;
	private const int MAXHEALTH = 100;
	private static int mana = MAXMANA;
	private static int health = MAXHEALTH;

	public static int getHealth()
	{
		return health;
	}

	public static int getMana()
	{
		return mana;
	}

	public static void resetPlayer()
	{
		mana = MAXMANA;
		health = MAXHEALTH;
	}

	public static void changeHealth(int byHowMuchHealth)
	{
		health += byHowMuchHealth;
		if (health > MAXHEALTH)
		{
			health = MAXHEALTH;
		}
		if (health <= 0)
		{
			health = 0;
			//TODO: kill player workflow
		}
	}

	public static void changeMana(int byHowMuchMana)
	{
		mana += byHowMuchMana;
		if (mana > MAXMANA)
		{
			mana = MAXMANA;
		}
		if (mana < 0)
		{
			mana = 0;
		}
	}
}
