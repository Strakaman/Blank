using UnityEngine;
using System.Collections;

public class BGMPlayer :MonoBehaviour
{
	public AudioClip forIntro;
	public AudioClip forCredits;
	public AudioClip forLevel1;
	public AudioClip forLevel2;
	public AudioClip forLevel3;
	public AudioClip forBosses;
	public AudioClip forDefault;

	/**
	 * Play music depending on what level was loaded
	 */ 
	void OnLevelWasLoaded(int level) {
		if (audio == null)
		{
			return; //if there is no audo component, don't do anything
		}
		string nameToCheck = Application.loadedLevelName;
		AudioClip newClip;
		if (nameToCheck.Equals(Stage.LEVEL1NAME))	{
			newClip = forLevel1;
		}
		else if (nameToCheck.Equals(Stage.LEVEL2NAME))	{
			newClip = forLevel2;
		}
		else if (nameToCheck.Equals(Stage.LEVEL3NAME))	{
			newClip = forLevel3;
		}
		else if (nameToCheck.Equals(Stage.BOSSLEVELS))	{
			newClip = forBosses;
		}
		else if (nameToCheck.Equals(Stage.THECREDITS))	{
			newClip = forCredits;
		}
		else if (nameToCheck.Equals(Stage.THEOPINTRO)) 	{
			newClip = forIntro;
		}
		else {
			newClip = forDefault;
		}
		/*this should only play the song if it's not the same song,
		makes it so that on level reload we don't start the song over
		to keep that smooth transition,	but on new level load, play the new song,
		since it should be different
		*/
		if (audio.clip != newClip) 
			{
				audio.clip = newClip;
				audio.Play();
			}
		}
	}


