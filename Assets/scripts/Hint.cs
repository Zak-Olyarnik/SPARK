using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour {

	public string robot;
	public GameObject hint;

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == robot)
		{
			hint.SetActive(true);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == robot)
		{
			hint.SetActive(false);
		}
	}
}
