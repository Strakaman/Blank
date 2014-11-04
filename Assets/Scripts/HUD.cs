using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	public Texture2D redSpell;
	public Texture2D blueSpell;
	public Texture2D yellowSpell;
	public Texture2D orangeSpell;
	public Texture2D whiteSpell;
	public Texture2D hpBar;
	public Texture2D mpBar;
	public Texture2D hp;
	public Texture2D mp;
	public Texture2D hudBG;
	//public Texture2D messageBar;
	public Rect hpPosition;
	public Rect mpPosition;
	public Rect hpContainerPosition;
	public Rect mpContainerPosition;
	public Rect spellPosition;
	public Rect hudBGPosition;
	//public Rect messageBarPosition;
	//public Rect messagePosition;
	//public string message;
	private float health;
	private float healthScale;
	private float mana;
	private float manaScale;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		health = PlayerInfo.getHealth ();
		mana = PlayerInfo.getMana ();
		healthScale = health / 100;
		manaScale = mana / 300;
	}

	void barUpdate(Rect pos, Texture2D tex, float scale) {
		GUI.DrawTexture (new Rect(Screen.width * pos.x, Screen.height * pos.y, Screen.width * (pos.width * scale), Screen.height * pos.height), tex);
	}

	void OnGUI() {
		/*
		GUI.skin = theSkin;
		GUI.Label (new Rect (50, 25, 100, 100), "Health: " + PlayerInfo.getHealth());
		GUI.Label (new Rect (Screen.width - 100, 25, 100, 100), "Mana: " + PlayerInfo.getMana());
		GUI.Label (new Rect (50, 50, 100, 100), "Current Spell: " + PlayerInfo.getCurrSpell().getName());
		 */
		if(PlayerInfo.GetState().Equals(PState.inmenus)) return; //dont draw HUD if player is in menu since it looks weird
		drawBar (hudBGPosition, hudBG);
		/*message = "Out of mana, kupo!"; 
		drawBar (messageBarPosition, messageBar);
		GUI.Label (getScreenRect (messagePosition), message);
		* deprecated code
		*/
		drawBar (hpContainerPosition, hpBar);
		drawBar (mpContainerPosition, mpBar);
		barUpdate (hpPosition, hp, healthScale);
		barUpdate (mpPosition, mp, manaScale);
	//	Debug.Log (PlayerInfo.getCurrSpell ().GetType());
		if (PlayerInfo.getCurrSpell ().GetType().ToString() == "RedSpell") {
			drawSpell (redSpell);
		}
		if (PlayerInfo.getCurrSpell ().GetType().ToString() == "BlueSpell") {
			drawSpell (blueSpell);
		}
		if (PlayerInfo.getCurrSpell ().GetType().ToString() == "YellowSpell") {
			drawSpell (yellowSpell);
		}
		if (PlayerInfo.getCurrSpell ().GetType().ToString() == "OrangeSpell") {
			drawSpell (orangeSpell);
		}
		if (PlayerInfo.getCurrSpell ().GetType().ToString() == "WhiteSpell") {
			drawSpell (whiteSpell);
		}
	}

	void drawBar(Rect pos, Texture2D bar) {
		GUI.DrawTexture (getScreenRect(pos), bar);

	}
	

	void drawSpell(Texture2D spell) {
		GUI.DrawTexture(getScreenRect(spellPosition), spell);
	}

	Rect getScreenRect(Rect pos) {
		return new Rect (Screen.width * pos.x, Screen.height * pos.y, Screen.width * pos.width, Screen.height * pos.height);
	}
}
