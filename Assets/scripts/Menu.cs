using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	public GameObject[] levelButtons;
	public GameObject quitButton, alphaHead, betaHead, blackScreen;
	public Animator anim;
	private int arrayPos;
	private bool levelSelected= false;
	void Start()
	{
		//Debug.Log(alphaHead.transform.position);
		blackScreen.SetActive(true);
		int levelReached = PlayerPrefs.GetInt("levelReached", 0); //Max level reached
		Debug.Log(levelReached);
		for(int i = 0; i < levelButtons.Length; i++)
		{
			if(i > levelReached) //If the level hasn't been reached, then don't show it
				levelButtons[i].GetComponent<Selectable>().interactable = false;
			
		}
	}

	void Update()
	{
		//Debug.Log(alphaHead.transform.position);
		if (Input.GetKeyDown(KeyCode.Escape))	// ESC to quit game
		{
			Application.Quit();
		}

		if (Input.GetKeyDown(KeyCode.U)) //Press U to unlock level by level
		{
			if(arrayPos >= levelButtons.Length -1)
			{
				arrayPos = 0;
			}
			else
			{
				arrayPos +=1;
			}
			levelButtons[arrayPos].GetComponent<Selectable>().interactable = true;

		}
		if(levelSelected)
		{
			for(int i = 0; i < levelButtons.Length; i++)
			{
				levelButtons[i].SetActive(false);		
				quitButton.SetActive(false);		
			}
			StartCoroutine(MoveFaces());
		}
	}

	public void Exit()
	{
		Application.Quit();
	}

	/*
	public void StartTutorial()
	{
		SceneManager.LoadScene("TutorialScene");
	}

	public void StartL1()
	{
		SceneManager.LoadScene("FirstPlayableScene");
	}

	public void StartFans()
	{
		SceneManager.LoadScene("FanPuzzles");
	}

	public void StartLasers()
	{
		SceneManager.LoadScene("LaserGatePuzzles");
	}
	*/
	public void SelectLevel(string levelName)
	{
		levelSelected = true;
		//Wait and then scene load.
		StartCoroutine(LoadLevel(levelName));
	}

	private IEnumerator LoadLevel(string levelName)
	{
		yield return new WaitForSeconds(2f);
		anim.SetBool("Fade", true);
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(levelName);
	}

	private IEnumerator MoveFaces()
	{
		yield return new WaitForSeconds(0.3f);
        Debug.Log("alpha - " + alphaHead.transform.position);
        Debug.Log("beta - " + betaHead.transform.position);
        alphaHead.transform.position = Vector3.MoveTowards(alphaHead.transform.position, new Vector3(0, alphaHead.transform.position.y, alphaHead.transform.position.z), 2.5f);
		betaHead.transform.position = Vector3.MoveTowards(betaHead.transform.position, new Vector3(0, betaHead.transform.position.y, betaHead.transform.position.z), 2.5f);
	}
	 
}
