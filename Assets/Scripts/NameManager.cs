using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NameManager : MonoBehaviour
{
    //instance
    public static NameManager instance { protected set; get; }

    List<string> playerNameList = new List<string>();
    //note : temporary hack for unique name id
    Dictionary<string, List<int>> nameDuplicateIndex;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        playerNameList = new List<string>();
        nameDuplicateIndex = new Dictionary<string, List<int>>();
    }
    public void AddPlayerName(string playerName)
    {
        if (playerNameList.Contains(playerName))
        {
            if (!nameDuplicateIndex.ContainsKey(playerName))
            {
                nameDuplicateIndex[playerName] = new List<int>();
                nameDuplicateIndex[playerName].Add(0);
            }
            else
            {
                int temporaryIndex = nameDuplicateIndex[playerName].Count;
                nameDuplicateIndex[playerName].Add(temporaryIndex);
            }
        }
        else
        {
            playerNameList.Add(playerName);
        }
    }


}
