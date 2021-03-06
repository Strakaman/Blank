﻿using UnityEngine;
using System.Collections;
/*
 * Static class used to represent player metadata
 * Made static because it makes my life easier
 * Since no instance of this class will ever be created, all added variables/methods should be static
 */
public static class PlayerInfo
{
	private const int MAXMANA = 300; //max MP that player can have
	private const int MAXHEALTH = 100; //max HP that player can have
	private static int mana = MAXMANA; //initialize player mana
	private static int health = MAXHEALTH; //initialize player health
	private static Spell currentSpell = null; 
	private static int powerModifier = 1;
	private static int defenseModifier = 1;
	private static int speedModifier = 1;
	private static float grabModifier = 1;
	private static PState playerState = PState.inmenus; 
	private static bool chargeEnabled = false;
	private static float slowDur;
	private static float stunDur;
	private static bool stunned;
	private static bool slowed;

	//umm....get the player's current health
	public static int getHealth()
	{
		return health; 
	}

	public static int getMaxHealth() {
		return MAXHEALTH;
	}

	//set player's health to a specific value like if the level was restarted
	public static void setHealth(int setToWhat)
	{
		health = setToWhat;
		if (health > MAXHEALTH)
		{
			health = MAXHEALTH;
		}
	}

	//get player's current MP
	public static int getMana()
	{
		return mana;
	}

	public static int getMaxMana() {
		return MAXMANA;
	}

	//set player's MP to a specific value like if the level was restarted
	public static void setMana(int setToWhat){
		mana += setToWhat;
		if (mana > MAXMANA)
		{
			mana = MAXMANA;
		}
	}

	//used maybe after player dies or a full restore pickup is dropped to reset the players stuff
	public static void resetPlayer()
	{
		mana = MAXMANA;
		health = MAXHEALTH;
	}

	/*used to increase or decreased player's health. 
	  positive number for increase
	  negative number for decrease */
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
			//Application.LoadLevel(Application.loadedLevel);
			//TODO: play death animation
			SetState(PState.dead);
		}
	}
	/*used to increase or decreased player's mana. 
	  positive number for increase
	  negative number for decrease */
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

	//For GUI display
	public static Spell getCurrSpell()
	{
		return currentSpell;
	}

	//To be able to update GUI
	public static void setCurrSpell(Spell s)
	{
		currentSpell = s;
	}

	//I think we can use this to make sure anymodifiers on the player disappear when they die
	public static void ResetStatModifiers()
	{
		powerModifier = 1;
		speedModifier = 1;
		defenseModifier = 1;
	}

	//used for enemy damage taken calculation
	public static int GetPowerModifier()
	{
		return powerModifier;
	}

	//used for player damage taken calculation
	public static int GetDefenseModifier()
	{
		return defenseModifier;
	}

	//used for movement distance calculation
	public static int GetSpeedModifier()
	{
		return speedModifier;
	}

	public static float GetGrabModifier()
	{
		return grabModifier;
	}

	public static void setGrabModifier(float modifier) {
		grabModifier = modifier;
	}

	//called by corresponding pickup item
	public static void PowerUp()
	{
		powerModifier = 2;
		GameObject.FindGameObjectWithTag("GameController").SendMessage("InvokePowerReset");
	}

	//called when corresponding pickup item should expire
	public static void PowerDown()
	{
		powerModifier = 1;
	}

	//called by corresponding pickup item
	public static void SpeedUp()
	{
		speedModifier = 2;
		GameObject.FindGameObjectWithTag("GameController").SendMessage("InvokeSpeedReset");
	}

	//called when corresponding pickup item should expir
	public static void SpeedDown()
	{
		speedModifier = 1;
	}

	//called by corresponding pickup item
	public static void InvulnUp()
	{
		defenseModifier = 0;
		GameObject.FindGameObjectWithTag("GameController").SendMessage("InvokeInvulnReset");
	}

	//called when corresponding pickup item should expir
	public static void InvulnDown()
	{
		defenseModifier = 1;
	}

	public static PState GetState()
	{
		return playerState;
	}

	public static void SetState(PState stateToSet)
	{
		playerState = stateToSet;
	}

	public static void SetCanCharge(bool setToWhat)
	{
		chargeEnabled = setToWhat;
	}

	public static bool CanCharge()
	{
		return chargeEnabled;
	}

	public static float getStunDur() {
		return stunDur;
	}
	public static float getSlowDur() {
		return slowDur;
	}

	public static void setStunDur(float duration) {
		stunDur = duration;
	}
	public static void setSlowDur(float duration) {
		slowDur = duration;
	}

	public static bool getStun() {
		return stunned;
	}
	public static bool getSlow() {
		return slowed;
	}
	
	public static void setStun(bool stun) {
		stunned = stun;
	}
	public static void setSlow(bool slow) {
		slowed = slow;
	}

}
