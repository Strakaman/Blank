using UnityEngine;
using System.Collections;

public class NPCScript : Interactable {
	public string[] talkLines;
	public GUIText talkTextGUI;
	public GUITexture textBoxTexture;
	public Texture textTexture;
	public int textScrollSpeed;
	public int edgeMarginPercentage;
	public GameObject omari;
	private Animator omariAnimator;
	GUIStyle smallFont;
	GUIStyle largeFont;

	private bool talking;
	private bool toggleGUI;
	private bool textIsScrolling;
	private PlayerControllerScript playerScript;
	private int currentLine;

	void Start () {
		omariAnimator = (Animator)omari.GetComponent ("Animator");
		smallFont = new GUIStyle();
		largeFont = new GUIStyle();
		smallFont.fontSize = 10;
		largeFont.fontSize = 32;
		omari.renderer.enabled = false;
		textBoxTexture.enabled = false;
		int edgeMargin = (Screen.width/100) * edgeMarginPercentage;
		//Vector2 pixel = new Vector2 (edgeMargin, edgeMargin);
		talkTextGUI.pixelOffset = new Vector2 (edgeMargin, edgeMargin);

	}

	// Update is called once per frame
	void Update () {
		if (talking) {
			if (Input.GetButtonDown("Interact")) {
				if (textIsScrolling) {
					//display full line
					talkTextGUI.text = talkLines[currentLine];
					textIsScrolling = false;
				} else {
				//display next line
					if(currentLine < talkLines.Length - 1) {
						currentLine++;
						//talkTextGUI.text = talkLines[currentLine]; //STATIC
						StartCoroutine(startScrolling());
					} else {
						currentLine = 0;
						talkTextGUI.text = "";
						updateNPC(false); //sets all the booleans to display 
					}
				}
			}
		}
	}

	IEnumerator startScrolling() {
		textIsScrolling = true;
		int startLine = currentLine;
		string displayText = "";
		for (int i = 0; i < talkLines[currentLine].Length; i++) {
			if(textIsScrolling && currentLine == startLine){
				displayText += talkLines[currentLine][i];
				talkTextGUI.text = displayText;
				omariAnimator.SetBool("Talking", true);
				yield return new WaitForSeconds(1/textScrollSpeed);
			} else {
				yield return true;
			}
		}
		omariAnimator.SetBool("Talking", false);
		textIsScrolling = false;
	}
	public void updateNPC(bool isNPCactive)
	{
		talking = isNPCactive;
		toggleGUI = isNPCactive;
		textBoxTexture.enabled = isNPCactive;
		omari.renderer.enabled = isNPCactive;
		playerScript.enabled = !isNPCactive;
		playerScript.animator.enabled = !isNPCactive;
	}

	public override void interact(GameObject player)
	{
		player.rigidbody2D.velocity = new Vector2(0,0);
		playerScript = player.GetComponent<PlayerControllerScript>();
		//Debug.Log(playerScript);
		updateNPC(true);
		currentLine = 0;
		//talkTextGUI.text = talkLines[currentLine];
		StartCoroutine(startScrolling());
	}

	/*void OnCollisionEnter2D(Collision2D coll) {
		if (coll.collider.tag == "Player") {
			playerScript = coll.collider.GetComponent<PlayerControllerScript>();
			Debug.Log(playerScript);
			talking = true;
			toggleGUI = true;
			textBoxTexture.enabled = true;
			omari.renderer.enabled = true;
			currentLine = 0;
			//talkTextGUI.text = talkLines[currentLine];
			StartCoroutine(startScrolling());
			playerScript.enabled = false;
			playerScript.animator.enabled = false;
		}
	}*/

	/*void OnGUI(){
		if (toggleGUI == true) {
			GUI.DrawTexture (new Rect (0, Screen.height/1.5f, 1000, 200), textTexture);
			GUI.Label (new Rect(100, Screen.height/1.25f, 900, 150), talkLines[currentLine], largeFont);
		}
	}*/
}
