using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : Activatable
{
    public float moveSpeed;
    public Transform endPos;
    public Vector3 startPos;

    public override void Activate()
    {
        isActivated = true;
    }

    public override void Deactivate()
    {
        isActivated = false;
    }

    private void Start()
    {
        startPos = transform.position;
    }

    void FixedUpdate()
    {
        if (isActivated)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime);
        }
    }
}
