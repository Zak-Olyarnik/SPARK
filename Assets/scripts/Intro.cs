using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    private AudioSource source;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        Invoke("LoadLevel", source.clip.length + .2f);
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene("Tutorial");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            LoadLevel();   
        }
    }
}
