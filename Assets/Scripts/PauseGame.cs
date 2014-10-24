using UnityEngine;
using System.Collections;


public class PauseGame : MonoBehaviour {
	 bool paused = false;

	void Update()
	{
		if (PlayerInfo.GetState() != PState.normal) {
			return;
		}
		if (Input.GetButtonDown ("pause"))
		{
			paused = togglePause ();

		}

		if (paused) 
		{
			Time.timeScale = 0f;
			guiTexture.enabled = true;
			//Debug.Log("Game is Paused");

		}
		else
		{
			Time.timeScale = 1f;
			guiTexture.enabled = false;
			//Debug.Log("Game is Resumed");
		}
	}

	bool togglePause()
	{
		if (paused == true) 
		{
			return false;
		}
		else
		{
			return true;
		}
				
	}

	void OnGUI()
	{
		if (paused)
		{
			//GUI.Box(Rect(Screen.width /2 - 100,Screen.height /2 - 100,250,200), "Game Paused");
			//if(GUI.Button(Rect(Screen.width /2 - 100,Screen.height /2 - 50,250,50), "Resume")){
			//	paused = togglePause();
			//}
		}
	}
}
