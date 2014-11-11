using UnityEngine;
using System.Collections;

public class FadingMessageBar : MonoBehaviour {
	bool stopFading = false;
	public Texture2D messageBar;
	public Rect messageBarPosition;
	public Rect messagePosition;
	Color guiTextOriginalColor;
	Color guiTextureOriginalColor;
	public GUIText datText; //set by prefab
	public GUITexture datTexture; //set by prefab

	void Start(){
		//Debug.Log("GUITEXT: " + datText.material.color.a + " GUITEXTURE: " + datTexture.color.a);
		guiTextOriginalColor = datText.material.color; //save the gui colors since we alter them when fading
		guiTextureOriginalColor = datTexture.color;
		datTexture.enabled = false; //disable by default since there is no message yet
		datText.enabled = false;
	}

	void Update(){

		/*if (Input.GetKeyDown(";")) { //for debugging
			BroadcastNewMessage("Current Time: " + Time.time);
		}*/
		//Debug.Log ("Screen Width: + " + Screen.width + " Screen Height: " + Screen.height);
	}
	void OnGUI()
	{
		/*
		 * Update message broadcaster position to match the screen resolution, probably a simpler formula but this will do
		 */ 
		datText.pixelOffset = new Vector2((Screen.width*100)/818, (Screen.height*140)/351);
		datTexture.pixelInset = new Rect((Screen.width * 50/818),(Screen.height*120)/351,(Screen.width * 400/818),(Screen.height * 40/351));
	}

	IEnumerator Fade(){
		//while (guiText.material.color.a > 0){
		int i=15; //lazy way of saying add transparency enough times to make it dissappear
		while (i > 0){
			if (stopFading) {break;} //used to stop fading from occuring, good for if we need to reshow box before fading from last box was done
			Color color = datText.material.color; //this is how you fade the color in c#
			color.a -= 0.2f;
			datText.material.color = color;
			Color color2 = datTexture.color;
			color2.a -= 0.1f;
			datTexture.color = color2;
			i--;
			//Debug.Log("GUITEXT: " + datText.material.color.a + " datTexture: " + datTexture.color.a);
			yield return new WaitForSeconds(.15f);
		}
	}
	/*
	 * used for when a new message should be popped up and 
	 * we want to set the box back to original transparency 
	 */
	void ResetTransparencies() 
	{
		datText.material.color = guiTextOriginalColor;
		datTexture.color = guiTextureOriginalColor;
	}

	/*
	 * Used to populate a new message in the message box
	 * Resets box to original settings and allows it to appear on the screen
	 * then invokes the fading to occur after a few seconds to give the user 
	 * time to read the message
	 */ 
	void BroadcastNewMessage(string datMessage)
	{
		stopFading = true;
		StopAllCoroutines();
		datText.text = datMessage;
		ResetTransparencies();
		datText.enabled = true;
		datTexture.enabled = true;
		stopFading = false;
		Invoke("TriggerFade",3f);
	}

	/*
	 * Because we don't want to trigger the fading immediately, this method call should be invoked.
	 */
	void TriggerFade() 
	{
		StartCoroutine(Fade());
	}
	
}
