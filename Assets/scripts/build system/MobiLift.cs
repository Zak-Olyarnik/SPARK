using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobiLift : MonoBehaviour {
	//Script does the following
	//Gets the minimum position that the dev allocates.
	//Gets the max pos that the dev allocates.
	//If "magical button" is clicked while on the trigger area, move possition from point min to max.
	//Return to min on exit

	public float minHeight, maxHeight;
	private float topPos, botPos;
	//public Axes.Action interact;
	public GameObject elevatorPlatform, elevatorBase;
	public float speed;
	//private int up = 0; //THIS IS FOR HOW MANY TIMES YOU CAN ACTIVATE THE THING
	public bool goingUp = true; // start this false if we want button-activated
	public AudioSource source;

	void Start()
	{
		//minHeight = elevatorPlatform.transform.position.y;
		//maxHeight = elevatorPlatform.transform.position.y + maxHeight;
	}
	void Update()
	{
		if (goingUp)
		{
			topPos = elevatorBase.transform.position.y + maxHeight;
			if (elevatorPlatform.transform.position.y < topPos - 0.1f)
			{
				elevatorPlatform.transform.position = Vector3.MoveTowards(elevatorPlatform.transform.position,
					new Vector3(elevatorPlatform.transform.position.x, topPos, elevatorPlatform.transform.position.z), speed * Time.deltaTime);
			}
			else
			{
				goingUp = false;
				source.Stop();
				source.PlayDelayed(.5f);
			}
			//elevatorPlatform.transform.Translate(new Vector3(0, 1, 0) * speed* Time.deltaTime); 
		}
		else
		{
			botPos = elevatorBase.transform.position.y + minHeight;
			if (elevatorPlatform.transform.position.y > botPos + 0.1f)
			{
				elevatorPlatform.transform.position = Vector3.MoveTowards(elevatorPlatform.transform.position,
					  new Vector3(elevatorPlatform.transform.position.x, botPos, elevatorPlatform.transform.position.z), speed * Time.deltaTime);
			}
			else
			{
				goingUp = true;
				source.Stop();
				source.PlayDelayed(.5f);
			}
			//elevatorPlatform.transform.Translate(new Vector3(0, 1, 0) * -speed* Time.deltaTime); 
		}
	}

	//The following is for if we want the platform to be activated by button-press, which we currently do not
	//void OnTriggerStay(Collider col)
	//{
	//	if(col.tag == "Alpha") //Please change this value later
	//	{
	//		Debug.Log("Colliding");
	//		if(Input.GetButton(Axes.toStr[interact]))// && up <= 1)
	//		{
	//			up++;
	//			//elevatorPlatform.transform.position = (new Vector3(this.transform.position.x, this.transform.position.y * maxHeight, this.transform.position.z));
	//			goingUp = true;
	//		}
	//		else { goingUp = false; }
	//	}
	//}

	//void OnTriggerExit(Collider col)
	//{
	//	Debug.Log("EXIT");
	//	//elevatorPlatform.transform.position = (new Vector3(this.transform.position.x, minHeight, this.transform.position.z));
	//	if(col.tag == "Alpha")
	//	{
	//		up--;
	//		goingUp = false;
	//	}
	//}
}
