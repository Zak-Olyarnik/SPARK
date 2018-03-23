using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetween : Activatable {

	public GameObject EndPos;
	public float speed;
	private bool returnToOrigin;
	private Vector3 initialPosition;

	public override void Activate()
    {
        isActivated = true;
    }

    public override void Deactivate()
    {
        isActivated = false;
    }
	void Start()
	{
		returnToOrigin = false;
		initialPosition = this.transform.position;
	}
	void FixedUpdate()
	{
		if(isActivated)
		{
			if(!returnToOrigin)
			{
				this.transform.position = Vector3.MoveTowards(this.transform.position, EndPos.transform.position, speed);
				if(this.transform.position == EndPos.transform.position || Vector3.Distance(this.transform.position, EndPos.transform.position) <= 0.5f)
				{
					returnToOrigin = true;
				}
			}
			else
			{
				this.transform.position = Vector3.MoveTowards(this.transform.position, initialPosition, speed);
				if(this.transform.position == initialPosition || Vector3.Distance(this.transform.position, initialPosition) <= 0.5f)
				{
					returnToOrigin = false;
				}
			}
		}
		
	}
}
