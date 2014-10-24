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
			GUI.Box(new Rect(Screen.width /2 - 100,Screen.height /2 - 100,250,175), "Game Paused");
			if(GUI.Button(new Rect(Screen.width /2 - 100,Screen.height /2 - 75,250,50), "Resume")){
				paused = togglePause();
			}
			if (GUI.Button (new Rect (Screen.width /2 - 100,Screen.height /2-25,250,50), "Quit to Main Menu")){
				Application.LoadLevel("Main Menu");
				Destroy(GameObject.FindGameObjectWithTag("GameManager"));
				paused = togglePause();
			}
			if (GUI.Button (new Rect (Screen.width /2 - 100,Screen.height /2 + 25,250,50), "Quit Game")){
				paused = togglePause();
				Application.Quit();
			}
		}
	}
}
