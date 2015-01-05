using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreditsDesu : MonoBehaviour {

	Dictionary<string, string[]> credits; //holds credit info
	public Texture2D background; 
	List<string> keys; 
	string[] currNames;
	string currKey;
	int index;
	public GUIStyle creditWordStyle; //font styles for display set in insppector
	public GUIStyle creditWord;
	public GUIStyle creditWord2;
	void Start () {
		credits = new Dictionary<string, string[]>()
		{
			{ "Blank Design Team" , new string[]{"Peter Pham", "Omari Straker", "Cecilia Tran", "Jacob Day", "Wilson Loi", "Dennis Hsu"}},
			{ "Director" , new string[]{"Peter Pham"}},
			{ "Lead Programmer" , new string[]{"Omari Straker"}},
			{ "Art Assets" , new string[]{"Cecilia Tran","Peter Pham"}},
			{ "Menus" , new string[]{"Jacob Day", "Omari Straker"}},
			{ "Level Design" , new string[]{"Peter Pham","Omari Straker"}},
			{ "Sound Effects" , new string[]{
					"Switch Click - www.kenney.nl/", "NPC Voices - www.text2speech.org/", "",
					"Freesound.org: User Uploaded Sounds","Pause Noise - Northern_Monkey",
					"Fire Spell - spookymodem","Water Spell - cusconauta","Yellow Spell - futureprobe",
					"Charged Yellow Spell - parnellij","Player Charging - javierzumer",
					"Item Pickup - crashoverride61088","","Other Sounds - Omari Straker"}},
			{ "Background Music" , new string[]{
					"Light Years - www.soundimage.org/sci-fi/", "Dark Techno City - www.soundimage.org/sci-fi/",
					"World of Automatons - www.soundimage.org/sci-fi/","Dystopic Factory - www.soundimage.org/sci-fi/",
					"Dystopic Technology - www.soundimage.org/sci-fi/", "Uncertain Future - www.soundimage.org/sci-fi/",
					"Credits Theme - Jacob Day"}},
			{ "Additional Thanks" , new string[]{"SJSU Game Dev Club","Megaman X4 Sprites"}},
		};
		keys = new List<string>(credits.Keys); //store keys in a seperate array for easier reference
		index = 0; //use index to keep track of which part of credits we are displaying
		currNames = credits[keys[index]];
		StartCoroutine(UpdateDisplay()); 
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("pause"))
		{
			GoToMainMenu();
		}
	}

	void OnGUI()
	{
		/*
		 * Draws the texture and text to screen based on the current value of index
		 * since dictionary stores a string array, loop through array to print full credits per section
		 */ 
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),background);
		GUI.Label(new Rect (Screen.width/2-50,10,100,50),"Credits",creditWord);
		//string currKey = keys[index];
		GUI.Label(new Rect (Screen.width/2-50,60,100,50),currKey,creditWord2);
		//string[] currNames = credits[currKey];
		//currNames = credits[currKey];
		for (int i = 0; i < currNames.Length; i++) {
			GUI.Label(new Rect (Screen.width/2-50,100+i*25,100,50),currNames[i],creditWordStyle);
		}
		GUI.Label(new Rect (Screen.width-215,Screen.height - 65,100,50),"Press Pause (P) for Main Menu",creditWord2);
	}
	/**
	 * Since we draw the credit info based on the value of the index counter,
	 * we increment the value of index every few seconds, thus promptng a redraw
	 * delay between updates dependent on length of string array to be drawn
	 */ 
	IEnumerator UpdateDisplay(){
		index = 0;
		while(index < keys.Count) {
			currKey = keys[index];
			currNames = credits[currKey];
			yield return new WaitForSeconds(1.0f + currNames.Length*.65f);
			//yield return new WaitForSeconds(1.0f);
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
