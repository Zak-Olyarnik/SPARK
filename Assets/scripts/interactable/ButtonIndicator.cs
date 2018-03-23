using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonIndicator : Activatable
{
	public Material activeMaterial, inactiveMaterial;

	public override void Activate()
	{
		isActivated = true;

		GetComponent<Renderer>().material = activeMaterial;
	}

	public override void Deactivate()
	{
		isActivated = false;

		GetComponent<Renderer>().material = inactiveMaterial;
	}
}
