using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroMagnet : MonoBehaviour {

	//Check for carrying an ElectroMagnet
	//Shoot a line from player in the direction of the camera.
	//If pulling shooting thing hits the object, freeze movement.
	//B is shoot
	//Move object to pulling position
	
	public GameObject wave;
	public GameObject[] pullingParticles, onParticles;
	public float timeStamp, pullingSpeed;
	private Transform parent;
	//private Vector3 currPosition;
	public Axes.Action interactAlpha, interactBeta;
	private bool turnOff=false;
	//private Vector3  startingPosition; //,pullDir;

	void Start()
	{
		
	}
	void Update()
	{
		if (this.GetComponent<Rigidbody>().isKinematic && transform.parent)
		{
			if (this.transform.parent.parent.name == "Alpha" || this.transform.parent.parent.name == "Beta")
			{
				parent = this.transform.parent.parent;
				this.transform.LookAt(this.transform.position + parent.forward);
				this.transform.Rotate(new Vector3(0, 180, 0));
			}
			if (Time.time >= timeStamp && Input.GetButtonDown(Axes.toStr[interactAlpha]) && turnOff ||
				Time.time >= timeStamp && Input.GetButtonDown(Axes.toStr[interactBeta]) && turnOff)
			{
				TurnOffShoot();
				turnOff = false;
			}
			else if (Time.time >= timeStamp && Input.GetButtonDown(Axes.toStr[interactAlpha]) && this.transform.parent.parent.name == "Alpha" ||
				Time.time >= timeStamp && Input.GetButtonDown(Axes.toStr[interactBeta]) && this.transform.parent.parent.name == "Beta")
			{
				Shoot();
				onParticles[0].SetActive(true);
				onParticles[1].SetActive(true);
				turnOff = true;
			}
		}
		else
		{
			TurnOffShoot();
			turnOff = false;
		}
		
	}
	void Shoot()
	{
		Debug.Log("okay?");
		wave.SetActive(true);
	}
	protected void TurnOffShoot()
	{
		wave.SetActive(false);
	}

	void OnTriggerStay(Collider col)
	{
		if (wave.activeSelf)
		{
			//CHECK WHY THIS LINE SOME TIMES BUGS - This happens when for some reason you are hitting both the soscket and the object, so add logic to don't look at the socket
			if (col.GetComponent<Pullable>() && this.transform.parent.parent.name == "Alpha" || col.GetComponent<Pullable>() && this.transform.parent.parent.name == "Beta") //Check if script has the pullable script
			{
				//startingPosition = col.GetComponent<Pullable>().initialLocation;

				//float distance = (Vector3.Distance(parent.transform.position, col.transform.position)); //Get object location, get curr pos, now decrease
				//Debug.Log(distance);
				pullingParticles[0].SetActive(true);
				pullingParticles[1].SetActive(true);
				onParticles[0].SetActive(false);
				onParticles[1].SetActive(false);

				//currPosition = parent.transform.localPosition;
				//PullAble PullAble = col.GetComponent<PullAble>();
				//pullDir = new Vector3(distance, 0, 0);
				//col.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

				if (col.GetComponent<Pullable>() && col.GetComponent<Pullable>().freezeZ)
				{
					if (col.transform.position.x > parent.transform.position.x) //DO MATH OF POSITION. YEA, IT'S WEIRD
					{//In general what this does is that if collider is in a spot x that is higher than alpha, then math will make it PULL to it
					 //Debug.Log("col.transform.position " + col.transform.position);
					 //Debug.Log(maxPosition[0] + startingPosition);
						col.transform.position = Vector3.MoveTowards(col.transform.position, col.GetComponent<Pullable>().finalLocationLeft, pullingSpeed * Time.deltaTime);
						//col.GetComponent<Rigidbody>().velocity = -pullDir * pullingSpeed;
					}
					else if (col.transform.position.x <= parent.transform.position.x)
					{//Without this, it would always push/pull to the dir of -pullDir*pullingSpeed
					 //Debug.Log(maxPosition[1] + col.transform.position);
						col.transform.position = Vector3.MoveTowards(col.transform.position, col.GetComponent<Pullable>().finalLocationRight, pullingSpeed * Time.deltaTime);
						//col.GetComponent<Rigidbody>().velocity = pullDir * pullingSpeed;
					}
					//Debug.Log("Velo " + col.GetComponent<Rigidbody>().velocity + " - PullDir " + pullDir + " - Distance " + distance);
				}
				if (col.GetComponent<Pullable>() && col.GetComponent<Pullable>().freezeX)
				{
					if (col.transform.position.z > parent.transform.position.z) //DO MATH OF POSITION. YEA, IT'S WEIRD
					{//In general what this does is that if collider is in a spot x that is higher than alpha, then math will make it PULL to it
					 //Debug.Log("col.transform.position " + col.transform.position);
					 //Debug.Log(maxPosition[0] + startingPosition);
						col.transform.position = Vector3.MoveTowards(col.transform.position, col.GetComponent<Pullable>().finalLocationLeft, pullingSpeed * Time.deltaTime);
						//col.GetComponent<Rigidbody>().velocity = -pullDir * pullingSpeed;
					}
					else if (col.transform.position.z <= parent.transform.position.z)
					{//Without this, it would always push/pull to the dir of -pullDir*pullingSpeed
					 //Debug.Log(maxPosition[1] + col.transform.position);
						col.transform.position = Vector3.MoveTowards(col.transform.position, col.GetComponent<Pullable>().finalLocationRight, pullingSpeed * Time.deltaTime);
						//col.GetComponent<Rigidbody>().velocity = pullDir * pullingSpeed;
					}
					//Debug.Log("Velo " + col.GetComponent<Rigidbody>().velocity + " - PullDir " + pullDir + " - Distance " + distance);
				}
			}
		}
	}

	void OnTriggerExit(Collider col)
	{
		pullingParticles[0].SetActive(false);
		pullingParticles[1].SetActive(false);
		onParticles[0].SetActive(true);
		onParticles[1].SetActive(true);
	}
}
