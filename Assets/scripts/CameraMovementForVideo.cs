using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementForVideo : MonoBehaviour {

	

	public GameObject pointA;
	public float speed = 5.0f;

	void Update()
	{
		this.transform.position = Vector3.MoveTowards(this.transform.position, pointA.transform.position, speed);
		this.transform.RotateAround(this.transform.position, Vector3.back, 8 * Time.deltaTime);
	}
}
