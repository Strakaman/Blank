using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	public GUISkin theSkin;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		GUI.skin = theSkin;
				GUI.Label (new Rect (50, 25, 100, 100), "Health: " + PlayerInfo.getHealth());
				GUI.Label (new Rect (Screen.width - 100, 25, 100, 100), "Mana: " + PlayerInfo.getMana());
				GUI.Label (new Rect (50, 50, 100, 100), "Current Spell: " + PlayerInfo.getCurrSpell().getName());
		}
}
