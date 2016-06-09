using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiplayerManager : MonoBehaviour {

    private ServerCommunication serverComms;
    public uint localPlayerID;
    public GameObject localPlayer = null;
    private Dictionary<uint, GameObject> chickenDic;

    void Awake()
    {
        chickenDic = new Dictionary<uint, GameObject>();

        serverComms = gameObject.GetComponent<ServerCommunication>();

        serverComms.onPlayerConnected += AddNewPlayer;
        serverComms.onPlayerDisconnected += RemovePlayer;
        serverComms.onGameInfoReceived += InitPlayers;
        serverComms.onPlayerMoved += PlayerMoved;
        serverComms.onPlayerUpdateHealth += HealthUpdated;
        serverComms.onPlayerUpdateState += (other, state) => {
            if (other.id != serverComms.player.id)
                chickenDic[other.id].GetComponent<ChickController>().state = state;
        };
    }


    public void GameStart (GameObject chicken, string name)
    {
        //Add our Player(s) to the game returns which number we are in the list. 
        //for(numLocalPlayers)
        //localPlayers.Add(Server.AddPlayer())
        localPlayer = chicken;
        serverComms.EnterGame(name, localPlayer.transform.position, 100f);
        
    }

    void InitPlayers(uint id, Dictionary<uint, ServerCommunication.Player> otherPlayers)
    {
        foreach (KeyValuePair<uint, ServerCommunication.Player> entry in serverComms.otherPlayers)
        {
            AddNewPlayer(entry.Value);
        }
        localPlayerID = id;
    }

    void HealthUpdated(ServerCommunication.Player player, float health)
    {
        GameObject chick = null;
        if (player.id == localPlayerID)
            localPlayer.GetComponent<HealthSystem>().SetHealth(health);
        if (chickenDic.TryGetValue(player.id, out chick))
            chick.GetComponent<HealthSystem>().SetHealth(health);
    }

    void AddNewPlayer(ServerCommunication.Player newPlayer)
    {
        //fix issue with name not being set to the other players
        GameObject newChick = ChickenSpawnerManager.Instance.SpawnChickenAt(newPlayer.pos, 0, 0, newPlayer.name);
        newChick.GetComponent<ChickController>().serverID = newPlayer.id;
        chickenDic[newPlayer.id] = newChick;
    }

    void RemovePlayer(ServerCommunication.Player newPlayer)
    {
        GameObject player = null;
        if (chickenDic.TryGetValue(newPlayer.id, out player))
        {
            var chickController = player.GetComponent<ChickController>();
            Destroy(chickController.ui.gameObject);
            Destroy(player);
        }
        
        chickenDic.Remove(newPlayer.id);
    }

    void PlayerMoved(ServerCommunication.Player player)
    {
        chickenDic[player.id].GetComponent<ChickDummy>().UpdatePosition(player.pos);
    }



}
