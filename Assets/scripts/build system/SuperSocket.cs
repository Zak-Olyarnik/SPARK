using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSocket : MonoBehaviour
{
	public Socket[] sockets;
	public int builtWatchedSockets;
	//public int totalWatchedSockets;
	public bool isBuilt = false;
	public Activatable[] activatables;

	private void Update()
	{
		builtWatchedSockets = 0;
		foreach (Socket socket in sockets)
		{
			if (socket.isBuilt)
			{
				builtWatchedSockets++;
			}
		}
		if (builtWatchedSockets == sockets.Length) // if everything is completed
		{
			//builtWatchedSockets = 0;
			if (!isBuilt)
			{
				isBuilt = true;
				foreach (Activatable activatable in activatables)
				{
					activatable.ChangeState();
				}
			}
		}
		else
		{
			if (isBuilt)
			{
				isBuilt = false;
				foreach (Activatable activatable in activatables)
				{
					activatable.ChangeState();
				}
			}
		}
	}
}
