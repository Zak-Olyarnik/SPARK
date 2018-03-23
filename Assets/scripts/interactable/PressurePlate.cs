using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
	public Activatable[] activatables;
	public GameObject button;
	public AudioSource source;

	private int objectsOn = 0;
	private bool buttonMove = false, reverseButtonMove = false;
	private Vector3 maxPush, initialPos;
    private bool safe = true;

    void Start()
	{
		initialPos = this.transform.position;
		maxPush = new Vector3(0,-0.1f,0) + initialPos;
	}

    private void MakeSafe()
    {
        safe = true;
        //Debug.Log("safe again");
    }

    void LateUpdate()
	{
		if(buttonMove)
		{
			button.transform.position = Vector3.Lerp(maxPush, initialPos, 0.02f * Time.deltaTime);
			if(button.transform.position == maxPush)
			{
				buttonMove = false;
				reverseButtonMove = false;
			}
		}
		if(reverseButtonMove)
		{
			button.transform.position = Vector3.Lerp(initialPos, maxPush, 0.02f * Time.deltaTime);
			if(button.transform.position == initialPos) 
			{
				buttonMove = false;
				reverseButtonMove = false;
			}	
		}
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.GetComponent(typeof(HeavyObject)))
		{
			objectsOn++;
			if (objectsOn == 1 && safe)
			{
				source.Play();
				buttonMove = true;
				reverseButtonMove = false;
                safe = false;
                Invoke("MakeSafe", 0.001f);

                foreach (Activatable activatable in activatables)
				{
					activatable.ChangeState();
				}
			}
		}
	}

	void OnTriggerExit(Collider coll)
	{
		if (coll.gameObject.GetComponent(typeof(HeavyObject)))
		{
			objectsOn--;
			if (objectsOn == 0 && safe)
			{
				source.Play();
				buttonMove = false;
				reverseButtonMove = true;
                safe = false;
                Invoke("MakeSafe", 0.001f);

                foreach (Activatable activatable in activatables)
				{
					activatable.ChangeState();
				}
			}
		}
	}
}
