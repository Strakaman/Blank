using UnityEngine;
using System.Collections;

public class BlockManipulate : Interactable {
	private bool isGrabbed;


	public void Start() {

	}
	// Update is called once per frame
	public override void interact (GameObject player) {
		if (!isGrabbed) {
						PlayerInfo.SetState (PState.grabbing);
						this.transform.parent = player.transform;
						isGrabbed = true;
						PlayerInfo.setGrabModifier(0.5f);
				} else {
						PlayerInfo.SetState (PState.normal);
						this.transform.parent = null;
						isGrabbed = false;
						PlayerInfo.setGrabModifier(1f);
				}

				
	}

}
