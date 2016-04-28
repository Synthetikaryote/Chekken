using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ChickenSpawnerManager : MonoBehaviour
{
    Input input;
    //singleton stuff
    public static ChickenSpawnerManager Instance { get; protected set; }
    //game stuff
    public GameObject[] myChicken;
    public bool spawnInRandomOrder = false;
    private List<ChickenSpawner> chickenSpawners;
    // Use this for initialization
    public AbilityRangedAttack ARA;
    public AbilityTeleportScript ATS;

    // TODO: add skill support to spawning (Add it in SpawnChickenInternal)

    void Awake()
    {
        Instance = this;
        if (myChicken.Length == 0)
        {
            Debug.Log("[Chicken Spawner] no chicken prefab attached");
        }

        chickenSpawners = new List<ChickenSpawner>();
        ARA = GetComponent<AbilityRangedAttack>();
        ATS = GetComponent<AbilityTeleportScript>(); 
    }
    public bool SpawnChicken(int chickenID, int skillID, string playerName)
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
        NameManager.instance.AddPlayerName(playerName);
        if(spawnInRandomOrder)
        {
            List<ChickenSpawner> randomList = GetRandomListOfChicken(new List<ChickenSpawner>(chickenSpawners));
            if(randomList == null)
            {
                return false;
            }
            return SpawnChickenInternal(ref randomList, chickenID, skillID, playerName);
        }
        else
        {
            return SpawnChickenInternal(ref chickenSpawners, chickenID, skillID, playerName);
        }
    }

    bool SpawnChickenInternal(ref List<ChickenSpawner> spawnerList, int chickenID, int skillID, string pName)
    {
        for (int i = 0; i < spawnerList.Count; ++i)
        {
            ChickenSpawner spawner = spawnerList[i];
            if (spawner.CanSpawn())
            {
                GameObject spawnedChicken = spawner.SpawnChicken(myChicken[chickenID], pName);
                spawnedChicken.GetComponent<ChickController>().enabled = true;
                spawnedChicken.GetComponent<HealthSystem>().enabled = true;
                // do the skill support here (using spawned chicken add the nessecary skills to the chicken)

                switch (skillID)
                {
                    case 0:
                        Debug.Log("AbilityRangedAttack attached");
                        spawnedChicken.GetComponent<AbilityRangedAttack>().enabled = true;
                        break;
                    case 1:
                        Debug.Log("AbilityTeleportScipt attached");
                        spawnedChicken.GetComponent<AbilityTeleportScript>().enabled = true;
                        break;
                    case 2:
                        Debug.Log("Skill 2 attached");
                        break;
                    case 3:
                        Debug.Log("Skill 3 attached");
                        break;
                    default:
                        Debug.LogError("Skill not found[id]: " + skillID);
                        break;
                }


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
        if (spawner == null)
        {
            return;
        }

        chickenSpawners.Add(spawner);
    }

    public void UnRegisterSpawner(ref ChickenSpawner spawner)
    {
        if(spawner == null)
        {
            return;
        }

        chickenSpawners.Remove(spawner);
    }
}
