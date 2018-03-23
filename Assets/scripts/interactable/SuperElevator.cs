using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperElevator : MonoBehaviour {

	public Socket[] sockets;
	public GameObject[] location;
	public float speed;
	public int socketsIn;
	private Vector3 initialPosition;

	void Start()
	{
		initialPosition = this.transform.position;
	}
	void Update()
	{
		socketsIn = 0;
		foreach (Socket socket in sockets)
		{
			if (socket.isBuilt)
			{
				socketsIn++;
			}
		}
		if(socketsIn == 1) //Hard coded because of time
		{
			Debug.Log(this.transform.position);
			this.transform.position = Vector3.MoveTowards(this.transform.position, location[0].transform.position, speed);
		}
		if(socketsIn == 2)
		{
			this.transform.position = Vector3.MoveTowards(this.transform.position, location[1].transform.position, speed);
		}
		else if(socketsIn == 0)
		{
			this.transform.position = Vector3.MoveTowards(this.transform.position, initialPosition, speed);
		}
	}
}
