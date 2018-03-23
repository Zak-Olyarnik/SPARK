using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
	public Activatable[] activatables;
	public string reqObj;
	public bool isBuilt = false;
	public GameObject buildable;
	public GameObject deconstructable;
	public AudioClip buildSound;

	void OnTriggerEnter(Collider coll)
	{
		if (coll.tag == reqObj)		// player has brought the required Buildable to this location
		{
			buildable = coll.gameObject;
		}
		if (coll.tag == "AlphaConstruct")	// player is trying to Build
		{
			if(buildable)
			{
				AlphaController alpha=GameObject.FindObjectOfType<AlphaController>();
				if(alpha) {alpha.animator.primary(); }

				ConstructObj();
			}
		}
	}

	void OnTriggerExit(Collider coll)
	{
		if (coll.tag == reqObj)     // player removes Buildable from this location, clear reference
		{
			buildable = null;
		}
	}

	void ConstructObj()
	{
		isBuilt = true;
		AudioSource.PlayClipAtPoint(buildSound, transform.position);
		Destroy(buildable);

		foreach (Activatable activatable in activatables)
		{
			activatable.ChangeState();
		}

		deconstructable.SetActive(true);
	}
}
