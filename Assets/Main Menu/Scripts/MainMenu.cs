using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Texture backgroundTexture;

	public float guiPlacementY;

	void OnGUI(){

		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),backgroundTexture);

		if (GUI.Button (new Rect(Screen.width * .5f, Screen.height * .4f, Screen.width * .5f, Screen.height * .1f), "New Game")){
			//print ("Clicked Play Game");
			Application.LoadLevel(Application.loadedLevel+1); //theoretically this should be the first level
		}

		if (GUI.Button (new Rect (Screen.width * .5f, Screen.height * .55f, Screen.width * .5f, Screen.height * .1f), "End Game")) {
						//print ("Clicked End Game");
			Application.Quit();
	
		}
	}
}
