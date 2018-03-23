using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundAfterTime : MonoBehaviour {

    public float delayTime;
    private AudioSource source;

	void Start () {
        source = GetComponent<AudioSource>();
        source.PlayDelayed(delayTime);
	}
	
}
