//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;

public static class CheckPointState
{
	public static int CPhealth;
	public static int CPmana;
	public static int CPlevel= -3; //guarentees that the level does not exist, so gamemanager will know to set state
	public static bool[] CPspellBools;
	public static bool CPchargeEnabled;

	public static void UpdateState(int Phealth, int PMana, int Plevel, bool[] PspellBools, bool PchargeEnabled)
	{
		CPhealth = Phealth;
		CPmana = PMana;
		CPlevel = Plevel;
		CPspellBools = PspellBools;
		CPchargeEnabled = PchargeEnabled;
	}
}

