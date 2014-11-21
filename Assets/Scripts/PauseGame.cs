using UnityEngine;
using System.Collections;


public class PauseGame : MonoBehaviour {
	 bool paused = false;

	void Update()
	{
		if (Input.GetButtonDown ("pause"))
		{
			if (paused)
			{
				ResumeTheGame();
				PlayPauseSound();
			}
			else if (PlayerInfo.GetState().Equals(PState.normal)) //players can only pause when not dead, etc.
			{
				PauseTheGame();
				PlayPauseSound();

			}
		}
		if (paused) 
		{
			Time.timeScale = 0f;
			//guiTexture.enabled = true;
			//Debug.Log("Game is Paused");
		}
		else
		{
			Time.timeScale = 1f;
			//guiTexture.enabled = false;
			//Debug.Log("Game is Resumed");
		}
	}

	void PauseTheGame()
	{
		paused = true;
		PlayerInfo.SetState(PState.inmenus);
	}

	void ResumeTheGame()
	{
		paused = false;
		PlayerInfo.SetState(PState.normal);
	}

	void PlayPauseSound()
	{
		if (audio)
		{
			audio.Play ();
		}
	}

	void OnGUI() //draw pause menu with player options
	{
		if (paused)
		{
			GUI.Box(new Rect(Screen.width /2 - 100,Screen.height /2 - 100,250,175), "Game Paused");
			if(GUI.Button(new Rect(Screen.width /2 - 100,Screen.height /2 - 75,250,50), "Resume")){
				ResumeTheGame();
			}
			if (GUI.Button (new Rect (Screen.width /2 - 100,Screen.height /2-25,250,50), "Quit to Main Menu")){
				Application.LoadLevel("Main Menu");
				ResumeTheGame();
				PlayerInfo.SetState(PState.inmenus); //still set player as inmenus since main menu is also a menu
			}
			if (GUI.Button (new Rect (Screen.width /2 - 100,Screen.height /2 + 25,250,50), "Quit Game")){
				ResumeTheGame();
				Application.Quit();
			}
		}
	}
}
