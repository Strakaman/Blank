using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{

	public Texture backgroundTexture;
	public bool showYesNoPrompt = false;
	//GUILayoutOption[] options = new GUILayoutOption[8];
	public GUIStyle oppaFontStyle;
	public GUIStyle promptStyle;

	void Start ()
	{
		/*options [0] = GUILayout.Width (Screen.width * .25f);
		options [1] = GUILayout.Width (Screen.height * .25f);
		options [2] = GUILayout.Width (Screen.width * .25f);
		options [3] = GUILayout.Width (Screen.height * .25f);
		options [4] = GUILayout.Width (Screen.width * .25f);
		options [5] = GUILayout.Width (Screen.height * .25f);
		options [6] = GUILayout.Width (Screen.width * .25f);
		options [7] = GUILayout.Width (Screen.height * .25f);*/
	}

	void OnGUI ()
	{

		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroundTexture);
		if (!showYesNoPrompt)
		{if (GUI.Button (new Rect (Screen.width * .575f, Screen.height * .4f, Screen.width * .2f, Screen.height * .1f), "New Game",oppaFontStyle)) {
					//print ("Clicked Play Game");
					Application.LoadLevel (Application.loadedLevel + 1); //theoretically this should be the first level
			}
//(new Rect (Screen.width * .5f, Screen.height * .55f, Screen.width * .5f, Screen.height * .1f)
		if (GUI.Button (new Rect (Screen.width * .575f, Screen.height * .6f, Screen.width * .2f, Screen.height * .1f), "End Game",oppaFontStyle)) {
					//print ("Clicked End Game");
					showYesNoPrompt = true;
			}
		}
			if (showYesNoPrompt == true) {
			//GUI.contentColor = Color.black; //USELESS
					GUI.Label (new Rect (Screen.width * .45f, Screen.height * .275f, Screen.width * .4f, Screen.height * .1f), "Are you sure you want to quit?",promptStyle);
			if (GUI.Button (new Rect (Screen.width * .575f, Screen.height * .4f, Screen.width * .16f, Screen.height * .1f), "I Guess",oppaFontStyle)) {
							Application.Quit ();
					}
					if (GUI.Button (new Rect (Screen.width * .575f, Screen.height * .6f, Screen.width * .16f, Screen.height * .1f), "No",oppaFontStyle)) {
						showYesNoPrompt = false;
					}
			}
		}
	}

