using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.tag == "Alpha" || coll.gameObject.tag == "Beta")
		{
			coll.GetComponentInParent<PlayerController>().Die();
		}
		if(coll.gameObject.layer == 8) // Kill Buildable on Collision
		{
			coll.gameObject.GetComponent<Buildable>().Reset();
		}
		if(coll.gameObject.layer == 13) // Kill and respawn Machines.
		{
			coll.gameObject.GetComponentInParent<Deconstructable>().Reset();
		}
	}
}
