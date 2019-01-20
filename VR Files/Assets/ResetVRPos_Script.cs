using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetVRPos_Script : MonoBehaviour {
	private int timer = 300;
	private int decrement = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		/* Checks if a button is pressed on oculus remote
		 * What it should do:
		 * Reset the VR position of the player
		 * Disable the UI Canvas for the reset vr thingy
		 */
		if (OVRInput.GetDown(OVRInput.Button.DpadDown))
		{
			if (GameObject.Find("Canvas_ResetVRPos") != null)
			{
				GameObject.Find("Canvas_ResetVRPos").GetComponent<Canvas>().enabled = false;
				GameObject.Find("Canvas_ResetVRPos").GetComponent<Canvas>().gameObject.SetActive(false);
			}
				
			//recenter position
			UnityEngine.XR.InputTracking.Recenter();

		}
	}
}
