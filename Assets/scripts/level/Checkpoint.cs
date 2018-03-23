using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	// sets players' respawn to this position
	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.tag == "Alpha")
			GameController.GetInstance().alphaSpawn = transform.position;
		else if (coll.gameObject.tag == "Beta")
			GameController.GetInstance().betaSpawn = transform.position;
	}
}
