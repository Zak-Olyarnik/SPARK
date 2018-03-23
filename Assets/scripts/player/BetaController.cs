using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Work log
//Initial File - Jack Phoebus
//Cleanup for demo - Zak Olyarnik
public class BetaController : PlayerController {

	public float deconstructAnimTime,shootingSpeed;
	public GameObject destroyBomb, deconstructBomb;
	public Transform bombPos, shootPos;
	public AudioSource deconstructShot;

	public Axes.Action deconstruct, destroy;
	private float timeStamp;

	protected override void Update()
	{
		base.Update();
		if (canMove)
		{
			if (Time.time >= timeStamp && Input.GetButton(Axes.toStr[deconstruct]))
			{
				Deconstruct();
				timeStamp = Time.time + .75f;
			}
			if (Time.time >= timeStamp && Input.GetButton(Axes.toStr[destroy])) 
			{
				 Destroy(); 
				 timeStamp = Time.time + 1.0f;
			}

			Jump();
			//if (Input.GetButtonDown(Axes.toStr[aim])) { BetaAim(); } //Aim
			//if(Input.GetButtonUp(Axes.toStr[aim])){Invoke("TurnOffReticle", 0.1f);}
		}
	}

	//void OnTriggerEnter(Collider coll)
	//{
	//	// Alpha is attempting to carry Beta
	//	if (coll.tag == "AlphaCarry")
	//	{
	//		this.canMove = false;
	//		coll.gameObject.GetComponentInParent<PlayerController>().Carry(this.gameObject);
	//	}
	//}

	protected void Deconstruct()
	{
		animator.primary();
		StartCoroutine(DeconstructCreation());
		/*
		if (aiming)
		{
			bomb.GetComponent<Rigidbody>().velocity = (cam.transform.forward * shootingSpeed * 2);
		}
		*/
	}

	protected void Destroy()
	{
		animator.secondary();
		StartCoroutine(DestroyCreation());
	}

	protected override void Respawn()
	{
		
			//GameController.GetInstance().StartCoroutine(GameController.GetInstance().BetaRespawn());
		base.Respawn();
		transform.position = GameController.GetInstance().betaSpawn;
	}	

	IEnumerator DestroyCreation() 
	{
		yield return new WaitForSeconds(0.2f);
		GameObject bomb = Instantiate(destroyBomb, bombPos);
		bomb.transform.SetParent(null);
	}

	IEnumerator DeconstructCreation() {
		yield return new WaitForSeconds(0.2f);
		GameObject bomb = Instantiate(deconstructBomb, shootPos);
		bomb.transform.SetParent(null);
		bomb.GetComponent<Rigidbody>().velocity = (bomb.transform.forward * shootingSpeed);
		deconstructShot.Play();
	}
}
