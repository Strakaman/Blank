using UnityEngine;
using System.Collections;

public class DeathGUI : MonoBehaviour {

	bool showGUI = false;
	
	void Update()
	{
		if ((PlayerInfo.GetState().Equals(PState.dead)) && (!showGUI))
		{
			TriggerDeathMenu();
		}
	}


	void TriggerDeathMenu()
	{
		showGUI = true;
		Time.timeScale = 0f;
	}

	void ResolveDeathMenu()
	{
		showGUI = false;
		Time.timeScale = 1f;
		PlayerInfo.SetState(PState.normal);
	}
	
	void OnGUI() //draw pause menu with player options
	{
		if (showGUI)
		{
			GUI.Box(new Rect(Screen.width /2 - 100,Screen.height /2 - 100,250,175), "You Died!");
			if(GUI.Button(new Rect(Screen.width /2 - 100,Screen.height /2 - 75,250,50), "Retry Level")){
				ResolveDeathMenu ();
				Application.LoadLevel(Application.loadedLevel);
			}
			if (GUI.Button (new Rect (Screen.width /2 - 100,Screen.height /2-25,250,50), "Rage Quit to Main Menu")){
				ResolveDeathMenu();
				Application.LoadLevel("Main Menu");
				PlayerInfo.SetState(PState.inmenus); //still set player as inmenus since main menu is also a menu
			}
			if (GUI.Button (new Rect (Screen.width /2 - 100,Screen.height /2 + 25,250,50), "Rage Quit Game")){
				ResolveDeathMenu();
				Application.Quit();
			}
		}
	}
}
