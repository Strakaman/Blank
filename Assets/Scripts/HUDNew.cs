﻿using UnityEngine;
using System.Collections;

public class HUDNew : MonoBehaviour {
	public GUISkin theSkin;	
	
	var BGTexture: Texture2D;
	BGTexture.setPixel (0,0, new Color (0,0,0,0.5));
	
	//var infoFont : Font;
	
	var HPTexture: Texture2D;
	
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		GUI.skin = theSkin;
		GUI.Box (new Rect ((1.0/6)Screen.width, (1.0/12)Screen.height, (2.0/3)Screen.width, (1.0/6)Screen.height), BGTexture);
		
		GUI.Label (new Rect (50, 25, 100, 100), "Health: " + PlayerInfo.getHealth());
		//GUI.Box (new Rect ((1.0/6)Screen.width, (1.0/12)Screen.height, (2.0/3)Screen.width, (1.0/6)Screen.height), BGTexture);
		GUI.Label (new Rect (Screen.width - 100, 25, 100, 100), "Mana: " + PlayerInfo.getMana());
		GUI.Label (new Rect (50, 50, 100, 100), "Current Spell: " + PlayerInfo.getCurrSpell().getName());
	}
}
