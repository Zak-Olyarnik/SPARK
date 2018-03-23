using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildable : MonoBehaviour
{
	public bool specialBool;
	public GameObject posReset;
	private Vector3 resetPos;

	private void Awake()
	{
		resetPos = transform.position;
	}

	void OnTriggerEnter(Collider coll)
	{
		// player is attempting to carry this object
		if (coll.tag == "AlphaCarry" || (coll.tag == "BetaCarry" && !gameObject.GetComponent<HeavyObject>()))
		{
			gameObject.GetComponent<Rigidbody>().isKinematic = true;
			//gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			gameObject.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
			coll.gameObject.GetComponentInParent<PlayerController>().Carry(gameObject);
		}
	}

	public void Reset()
	{
		if(specialBool)
		{
			transform.position = posReset.transform.position;
		}
		else
			transform.position = resetPos;
	}
}
