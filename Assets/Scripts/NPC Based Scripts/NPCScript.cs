using UnityEngine;
using System.Collections;

public class NPCScript : Interactable
{
	//public string[] talkLines;
	public GUIText talkTextGUI;
	public GUITexture textBoxTexture;
	public Texture2D dialogTexture;
	public int textScrollSpeed;
	public int edgeMarginPercentage;
	private bool talking;
	//private bool toggleGUI; //used for animating mouths which was removed
	private bool textIsScrolling;
	private PlayerControllerScript playerScript;
	private int currentLine;
	public Texture2D npcImage;
	public TalkLine[] dialogueLines;
	public Texture2D[] npcImages;
	int randomImageIndex;
	public AudioClip[] GreetingSounds;
	string displayText;
	public GUIStyle oppaFontStyle = new GUIStyle();
	public Texture2D ritaImage;
	private Texture2D textureToDraw;
	private Rect drawTangle;
	void Start ()
	{
			textBoxTexture.enabled = false;
		talkTextGUI.enabled = false;
			int edgeMargin = (Screen.width / 100) * edgeMarginPercentage;
			//Vector2 pixel = new Vector2 (edgeMargin, edgeMargin);
			talkTextGUI.pixelOffset = new Vector2 (edgeMargin, edgeMargin);
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
		/* switched order of statements so that check to see if the line is finished comes before 
		 * check to see if text is scrolling, makes code work so oh well
		 */
			if (talking) {
				if (Input.GetButtonDown ("Interact")) {
						if (talkTextGUI.text.Equals (dialogueLines[currentLine].Sentence)) {
							//display next line
							textIsScrolling = false;
							if (currentLine < dialogueLines.Length - 1) { //player not on last line yet
								currentLine++; 
								updateDrawImage(); //update what pic to draw in case npc changed
								//talkTextGUI.text = talkLines[currentLine]; //STATIC
								StartCoroutine (startScrolling ());
							} else {  //player just finished reading the last line
								currentLine = 0;
								talkTextGUI.text = "";
								updateNPC (false); //sets all the booleans to display
								FollowUps (); //trigger follow ups in any subclasses, maybe replenish health or spawn an item or something
								PlayerInfo.SetState(PState.normal); //set player back to normal state
								}
							} else if (textIsScrolling) {
								//display full line
								talkTextGUI.text = dialogueLines [currentLine].Sentence;
								textIsScrolling = false;
							}
				}
				//auto skip to the last line if start is pressed
				if(Input.GetButtonDown("pause")) {
					currentLine = dialogueLines.Length - 1;
					updateDrawImage(); //update what pic to draw in case npc changed
					talkTextGUI.text = dialogueLines[currentLine].Sentence;
					textIsScrolling = false;
				}
			}
	}

	IEnumerator startScrolling ()
	{
			textIsScrolling = true;
			int startLine = currentLine;
			displayText = "";
			for (int i = 0; i < dialogueLines[currentLine].Sentence.Length; i++) {
					if (textIsScrolling && currentLine == startLine) {
							displayText += dialogueLines [currentLine].Sentence [i];
							talkTextGUI.text = displayText;
							yield return new WaitForSeconds (1 / textScrollSpeed);
					} else {
							yield return true;
					}
			}
			//textIsScrolling = false;
	}

	public void updateNPC (bool isNPCactive)
	{
			talking = isNPCactive;
			//toggleGUI = isNPCactive;
			//textBoxTexture.enabled = isNPCactive;
			playerScript.enabled = !isNPCactive;
	}

	public override void interact (GameObject player)
	{
			//ensures text and pic elements are in the right position
			if (dialogueLines.Length == 0) {return;}
			talkTextGUI.transform.position = new Vector3 (0, -.12f, talkTextGUI.transform.position.z);
			textBoxTexture.transform.position = new Vector3 (0.3198967f, 0.07225594f, textBoxTexture.transform.position.z);
			transform.parent.transform.position = new Vector3 (0, 0, -10);
			player.rigidbody2D.velocity = new Vector2 (0, 0);
			playerScript = player.GetComponent<PlayerControllerScript> ();
			PlayerInfo.SetState(PState.talking); //to prevent pausing and any other stuff to come
			updateNPC (true);
			currentLine = 0;
			randomImageIndex = Random.Range(0,npcImages.Length);
			updateDrawImage(); //update what pic to draw in case npc changed
			PlayRandomGreeting();
			//talkTextGUI.text = talkLines[currentLine];
			StartCoroutine (startScrolling ());
	}

	void OnGUI()
	{
		if (talking) //scale image to screen size
		{
			GUI.DrawTexture(drawTangle,textureToDraw);
			GUI.DrawTexture(new Rect(-Screen.width/2.5f,Screen.height/1.15f,Screen.width*1.5f,(Screen.height/12)+40),dialogTexture);
			GUI.Label(new Rect(30,Screen.height/1.11f,Screen.width*.85f,(Screen.height/12)+40),talkTextGUI.text,oppaFontStyle);
			//talkTextGUI.pixelOffset = new Vector2((Screen.width*20)/818, (Screen.height*150)/825) ;
			//textBoxTexture.pixelInset = new Rect(-Screen.width/2,-40,Screen.width*1.5f,(Screen.height/12)+40);
			//GUI.DrawTexture(new Rect(-50 , Screen.height/3, Screen.width/3.5f, Screen.height/2),npcImage);
		}
	}


	public virtual void FollowUps()
	{

	}

	void updateDrawImage()
	{
		Talker currTalker = dialogueLines[currentLine].thingTalking;
		if (currTalker.Equals(Talker.NPC))
		{
			textureToDraw = npcImages[randomImageIndex];
			drawTangle = new Rect(Screen.width - Screen.width/3.5f - 5, Screen.height/2.5f, Screen.width/3.5f, Screen.height/2);
		}
		else if (currTalker.Equals(Talker.Player))
		{
			textureToDraw = ritaImage;
			drawTangle = new Rect(0, Screen.height/3.0f, Screen.width/3.5f, Screen.height);
		}
	}
	void PlayRandomGreeting()
	{
		if ((!audio) || (GreetingSounds.Length < 1)) {
			return;
		}
		int randomNum = Random.Range(0,GreetingSounds.Length);
		audio.clip = GreetingSounds[randomNum];
		audio.Play();
	}
}
