using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EndLevel : MonoBehaviour {

	public bool AlphaCollision = false;
	public bool BetaCollision = false;
	public string nextLevelName; //The name of the next level
	public int levelToUnlock; //The level that this level unlocks 
	public Animator anim;


	void OnTriggerEnter(Collider coll)
	{
		// Checks for both players colliding and ends game if they are
		if (coll.gameObject.tag == "Alpha")
			AlphaCollision = true;
		else if (coll.gameObject.tag == "Beta")
			BetaCollision = true;

		if (AlphaCollision && BetaCollision)
		{
			if(PlayerPrefs.GetInt("levelReached") > levelToUnlock) //Make sure that you can play old levels and not lock your future levels
			{
				levelToUnlock = PlayerPrefs.GetInt("levelReached");
			}
			PlayerPrefs.SetInt("levelReached", levelToUnlock);
					//Fade to black, then go to main menu

			anim.SetBool("Fade", true);

			
			//GameController.GetInstance().EndGame();
			//yield return new WaitForSeconds(1);
			StartCoroutine(GameController.GetInstance().EndGame());
		}
	}

	void OnTriggerExit(Collider coll)
	{
		if (coll.gameObject.tag == "Alpha")
			AlphaCollision = false;
		else if (coll.gameObject.tag == "Beta")
			BetaCollision = false;
	}
}
