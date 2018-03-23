using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeconstructBomb : MonoBehaviour {

	private void Start()
	{
		Destroy(gameObject, 5f);
	}

	private void OnCollisionEnter(Collision other)
	{
		Debug.Log(other.gameObject.name);
		Destroy(gameObject, 0.01f);
	}
}
