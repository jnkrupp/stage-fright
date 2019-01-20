using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Player_VoiceRecognition : MonoBehaviour
{
	public bool Timer_Ready = false;
	private KeywordRecognizer keywordRecognizer;
	private Dictionary<string, Action> actions = new Dictionary<string, Action>();

	// Use this for initialization
	void Start()
	{
		actions.Add("start", StartTimer);
		actions.Add("stop timer", StopTimer);
		actions.Add("start over", ResetTimer);
		keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
		keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
		keywordRecognizer.Start();
	}

	private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
	{
		Debug.Log(speech.text);
		actions[speech.text].Invoke();
	}

	private void StartTimer()
	{
		Debug.Log("start was recognized");
		if (Timer_Ready) //prevents timer starting if player isn't ready
		{
			Debug.Log("StartTimer() has been called");
			GameObject timer_gameobject = GameObject.Find("Canvas_Timer");
			Timer_Script ts = timer_gameobject.GetComponent<Timer_Script>();
			ts.timerStarted = true;
		}
	}

	private void StopTimer()
	{
		Debug.Log("StopTimer() has been called");
		GameObject timer_gameobject = GameObject.Find("Canvas_Timer");
		Timer_Script ts = timer_gameobject.GetComponent<Timer_Script>();
		ts.timerStarted = false;
	}

	private void ResetTimer()
	{
		Debug.Log("ResetTimer() has been called");
		GameObject timer_gameobject = GameObject.Find("Canvas_Timer");
		Timer_Script ts = timer_gameobject.GetComponent<Timer_Script>();
		ts.ResetTimer();
	}
}
