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
	//GUIStyle smallFont;
	//GUIStyle largeFont;

	private bool talking;
	private bool toggleGUI;
	private bool textIsScrolling;
	private PlayerControllerScript playerScript;
	private int currentLine;
	private Vector3 omariStuffPosition;

	void Start () {
		omariAnimator = (Animator)omari.GetComponent ("Animator");
		//smallFont = new GUIStyle();
		//largeFont = new GUIStyle();
		//smallFont.fontSize = 10;
		//largeFont.fontSize = 32;
		omari.renderer.enabled = false;
		textBoxTexture.enabled = false;
		int edgeMargin = (Screen.width/100) * edgeMarginPercentage;
		//Vector2 pixel = new Vector2 (edgeMargin, edgeMargin);
		talkTextGUI.pixelOffset = new Vector2 (edgeMargin, edgeMargin);
		//omariStuffPosition = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
		//omari.transform.position = new Vector3(omariStuffPosition.x,omariStuffPosition.y, omari.transform.position.z);
		//omari.transform.position = new Vector3(transform.position.x+10,transform.position.y, 5);
		float x = transform.position.x + 40;
		/*foreach (Transform child in transform)
		{
			Debug.Log(x + name);
			child.position = new Vector3(x, transform.position.y, child.position.z);
			//child.position = new Vector3(textStuffPosition.x +40, textStuffPosition.y, child.position.z);
		}*/
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
				//omariAnimator.SetBool("Talking", true);
				yield return new WaitForSeconds(1/textScrollSpeed);
			} else {
				yield return true;
			}
		}
		//omariAnimator.SetBool("Talking", false);
		textIsScrolling = false;
	}
	public void updateNPC(bool isNPCactive)
	{
		talking = isNPCactive;
		toggleGUI = isNPCactive;
		textBoxTexture.enabled = isNPCactive;
		omari.renderer.enabled = isNPCactive;
		playerScript.enabled = !isNPCactive;
		//playerScript.animator.enabled = !isNPCactive;
	}

	public override void interact(GameObject player)
	{
		omariStuffPosition = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
		omari.transform.position = new Vector3(omariStuffPosition.x-7,omariStuffPosition.y, omari.transform.position.z);
		//omari.transform.position = new Vector3(-7,-1, omari.transform.position.z);
		Debug.Log(omari.transform.position);
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
