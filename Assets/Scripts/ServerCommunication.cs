using UnityEngine;
using System.Collections;
using System;

public class ServerCommunication : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {
        
		WebSocket w = new WebSocket(new Uri("ws://ec2-54-187-163-147.us-west-2.compute.amazonaws.com:8080"));
        w.OnError += OnError;
        w.OnLogMessage += OnLogMessage;
        Debug.Log("Trying to connect");
        yield return StartCoroutine(w.Connect());
        Debug.Log(w.isConnected ? "Connected!" : "Couldn't connect");
        if (w.isConnected) {
            Debug.Log("Sending \"Hi there\"");
            w.SendString("Hi there");
        }
		int i=0;
		while (true)
		{
			string reply = w.RecvString();
			if (reply != null)
			{
				Debug.Log ("Received: "+reply);
				w.SendString("Hi there"+i++);
			}
			if (w.lastError != null)
			{
				break;
			}
			yield return 0;
		}
		w.Close();
	}

    void OnLogMessage(WebSocketSharp.LogData logData, string message) {
        Debug.LogWarning(logData.Message);
    }

    void OnError(string message) {
        Debug.LogWarning("WebSocket error: " + message);
    }
}
