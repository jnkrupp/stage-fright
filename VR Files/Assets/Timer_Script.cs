using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer_Script : MonoBehaviour {
	public bool timerStarted = false; //If true, timer will start
	private int hour = 0, min = 0; //default values
	private float sec = 0; //This is a float since adding this to Time.deltaTime seems to be problematic when rounding
	private string hourString = "", minString = "", secString = ""; //Helps with formatting timer
	private float fogPercentage = 100; //100% = 0.50 fog density, 0% = 0.20 fog density

	// Update is called once per frame
	void Update () {
		//Starts timer and also slowly reduces fog
		if (timerStarted)
		{
			if (fogPercentage > 0)
			{
				fogPercentage -= Time.deltaTime * 0.5f;
				RenderSettings.fogDensity = 0.20f + (0.30f * (fogPercentage / 100));
				Debug.Log(string.Format("FogDensity = {0}, fogPercentage = {1}", RenderSettings.fogDensity, fogPercentage));
			}
			//Start incrementing time here
			sec += Time.deltaTime;
			if (sec >= 60)
			{
				min++;
				sec = 0;
				if (min >= 60)
				{
					hour++;
					min = 0;
				}
			}
		}

		//conversions from int to string (if number is single digit, add 0 to it)
		hourString = hour.ToString(); //doesn't really matter where 0 is up front, it'll be like 0:00:00
		if (min < 10)
			minString = "0" + min.ToString();
		else
			minString = min.ToString();
		//Seconds are a strange case, round before making it into a string
		if (sec < 10)
			secString = "0" + Mathf.FloorToInt(sec).ToString();
		else
			secString = Mathf.FloorToInt(sec).ToString(); //round to nearest whole number

		//Output this to text
		this.GetComponentInChildren<Text>().text = string.Format("{0}:{1}:{2}", hourString, minString, secString);
	}

	public void ResetTimer()
	{
		hour = 0;
		min = 0;
		sec = 0;
		fogPercentage = 100; //should also reset too
	}
}
