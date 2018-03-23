using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaController : PlayerController
{
	public GameObject constructCollider, createIndicator;
	public float constructAnimTime, volume; //aimAnimTime
	public Transform createPos;
	public GameObject[] simpleShapes;
	private GameObject[] createdSimpleShapes = new GameObject[3];
	private int simpleShapeNum = 0;
	public AudioClip createSound;
	private AudioSource sourceAlpha;
	public Vector3 createIndicatorPos;

	public Axes.Action construct, create, toss;

	private Transform simpleSpawn;
	void Start()
	{
		sourceAlpha = GetComponent<AudioSource>();
		createIndicatorPos = createIndicator.transform.localPosition;
	}

	protected override void Update() {
		base.Update();
		if(Input.GetButtonDown(Axes.toStr[construct])) { Construct(); }
		if (Input.GetButton(Axes.toStr[create])) {
			ShowCreate();
			animator.secondary();
		}else {
			animator.releaseSecondary();
		}
		if (Input.GetButtonUp(Axes.toStr[create])) { Create(); }

		//if(Input.GetButtonDown(Axes.toStr[toss])) { Throw(); } //Throw
	}
	protected void Construct() {
		//animator.primary();
		constructCollider.SetActive(true);
		Invoke("TurnOffConstructCollider", constructAnimTime);
	}

	protected void Create()
	{
		createIndicator.SetActive(false);
		sourceAlpha.PlayOneShot(createSound, volume);
		if(createdSimpleShapes[simpleShapeNum])
		{
			createdSimpleShapes[simpleShapeNum].gameObject.transform.position = new Vector3(0, -500, 0);	// this allows OnTriggerExit to be called
			Destroy(createdSimpleShapes[simpleShapeNum], 1f);
		}
		Vector3 temp = new Vector3(0, 0.5f, 0);
		simpleSpawn = createIndicator.transform;
		simpleSpawn.position = createIndicator.transform.position + temp;
		GameObject ss = Instantiate(simpleShapes[simpleShapeNum], simpleSpawn);
		ss.transform.SetParent(null);
		ss.transform.localScale = new Vector3(1f, 1f, 1f);
		createdSimpleShapes[simpleShapeNum] = ss;
		simpleShapeNum += 1;
		if(simpleShapeNum >=3)
		{ simpleShapeNum = 0; }
	}

	protected void ShowCreate()
	{
		RaycastHit hit;
		Ray inputRay = new Ray(createPos.transform.position, Vector3.down*2.6f); //Line from create position to Alpha's feet
		if (Physics.Raycast(inputRay, out hit)) //If it hits a simple cube
		{
			if (hit.collider && hit.collider.tag == "Wall")
			{
				createIndicator.transform.localPosition = createIndicatorPos;
				createIndicator.SetActive(true);
			}
			else
			{ 
				//Debug.Log(hit.transform.InverseTransformDirection(Vector3.up).y);
				createIndicator.transform.position = new Vector3(createIndicator.transform.position.x, -hit.transform.InverseTransformDirection(Vector3.up).y, createIndicator.transform.position.z);
				createIndicator.transform.position = createIndicator.transform.position + new Vector3(0, hit.transform.InverseTransformDirection(Vector3.up).y * .25f, 0); //Spawn object on top of the cube

				createIndicator.transform.position = new Vector3(createIndicator.transform.position.x, hit.point.y + .1f, createIndicator.transform.position.z);


				//createIndicator.transform.position = new Vector3(createIndicator.transform.position.x, -hit.transform.InverseTransformDirection(Vector3.up).y, createIndicator.transform.position.z);
				//if (hit.collider && hit.collider.tag != "Wall")
				//{
				//	Debug.Log("2");
				//	createIndicator.transform.position = createIndicator.transform.position + new Vector3(0, hit.transform.InverseTransformDirection(Vector3.up).y * .25f, 0); //Spawn object on top of the cube
				//}
				createIndicator.SetActive(true);
			}
		}

		//createIndicator.SetActive(true);
		//Debug.DrawRay(createPos.transform.position, Vector3.down*2.6f, Color.black);

		//Add

	}
	/*
	void Throw() {
		Debug.Log("Thrown");
		if(carryObj && aiming) //Check if player is carrying an Object
		{
			if (carryObj.GetComponent<Rigidbody>())		// Buildable
			{
				float oMass = carryObj.GetComponent<Rigidbody>().mass;
				carryObj.GetComponent<Rigidbody>().isKinematic = false;
				carryObj.transform.SetParent(null);
				if(cam.transform.rotation.x >= 0)
					carryObj.GetComponent<Rigidbody>().velocity = new Vector3(cam.transform.forward.x * throwSpeed,cam.transform.forward.y * throwSpeed * (float)Math.Sin(cam.transform.forward.y),cam.transform.forward.z * throwSpeed);
				else
				{
					carryObj.GetComponent<Rigidbody>().velocity = new Vector3(cam.transform.forward.x * throwSpeed,cam.transform.forward.y * throwSpeed * 0.9f,cam.transform.forward.z * throwSpeed);
				}
				carryObj = null;
			}
			else //Beta
			{
				carryObj.GetComponent<BetaController>().canMove = true;
				carryObj.transform.SetParent(null);
				//Vector3 throwBeta = new Vector3(throwSpeed / 1000, carryObj.transform.up.y, 0);
				//carryObj.GetComponent<BetaController>().transform.position = throwBeta;
				carryObj = null;
				
			}

		}

		//
	}
	*/

	protected void TurnOffConstructCollider()
	{
		constructCollider.SetActive(false);
	}

	protected override void Respawn()
	{
		//StartCoroutine(GameController.GetInstance().AlphaRespawn());
		base.Respawn();
		transform.position = GameController.GetInstance().alphaSpawn;
	}
	
}
