using UnityEngine;
using System.Collections;

public class EndScript : MonoBehaviour {
		
		public Texture backgroundTexture;
		//GUILayoutOption[] options = new GUILayoutOption[8];
		public GUIStyle quitGameButton;
		public GUIStyle thanksForPlaying;
		

		void Start()
	{
		GameObject yo = GameObject.FindGameObjectWithTag("GameController");
		if (yo != null) {
		Destroy (yo);
	}
	}
		void OnGUI ()
		{
			
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroundTexture);
		GUI.Label(new Rect (Screen.width * .475f, Screen.height * .45f, Screen.width * .4f, Screen.height * .1f),"THANK YOU FOR PLAYING!",thanksForPlaying);
				//(new Rect (Screen.width * .5f, Screen.height * .55f, Screen.width * .5f, Screen.height * .1f)
				if (GUI.Button (new Rect (Screen.width * .575f, Screen.height * .8f, Screen.width * .2f, Screen.height * .1f), "Quit Game",quitGameButton)) {
					//print ("Clicked End Game");
			Application.Quit ();
				}

			
			}
		}
	


	

	

