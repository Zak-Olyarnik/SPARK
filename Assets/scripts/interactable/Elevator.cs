using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : Activatable {

	public float minHeight, maxHeight;
	public float speed;
	public AudioSource source;
	private bool goingUp = false;

	public override void Activate()
	{
		isActivated = true;

		source.Play();
	}

	public override void Deactivate()
	{
		isActivated = false;

		source.Stop();
	}

	void FixedUpdate()
	{
		if (isActivated)
		{
			if (goingUp)
			{
				if (transform.position.y <= maxHeight)
				{ transform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime); }
				else
				{ goingUp = false; }
			}
			else
			{
				if (transform.position.y >= minHeight)
				{ transform.Translate(new Vector3(0, 1, 0) * -speed * Time.deltaTime); }
				else
				{ goingUp = true; }
			}
		}
	}
}
