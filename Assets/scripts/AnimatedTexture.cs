using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class AnimatedTexture : MonoBehaviour {
	public Texture[] frames;
	public float fps=10;

	[Space(10)]
	public VideoClip movie;
	private VideoPlayer vp;

	// Use this for initialization
	void Start () {
		if(movie) {
			/*
			Material mat=GetComponent<Renderer>().material;
			mat.mainTexture=movie;
			((MovieTexture)mat.mainTexture).Play();
			/*/
			//StartCoroutine(playVideo());
			//*/
		}
			
	}

	IEnumerator playVideo() {
		vp=GetComponent<VideoPlayer>();
		vp.playOnAwake=false;
		vp.isLooping=true;
		vp.clip=movie;
		vp.Prepare();

		while(!vp.isPrepared) {
			yield return null;
		}

		//Debug.Log("Video Ready!");
		GetComponent<Renderer>().material.mainTexture=vp.texture;
		vp.Play();
	}
	
	// Update is called once per frame
	void Update () {
		if(!movie) {
			int index=((int)(Time.time*fps))%frames.Length;
			GetComponent<Renderer>().material.mainTexture=frames[index];
		}
	}
}
