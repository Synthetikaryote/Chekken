using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiplayerManager : MonoBehaviour {

    public int[] localPlayers;
    private List<GameObject> PlayerChickens;
    private const float updateTime = 0.1f;
    private float currtime = 0.0f;
    private float downtime = 0.0f;


    void Start ()
    {
        //find a game to play in
        //Server.doStuffIdkWhat()
        
        //Get the list of players from the host game
        //Server.GetPlayers();

        //Add our Player(s) to the game returns which number we are in the list. 
        //for(numLocalPlayers)
        //localPlayers.Add(Server.AddPlayer())


    }
	
	void Update ()
    {
        //update the local players every frame 
        for (int i = 0; i < localPlayers.Length; ++i)
        {
            //update the local chickens
            //PlayerChickens[localPlayer[i]].updateChick();
        }

        //update the other chicks a couple of times every second
        currtime += Time.deltaTime;
        //check that the time is up to do another update
        if (currtime <= updateTime)
        {
            //check that we still have connection and can do update
            if (true /*connection == true*/)
            {
                for (int i = 0; i < PlayerChickens.Count; ++i)
                {
                    //update the chicks to the host game
                    //PlayerChickens[i].ServerUpdate();
                }
                currtime = 0.0f;
                downtime = 0.0f;
            }
            else
            {
                //boot the player from the game if they are not connecting for too long. 
                downtime += Time.deltaTime; 
            }
        }
	}

    void DealDamage(int recievingPlayer)
    {
        //update the host game 
    }

    void AddNewPlayer(int newPlayer)
    {
        //new player joined the game
        if (newPlayer >= PlayerChickens.Count)
        {
            int addtoend = newPlayer - PlayerChickens.Count;
            for (int i = 0; i < addtoend; ++i)
                //PlayerChickens.Add(new Chicken);
        }
        //PlayerChickens[player].enable();
    }

    void RemovePlayer(int player)
    {
        //set the player in the array to disabled
        //PlayerChickens[player].disable();
    }



}
