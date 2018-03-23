using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script returns the object to its original location if it hits something that we declared.
public class ReturnObject : MonoBehaviour {

	public GameObject[] walls;
	private Vector3 initialPosition;
	private Quaternion initialRotation;
	void Start () {
		initialPosition = this.transform.position;
		initialRotation = this.transform.rotation;
	}
	
	void OnTriggerEnter(Collider col)
	{
		foreach(GameObject w in walls)
		{
			if(col.gameObject == w)
			{
				this.transform.rotation = initialRotation;
				this.transform.position = initialPosition;
			}
		}	
	}

}
