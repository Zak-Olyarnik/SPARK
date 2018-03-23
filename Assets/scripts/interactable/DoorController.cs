using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : Activatable
{
	public GameObject leftDoor, rightDoor;
	public GameObject doorCollider;
	public Vector3 leftTransform, rightTransform;
	public float openSpeed;
	public AudioSource source;
	private bool play = false, reversePlay = false;
	private Vector3 closedLeftPos, closedRightPos, openLeftPos, openRightPos;

	void Start ()
	{
		closedLeftPos = leftDoor.transform.position;
		closedRightPos = rightDoor.transform.position;
		openLeftPos = closedLeftPos + leftTransform;
		openRightPos= closedRightPos + rightTransform;
	}

	public override void Activate()
	{
		isActivated = true;

		source.Play();
		play = false;
		reversePlay = true;
		doorCollider.SetActive(true);
	}

	public override void Deactivate()
	{
		isActivated = false;

		source.Play();
		play = true;
		reversePlay = false;
		doorCollider.SetActive(false);
	}

	void Update ()
	{
		if(play)
		{
			// door open animation
			leftDoor.transform.position = Vector3.MoveTowards(leftDoor.transform.position, openLeftPos, openSpeed * Time.deltaTime);
			rightDoor.transform.position = Vector3.MoveTowards(rightDoor.transform.position, openRightPos, openSpeed * Time.deltaTime);

			if (leftDoor.transform.position == openLeftPos)
			{
				leftDoor.SetActive(false);
			}
			if (rightDoor.transform.position == openRightPos)
			{
				rightDoor.SetActive(false);
			}
		}
		if(reversePlay)
		{
			// door close animation
			leftDoor.SetActive(true);
			rightDoor.SetActive(true);
			leftDoor.transform.position = Vector3.MoveTowards(leftDoor.transform.position, closedLeftPos, openSpeed * Time.deltaTime);
			rightDoor.transform.position = Vector3.MoveTowards(rightDoor.transform.position, closedRightPos, openSpeed * Time.deltaTime);
		}
	}
}
