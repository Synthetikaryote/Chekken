using UnityEngine;
using System.Collections;
using System;

public class ServerCommunication : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {
		WebSocket w = new WebSocket(new Uri("ws://ec2-54-187-163-147.us-west-2.compute.amazonaws.com"));
		yield return StartCoroutine(w.Connect());
		w.SendString("Hi there");
		int i=0;
		while (true)
		{
			string reply = w.RecvString();
			if (reply != null)
			{
				Debug.Log ("Received: "+reply);
				w.SendString("Hi there"+i++);
			}
			if (w.error != null)
			{
				Debug.LogError ("Error: "+w.error);
				break;
			}
			yield return 0;
		}
		w.Close();
	}
}
