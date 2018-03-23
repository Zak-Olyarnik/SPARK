using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityActivator : MonoBehaviour
{
	public Activatable[] activatables;
	public bool AlphaCollision = false;
	public bool BetaCollision = false;

	void OnTriggerEnter(Collider coll)
	{
		// Checks for both players colliding and ends game if they are
		if (coll.gameObject.tag == "Alpha")
			AlphaCollision = true;
		else if (coll.gameObject.tag == "Beta")
			BetaCollision = true;

		if (AlphaCollision && BetaCollision)
		{
			foreach (Activatable activatable in activatables)
			{
				activatable.Activate();
			}
		}
	}

	void OnTriggerExit(Collider coll)
	{
		if (coll.gameObject.tag == "Alpha")
			AlphaCollision = false;
		else if (coll.gameObject.tag == "Beta")
			BetaCollision = false;

		// may need to deactivate here but for the current use case (elevator) this is a bad idea
	}
}
