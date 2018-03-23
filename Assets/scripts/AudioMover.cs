using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMover : MonoBehaviour {

	public GameObject AlphaPosition, BetaPosition;
	private AudioSource AudioSound;
	// Use this for initialization
	private float delay;

	void Start()
	{
		delay = 2;
		AudioSound = this.GetComponent<AudioSource>();
	}
	void Update()
	{
		//Every 2 seconds check if alpha or beta is in, if not turn audio of.
	}
	void OnTriggerEnter(Collider col)
	{
		//Debug.Log(col.tag);
		if(col.tag == "Alpha" ||col.tag == "Beta")
		{
			AudioSound.Play();
		}
	}

	void OnTriggerExit()
	{
		AudioSound.Stop();
	}
}
