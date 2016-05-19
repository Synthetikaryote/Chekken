﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiplayerManager : MonoBehaviour {

    private ServerCommunication serverComms;
    public Object[] chickPrefab;
    public int[] localPlayers;
    private Dictionary<uint, GameObject> chickenDic;
    private const float updateTime = 0.1f;
    private float currtime = 0.0f;
    private float downtime = 0.0f;

    //OnPlayerConnected
    //OnPlayerMoved
    //OnPlayerDisconnected


    void GameStart ()
    {

        
        serverComms = gameObject.GetComponent<ServerCommunication>();
        serverComms.onPlayerConnected = AddNewPlayer;
        serverComms.onPlayerDisconnected = RemovePlayer;
        serverComms.onGameInfoReceived = InitPlayers;

        //Add our Player(s) to the game returns which number we are in the list. 
        //for(numLocalPlayers)
        //localPlayers.Add(Server.AddPlayer())

    }

    void InitPlayers(uint id, Dictionary<uint, ServerCommunication.Player> otherPlayers)
    {
        foreach (KeyValuePair<uint, ServerCommunication.Player> entry in serverComms.otherPlayers)
        {
            AddNewPlayer(entry.Value);
        }
    }
	
	void Update ()
    {
        //update the local players every frame 
        for (int i = 0; i < localPlayers.Length; ++i)
        {
            //update the local chickens
            //PlayerChickens[localPlayer[i]].updateChick();
        }
	}

    void DealDamage(int recievingPlayer)
    {
        //update the host game 
    }

    void AddNewPlayer(ServerCommunication.Player newPlayer)
    {
        GameObject newChick = (GameObject)Instantiate(chickPrefab[0], newPlayer.pos, Quaternion.identity);
        
        chickenDic.Add(newPlayer.id, newChick);
    }

    void RemovePlayer(ServerCommunication.Player newPlayer)
    {
        chickenDic.Remove(newPlayer.id);
    }



}