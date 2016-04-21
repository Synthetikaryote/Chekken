using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChickenSpawnerManager : MonoBehaviour
{
    //singleton stuff
    public static ChickenSpawnerManager Instance { get; protected set; }
    //game stuff
    public GameObject[] myChicken;
    public bool spawnInRandomOrder = false;
    private List<ChickenSpawner> chickenSpawners;
    // Use this for initialization

    // TODO: add skill support to spawning (Add it in SpawnChickenOnternal)

    void Awake()
    {
        Instance = this;
        if (myChicken.Length == 0)
        {
            Debug.Log("[Chicken Spawner] no chicken prefab attached");
        }

        chickenSpawners = new List<ChickenSpawner>();
    }
    public bool SpawnChicken(int chickenID, int skillID)
    {
        //TODO : Attach skill id to chicken ID later
        if (chickenID >= myChicken.Length || chickenID < 0)
        {
            Debug.Log("[Chicken Spawner] id out of bound");
            return false;
        }
        /*
          only spawning prefab right now
          need to add skill support
         */

        if(spawnInRandomOrder)
        {
            List<ChickenSpawner> randomList = GetRandomListOfChicken(new List<ChickenSpawner>(chickenSpawners));
            if(randomList == null)
            {
                return false;
            }

            return SpawnChickenInternal(ref randomList, chickenID, skillID);
        }
        else
        {
            return SpawnChickenInternal(ref chickenSpawners, chickenID, skillID);
        }
    }

    bool SpawnChickenInternal(ref List<ChickenSpawner> spawnerList, int chickenID, int skillID)
    {
        for (int i = 0; i < spawnerList.Count; ++i)
        {
            ChickenSpawner spawner = spawnerList[i];
            if (spawner.CanSpawn())
            {
                GameObject spawnedChicken = spawner.SpawnChicken(myChicken[chickenID]);

                // do the skill support here (using spawned chicken add the nessecary skills to the chicken)
                return true;
            }
        }
        return false;
    }

    List<ChickenSpawner> GetRandomListOfChicken(List<ChickenSpawner> startingList)
    {
        if(startingList.Count == 0)
        {
            Debug.Log("No spawners to randomize");
            return null;
        }

        List<ChickenSpawner> returnList = new List<ChickenSpawner>();

        while(startingList.Count > 1)
        {
            int nextIndex = Random.Range(0, startingList.Count);
            returnList.Add(startingList[nextIndex]);
            startingList.RemoveAt(nextIndex);
        }
        returnList.Add(startingList[0]);

        return returnList;
    }

    // fuctions used to manage list of spawners
    public void RegisterSpawner(ref ChickenSpawner spawner)
    {
        chickenSpawners.Add(spawner);
    }

    public void UnRegisterSpawner(ref ChickenSpawner spawner)
    {
        chickenSpawners.Remove(spawner);
    }
}
