﻿using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{

	public Texture backgroundTexture;
	public bool showYesNoPrompt = false;
	GUILayoutOption[] options = new GUILayoutOption[8];
	public float guiPlacementY;

	void Start ()
	{
		options [0] = GUILayout.Width (Screen.width * .25f);
		options [1] = GUILayout.Width (Screen.height * .25f);
		options [2] = GUILayout.Width (Screen.width * .25f);
		options [3] = GUILayout.Width (Screen.height * .25f);
		options [4] = GUILayout.Width (Screen.width * .25f);
		options [5] = GUILayout.Width (Screen.height * .25f);
		options [6] = GUILayout.Width (Screen.width * .25f);
		options [7] = GUILayout.Width (Screen.height * .25f);
	}

	void OnGUI ()
	{

			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroundTexture);
		
		if (GUI.Button (new Rect (Screen.width * .2f, Screen.height * .4f, Screen.width * .1f, Screen.height * .1f), "New Game")) {
					//print ("Clicked Play Game");
					Application.LoadLevel (Application.loadedLevel + 1); //theoretically this should be the first level
			}
//(new Rect (Screen.width * .5f, Screen.height * .55f, Screen.width * .5f, Screen.height * .1f)
		if (GUI.Button (new Rect (Screen.width * .2f, Screen.height * .6f, Screen.width * .1f, Screen.height * .1f), "End Game")) {
					//print ("Clicked End Game");
					showYesNoPrompt = true;
			}
			if (showYesNoPrompt == true) {
					GUI.Label (new Rect (Screen.width * .1f, Screen.height * .5f, Screen.width * .4f, Screen.height * .1f), "Are You sure you want to quit?");
			if (GUI.Button (new Rect (Screen.width * .4f, Screen.height * .35f, Screen.width * .1f, Screen.height * .1f), "I GUESS")) {
							Application.Quit ();
					}
					if (GUI.Button (new Rect (Screen.width * .4f, Screen.height * .5f, Screen.width * .1f, Screen.height * .1f), "NO")) {
						showYesNoPrompt = false;
					}
			}
		}
	}

