using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NameManager : MonoBehaviour
{
    //instance
    public static NameManager instance { protected set; get; }

    //List<string> playerNameList = new List<string>();
    //note : temporary hack for unique name id
    List<string> playerList;
    Dictionary<string, List<int>> playerDictionary;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        playerList = new List<string>();
        playerDictionary = new Dictionary<string, List<int>>();
    }
    public void AddPlayerName(string playerName)
    {
        if (playerDictionary.ContainsKey(playerName))
        {
            int id = playerDictionary[playerName].Count;
            playerDictionary[playerName].Add(id);
        }
        else
        {
            playerDictionary = new Dictionary<string, List<int>>();
            List<int> tempList = new List<int>();
            playerDictionary.Add(playerName, tempList);
            playerDictionary[playerName].Add(0);
        }
    }
    //    if (playerNameList.Contains(playerName))
    //    {
    //        if (!nameDuplicateIndex.ContainsKey(playerName))
    //        {
    //            nameDuplicateIndex[playerName] = new List<int>();
    //            nameDuplicateIndex[playerName].Add(0);
    //        }
    //        else
    //        {
    //            int temporaryIndex = nameDuplicateIndex[playerName].Count;
    //            nameDuplicateIndex[playerName].Add(temporaryIndex);
    //        }
    //    }
    //    else
    //    {
    //        //playerNameList.Add(playerName);
    //    }
    //}


}
