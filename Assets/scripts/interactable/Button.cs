using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
	public Activatable[] activatables;
	public AudioSource source;

	void OnTriggerEnter(Collider coll)
	{
		if (coll.tag == "AlphaInteract" || coll.tag == "BetaInteract")
		{
			source.Play();
			foreach (Activatable activatable in activatables)
			{
				activatable.ChangeState();
			}
		}
	}
}
