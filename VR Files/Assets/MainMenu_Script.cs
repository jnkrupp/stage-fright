using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu_Script : MonoBehaviour {

	//Menu items (start, quit, title)
	private Text[] menuitems;
	private bool menuDisabled = true;
	private bool simulationBegun = false;
	private int current_selection = 0;

	// Use this for initialization
	void Start ()
	{
		menuitems = this.GetComponentsInChildren<Text>();
		foreach (Text t in menuitems)
		{
			t.gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Check if this is null before showing menu
		if (GameObject.Find("Canvas_ResetVRPos"))
		{
			return;
		}
		else
		{
			//show menu UI
			if (menuDisabled)
			{
				menuitems[0].gameObject.SetActive(true); //Title
				menuitems[1].gameObject.SetActive(true); //Start
				menuitems[2].gameObject.SetActive(true); //Quit
				menuDisabled = false;
			}

			//handle menu choosing inputs
			if (!simulationBegun &&
				(OVRInput.GetDown(OVRInput.Button.DpadLeft) || OVRInput.GetDown(OVRInput.Button.DpadRight)))
			{
				if (current_selection == 0)
					current_selection = 1;
				else
					current_selection = 0;
			}
			//handle menu choosing text options
			if (current_selection == 0)
			{
				menuitems[1].text = ">Start";
				menuitems[2].text = "Quit";
			}
			else if (current_selection == 1)
			{
				menuitems[1].text = "Start";
				menuitems[2].text = ">Quit";
			}


			//handles menu selecting text options
			if (OVRInput.GetDown(OVRInput.Button.One) && !simulationBegun)
			{
				Debug.Log("Current Selection: " + current_selection);
				if (current_selection == 0)
				{
					//SIMULATION STARTS HERE
					menuitems[0].gameObject.SetActive(false); //Title
					menuitems[1].gameObject.SetActive(false); //Start
					menuitems[2].gameObject.SetActive(false); //Quit
					menuitems[3].gameObject.SetActive(true); //This should be the gameObject that displays instructions
					simulationBegun = true;

					GameObject player_voicerecogObj = GameObject.Find("OVRPlayerController");
					Player_VoiceRecognition p_vr= player_voicerecogObj.GetComponent<Player_VoiceRecognition>();
					p_vr.Timer_Ready = true;

				}
				else if (current_selection == 1)
 				{
					Debug.Log("Game is quit");
					Application.Quit(); //apparently this is ignored in editor
				}
			}
		}
	}
}
