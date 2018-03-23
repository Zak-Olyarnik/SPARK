using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Taken from https://answers.unity.com/questions/1145582/disable-mouse-input-ui.html from Mmmpies.
public class ControllerRefocus : MonoBehaviour {

	private GameObject selectedObj;
	void Start()
	{
		selectedObj = EventSystem.current.currentSelectedGameObject;
	}
	 void Update()
     {
         if (EventSystem.current.currentSelectedGameObject == null)
         {
             //Debug.Log("Reselecting first input");
             EventSystem.current.SetSelectedGameObject(selectedObj);
         }
		 selectedObj = EventSystem.current.currentSelectedGameObject;
     }
}
