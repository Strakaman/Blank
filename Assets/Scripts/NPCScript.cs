using UnityEngine;
using System.Collections;

public class NPCScript : Interactable
{
	public string[] talkLines;
	public GUIText talkTextGUI;
	public GUITexture textBoxTexture;
	public int textScrollSpeed;
	public int edgeMarginPercentage;
	public GameObject omari;
	private bool talking;
	private bool toggleGUI;
	private bool textIsScrolling;
	private PlayerControllerScript playerScript;
	private int currentLine;
	private Vector3 omariStuffPosition;

	void Start ()
	{
			omari.renderer.enabled = false;
			textBoxTexture.enabled = false;
			int edgeMargin = (Screen.width / 100) * edgeMarginPercentage;
			//Vector2 pixel = new Vector2 (edgeMargin, edgeMargin);
			talkTextGUI.pixelOffset = new Vector2 (edgeMargin, edgeMargin);
			float x = transform.position.x + 40;
			/*foreach (Transform child in transform)
	{
		Debug.Log(x + name);
		child.position = new Vector3(x, transform.position.y, child.position.z);
		//child.position = new Vector3(textStuffPosition.x +40, textStuffPosition.y, child.position.z);
	}*/
	}

	// Update is called once per frame
	void Update ()
	{
		if (talking) {
			if (Input.GetButtonDown ("Interact")) {
				if (textIsScrolling) {
						//display full line
						talkTextGUI.text = talkLines [currentLine];
						textIsScrolling = false;
				} else {
						//display next line
					if (currentLine < talkLines.Length - 1) {
							currentLine++;
							//talkTextGUI.text = talkLines[currentLine]; //STATIC
							StartCoroutine (startScrolling ());
					} else {
							currentLine = 0;
							talkTextGUI.text = "";
							updateNPC (false); //sets all the booleans to display 
					}
				}
			}
		}
	}

	IEnumerator startScrolling ()
	{
			textIsScrolling = true;
			int startLine = currentLine;
			string displayText = "";
			for (int i = 0; i < talkLines[currentLine].Length; i++) {
					if (textIsScrolling && currentLine == startLine) {
							displayText += talkLines [currentLine] [i];
							talkTextGUI.text = displayText;
							yield return new WaitForSeconds (1 / textScrollSpeed);
					} else {
							yield return true;
					}
			}
			textIsScrolling = false;
	}

	public void updateNPC (bool isNPCactive)
	{
			talking = isNPCactive;
			toggleGUI = isNPCactive;
			textBoxTexture.enabled = isNPCactive;
			omari.renderer.enabled = isNPCactive;
			playerScript.enabled = !isNPCactive;
	}

	public override void interact (GameObject player)
	{
			omariStuffPosition = GameObject.FindGameObjectWithTag ("MainCamera").transform.position;
			omari.transform.position = new Vector3 (omariStuffPosition.x - 7, omariStuffPosition.y, omari.transform.position.z);
			talkTextGUI.transform.position = new Vector3 (0, -.12f, talkTextGUI.transform.position.z);
			textBoxTexture.transform.position = new Vector3 (0.3198967f, 0.07225594f, textBoxTexture.transform.position.z);
			transform.parent.transform.position = new Vector3 (0, 0, -10);
			player.rigidbody2D.velocity = new Vector2 (0, 0);
			playerScript = player.GetComponent<PlayerControllerScript> ();
			updateNPC (true);
			currentLine = 0;
			//talkTextGUI.text = talkLines[currentLine];
			StartCoroutine (startScrolling ());
	}

}
