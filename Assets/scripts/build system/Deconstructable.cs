using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deconstructable : MonoBehaviour
{
	public Activatable[] activatables;
	public GameObject[] subComponents;	// sub-component gameObjects to instantiate on deconstruct
	public Transform instantiatePos;    // place to instantiate subcomponents
	public Socket socket;
	public AudioClip deconstructSound;	// used for machines
	public AudioSource source;			// used for sockets
	private Vector3 resetPos;

	private void Awake()
	{
		resetPos = transform.position;
	}
	void OnTriggerEnter(Collider coll)
	{
		if (coll.tag == "BetaDeconstruct")   // player is trying to Deconstruct
		{
			// instantiate subcomponents
			foreach (GameObject subComponent in subComponents)
			{
				Instantiate(subComponent, instantiatePos.position, new Quaternion(0, 0, 0, 0));
			}

			// turn on/off environment objects
			foreach (Activatable activatable in activatables)
			{
				activatable.ChangeState();
			}

			// sound
			if (deconstructSound)	// used for machines
			{
				AudioSource.PlayClipAtPoint(deconstructSound, transform.position);
			}
			else		// used for sockets
			{
				source.Play();
			}

			// destroy Beta's deconstruct bomb
			Destroy(coll.gameObject);

			// destroy simple machine or set the socket-buildable inactive
			if (tag == "SimpleMachine")
			{
				Destroy(gameObject, 0.01f);
			}
			else
			{
				Debug.Log("here");
				gameObject.SetActive(false);
				socket.isBuilt = false;
			}
		}
	}

	public void Reset()
	{
		transform.position = resetPos;
	}
}
