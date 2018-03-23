using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activatable : MonoBehaviour
{
	public bool isActivated;
	public GameObject[] activatables;

	private bool safe = true;

	public void ChangeState()
	{
		if (safe)	// observed ChangeState() being called quickly in succession from the same OnTrigger event
		{
			if (isActivated) Deactivate();
			else Activate();
			safe = false;
			Invoke("MakeSafe", 0.001f);
		}
	}

	public virtual void Activate()
	{
		isActivated = true;
		Debug.Log(isActivated);
		foreach (GameObject activatable in activatables)
		{
			activatable.SetActive(true);
		}
	}

	public virtual void Deactivate()
	{
		isActivated = false;
		Debug.Log(isActivated);
		foreach (GameObject activatable in activatables)
		{
			activatable.SetActive(false);
		}
	}

	private void MakeSafe()
	{
		safe = true;
	}
}
