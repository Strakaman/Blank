using UnityEngine;
using System.Collections;

public class NPCHealer : NPCScript {

	// Use this for initialization
	
	public override void FollowUps()
	{
		//should be able to heal player as many times as they want
		PlayerInfo.resetPlayer();
		Utilities.TellPlayer("Health and Mana Restored!");
	}
}
