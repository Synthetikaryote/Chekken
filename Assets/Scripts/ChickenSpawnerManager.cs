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

    public GameObject projectile;

    public bool spawnInRandomOrder = false;
    private List<ChickenSpawner> chickenSpawners;
    private bool isAlive;


    // TODO: add skill support to spawning (Add it in SpawnChickenInternal)
    public void ChickenIsDead()
    {
        isAlive = false;
    }
    void Awake()
    {
        isAlive = false;
        Instance = this;
        if (myChicken.Length == 0)
        {
            Debug.Log("[Chicken Spawner] no chicken prefab attached");
        }
        chickenSpawners = new List<ChickenSpawner>();
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
                GameObject spawnedChicken = spawner.SpawnChicken(myChicken[chickenID], skillID, pName);
                spawnedChicken.GetComponentInChildren<TextMesh>().text = pName;
                if (!isAlive)
                {
                    SpawnChicken(spawnedChicken, skillID);
                    isAlive = true;
                }
                // do the skill support here (using spawned chicken add the nessecary skills to the chicken)
                return true;
            }
        }
        return false;
    }
    void SpawnChicken(GameObject spawnedChicken, int skillID)
    {
        spawnedChicken.GetComponent<ChickController>().enabled = true;
        spawnedChicken.GetComponent<ChickController>().gameObject.SetActive(true);
        switch (skillID)
        {
            case 0:
                Debug.Log("All skill attached!");
                spawnedChicken.AddComponent<AbilityTeleportScript>().Initialize();
                spawnedChicken.AddComponent<AbilityRangedAttack>().Intialize();
                spawnedChicken.GetComponent<AbilityRangedAttack>().mProjectile = projectile;
                break;
            case 1:
                Debug.Log("AbilityTeleportScipt attached");
                spawnedChicken.AddComponent<AbilityTeleportScript>();
                break;
            case 2:
                Debug.Log("AbilityRangedAttack attached");
                spawnedChicken.AddComponent<AbilityRangedAttack>();
                spawnedChicken.GetComponent<AbilityRangedAttack>().mProjectile = projectile;
                break;
            case 3:
                Debug.Log("Skill 3 attached");
                break;
            default:
                Debug.LogError("Skill not found[id]: " + skillID);
                break;
        }
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
