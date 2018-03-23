using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	//public Transform target;
	public  PlayerController pc;
	public GameObject behindPlayer;
	public GameObject cameraFocus;
	public float camDistance=5;
	public float camSensitivity=200;
	public float camMaxAngle=80;
	public float camMinAngle=-70;
	public float camDispFactor=1.2f;
	//private float camMaxAngleTemp;
	//private float camMinAngleTemp;
	public Axes.Action xaxis,yaxis;

	float camVertAngle=20,camHorAngle=0;

	const float p=Mathf.PI/180;
	private bool temp = false;
	// Use this for initialization
	void Start () {
		//camMaxAngleTemp = camMaxAngle;
		//camMinAngleTemp = camMinAngle;
	}
	
	// Update is called once per frame
	void LateUpdate () {

		camHorAngle += camSensitivity * Time.deltaTime * Input.GetAxis(Axes.toStr[xaxis]);

		camVertAngle += camSensitivity * Time.deltaTime * Input.GetAxis(Axes.toStr[yaxis]);
		camVertAngle = Mathf.Clamp(camVertAngle, camMinAngle, camMaxAngle);

		Vector3 pos = camDistance * Vector3.forward;

		pos = Quaternion.AngleAxis(-camVertAngle, Vector3.right) * pos;
		pos = Quaternion.AngleAxis(camHorAngle, Vector3.up) * pos;

		//make sure camera stays in front of walls
		float dist = camDistance;
		//RaycastHit[] hits = Physics.RaycastAll(cameraFocus.transform.position, pos, camDistance);
		RaycastHit[] hits = Physics.SphereCastAll(cameraFocus.transform.position, 0.5f , pos, camDistance, -1);
		foreach (RaycastHit hit in hits)
		{
			if (hit.transform.tag == "Wall")
			{
				dist = Mathf.Min(dist, hit.distance);
			}
		}
				
		
		pos = pos.normalized * dist;
		float distanceRatio = (dist / camDistance);
		Vector3 focusDisplacement = Vector3.up * distanceRatio;
		cameraFocus.transform.position = pc.transform.position + (Vector3.up * camDispFactor) + focusDisplacement;

		if (pc.aiming)
		{	
			transform.position = behindPlayer.transform.position;
			temp = true;
		}
		else
		{
			transform.position = cameraFocus.transform.position+pos; //This line is breaking it because of pc.transform.position

			/*
			if (temp)
			{
				pos = behindPlayer.transform.position; //This line is breaking it because of pc.transform.position
				//temp = false;
				transform.position = pos; //This line is breaking it because of pc.transform.position
			}
			*/
		}

		transform.LookAt(cameraFocus.transform);

		/*
		if(pc.aiming)
		{
			//pos =new Vector3(-0.9f, 1, .5f);
			if(this.name == "Beta Cam")
			{
				transform.position+=
					0.25f*transform.right+
					0.3f*transform.up+
					3f*transform.forward;
			}

			if(this.name == "Alpha Cam")
			{
				transform.position+=
					0.55f*transform.right+
					0.5f*transform.up+
					3f*transform.forward;
			}
			camMaxAngle=80;
			camMinAngle=-40;
		}
		else
		{
			camMaxAngle = camMaxAngleTemp;
			camMinAngle = camMinAngleTemp;
		}
		*/
	}
}
