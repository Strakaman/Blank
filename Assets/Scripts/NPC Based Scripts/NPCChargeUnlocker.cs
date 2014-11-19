using UnityEngine;
using System.Collections;


public class NPCChargeUnlocker : NPCScript {

	private bool followUpsCompleted = false;

	public override void FollowUps()
	{
		if (!followUpsCompleted)
		{
			Utilities.TellPlayer("Spells can now be charged!");
			PlayerInfo.SetCanCharge(true);
			followUpsCompleted = true;
		}
	}
}
