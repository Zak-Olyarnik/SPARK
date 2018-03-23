using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour {

	private void OnTriggerEnter(Collider coll)
	{
        if (coll.tag == "Bomb")
		{
            Destroy(coll.gameObject);
			Destroy(gameObject);
		}
	}
}
