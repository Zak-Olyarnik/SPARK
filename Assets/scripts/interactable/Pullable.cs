using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pullable : MonoBehaviour
{

	//This script is used as a check.
	public Vector3 initialLocation, finalLocationLeft, finalLocationRight; //Change this to something else
	public float speed, timer; //Timer has to be greater than 0, otherwise it will NOT work
	public bool movingPlatform;

	private float initialPullingSpeed, time;
	private bool pulled = false, couroutine = true;
	public bool freezeX, freezeZ;
	private Vector3 currPos;

	void Start()
	{
		initialLocation += this.transform.position;
		finalLocationLeft += this.transform.position;
		finalLocationRight += this.transform.position;
		time = Time.time;
	}


	void Update()
	{
		if (movingPlatform)
		{
			//Debug.Log("BEING PULLED? : " + pulled);
			if (this.transform.position != initialLocation && !pulled)
			{
				//Debug.Log("Moving to origin");
				this.transform.position = Vector3.MoveTowards(this.transform.position, initialLocation, speed);
			}
			if (this.transform.position == currPos && time > timer)
			{
				pulled = false;
			}
			//Debug.Log(time);
			if (time > timer) //Why? because if it's 0, then timer will always MOVE.
			{
				currPos = this.transform.position;
				time = 0;
			}

			time += UnityEngine.Time.deltaTime;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (movingPlatform)
		{
			if (col.name == "Shooting Spot")
			{
				//Debug.Log("POTATO");
				pulled = true;
			}
		}
	}

	IEnumerator ResetPosition()
	{
		yield return new WaitForSeconds(timer);
		//Debug.Log("Curr pos");
		currPos = this.transform.position;
	}


	/*
	void OnTriggerExit(Collider col)
	{
		if(pulled)
		{
			Debug.Log("EXIT");
			pulled = false;
		}
	}
 */
	/*
		void OnTriggerStay(Collider col)
	{
		if(col.name == "Shooting Spot" && maxLocation.magnitude != 0) //Touching ElectroMagnet and we want it to move somewhere
		{
			Debug.Log(Vector3.Distance(this.transform.position, maxLocation));
			if(Vector3.Distance(this.transform.position, maxLocation) <= 0.5f && freezeY && freezeZ) //If this objects pos is the same as max location
			{//Make speed to 0 so that they both don't move.
				Debug.Log("potato");
				speed = 0;
			}
		}

		//Call resetspeeds after 2 seconds of leaving the stuff.
	}

	void OnTriggerExit(Collider col)
	{
		speed = initialSpeed;
		col.transform.parent.GetComponent<ElectroMagnet>().pullingSpeed = initialPullingSpeed;
		Debug.Log("exit col.get " + col.transform.parent.GetComponent<ElectroMagnet>().pullingSpeed);
		Debug.Log("speed " + speed);
	}

	void ResetSpeeds(Collider col)
	{
		speed = initialSpeed;
	}
	  */

}
