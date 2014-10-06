using UnityEngine;
using System.Collections;

public class NPC : Interactable {
	string delete = "Annie are you okay?";
	//string delete2 = "Are you okay? Annie";
	bool displayText = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		if (displayText)
		{
			GUILayout.BeginArea(new Rect(700,600,400,400));
			GUILayout.Label(delete);
			//if (GUI.Button(new Rect(800,700,100,100),""))
			//{
			//
			//}
			Debug.Log ("implementing guistuff");

		}
	}

	public	override void interact()
	{
		displayText = !displayText;
	}
}
