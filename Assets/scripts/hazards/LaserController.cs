using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
	//https://www.youtube.com/watch?v=hUg3UfE186Q
	private LineRenderer lr;

	private void Start()
	{
		lr = GetComponent<LineRenderer>();
	}

	private void Update()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit))
		{
			if (hit.collider)
			{
				lr.SetPosition(1, new Vector3(0, 0, hit.distance)); // resets end position of line renderer
				if (hit.collider.gameObject.tag == "Alpha" || hit.collider.gameObject.tag == "Beta")
				{
					hit.collider.gameObject.GetComponentInParent<PlayerController>().Die(); // kills player on collision
				}
				if(hit.collider.gameObject.layer == 8) // Kill Buildable on Collision
				{
					hit.collider.gameObject.GetComponent<Buildable>().Reset();
				}
				if(hit.collider.gameObject.layer == 13) // Kill and respawn Machines.
				{
					hit.collider.gameObject.GetComponentInParent<Deconstructable>().Reset();
				}
			}
		}
		else lr.SetPosition(1, new Vector3(0, 0, 5000));
	}
}
