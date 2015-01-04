using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreditsDesu : MonoBehaviour {

	Dictionary<string, string[]> credits; //holds credit info
	public Texture2D background; 
	List<string> keys; 
	string[] currNames;
	int index;
	public GUIStyle creditWordStyle; //font styles for display set in insppector
	public GUIStyle creditWord;
	public GUIStyle creditWord2;
	void Start () {
		credits = new Dictionary<string, string[]>()
		{
			{ "Blank Design Team" , new string[]{"Peter Pham", "Omari Straker", "Ceci Tran", "Jacob Day", "Wilson Loi", "Dennis Hsu"}},
			{ "Director" , new string[]{"Peter Pham"}},
			{ "Lead Programmer" , new string[]{"Omari Straker"}},
			{ "Additional Thanks" , new string[]{"SJSU Game Dev Club","Megaman X4 Sprites"}},
		};
		keys = new List<string>(credits.Keys); //store keys in a seperate array for easier reference
		index = 0; //use index to keep track of which part of credits we are displaying
		currNames = credits[keys[index]];
		StartCoroutine(UpdateDisplay()); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		/*
		 * Draws the texture and text to screen based on the current value of index
		 * since dictionary stores a string array, loop through array to print full credits per section
		 */ 
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),background);
		GUI.Label(new Rect (Screen.width/2-50,10,100,50),"Credits",creditWord);
		string currKey = keys[index];
		GUI.Label(new Rect (Screen.width/2-50,60,100,50),currKey,creditWord2);
		//string[] currNames = credits[currKey];
		currNames = credits[currKey];
		for (int i = 0; i < currNames.Length; i++) {
			GUI.Label(new Rect (Screen.width/2-50,100+i*25,100,50),currNames[i],creditWordStyle);
		}
	}
	/**
	 * Since we draw the credit info based on the value of the index counter,
	 * we increment the value of index every few seconds, thus promptng a redraw
	 * delay between updates dependent on length of string array to be drawn
	 */ 
	IEnumerator UpdateDisplay(){
		index = 0;
		while(index < keys.Count) {
			yield return new WaitForSeconds(1.0f + currNames.Length*.65f);
			index++;
		}
		index = keys.Count - 1; //at this point the while loop is over, just show the last credit
		Invoke("GoToMainMenu",1); //after credits finish go back to main menu
	}

	void GoToMainMenu()
	{
		Application.LoadLevel("Main Menu");
	}
}
