using UnityEngine;
using System.Collections;

public class NPCDoorUnlocker : NPCScript {
	bool followUpsCompleted = false;
	// Use this for initialization
	
	public override void FollowUps()
	{
		//should trigger true state in all children
		//only really need to do it once
		if (!followUpsCompleted)
		{
			Utilities.setStatusInChildren(transform,true);
			followUpsCompleted = true;
		}
	}
}
