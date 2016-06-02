using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ChickenSpawnerManager : MonoBehaviour
{

    public float damageRateMultiplier;
    //Audio and particle effects to dynamically add
    public GameObject JumpEffect;
    public GameObject ExplosionEffect;
    public GameObject JumpAudio;
    public GameObject ExplosionAudio;
    public GameObject AttackAudio;
    public GameObject DamageAudio;
    public GameObject mProjectile;
    public GameObject mShield;

    Input input;
    //singleton stuff
    public static ChickenSpawnerManager Instance { get; protected set; }
    
    //game stuff
    public GameObject[] chickenPrefabs;
    public bool spawnInRandomOrder = false;
    private List<ChickenSpawner> chickenSpawners;

    void Awake()
    {
        Instance = this;
        Assert.IsFalse(chickenPrefabs.Length == 0, "[Chicken Spawner] no chicken prefab attached");
        chickenSpawners = new List<ChickenSpawner>();
    }
    public GameObject SpawnChicken(int prefabID, int skillID, string playerName)
    {
        Assert.IsFalse(prefabID >= chickenPrefabs.Length || prefabID < 0, "[Chicken Spawner] id out of bound");
        NameManager.instance.AddPlayerName(playerName);
        int spawnerId = 0;
        if(spawnInRandomOrder)
        {
            spawnerId = GetRandomChickenSpawner(new List<ChickenSpawner>(chickenSpawners));
        }
        else
        {
            spawnerId = GetSequencedChickenSpawner();
        }

        GameObject spawnedChicken = chickenSpawners[spawnerId].SpawnChicken(chickenPrefabs[prefabID], skillID, playerName);
        spawnedChicken.AddComponent<ChickLocal>().damageRate = damageRateMultiplier;


        switch (skillID)
        {
            case 0://Remember to watch for GetComponent<AbilityNameScript>().Initialize() or AddComponent...
                Debug.Log("All skill attached!");
                spawnedChicken.AddComponent<AbilityTeleportScript>().Initialize();
                spawnedChicken.AddComponent<AbilityRangedAttack>().Initialize();
                spawnedChicken.AddComponent<AbilityTornadoScript>().Initialize();
                break;
            case 1:
                Debug.Log("AbilityTeleportScipt attached");
                spawnedChicken.AddComponent<AbilityTeleportScript>().Initialize();
                break;
            case 2:
                Debug.Log("AbilityRangedAttack attached");
                spawnedChicken.AddComponent<AbilityRangedAttack>().Initialize();


                break;
            case 3:
                Debug.Log("AbilityTornadoScript attached");
                spawnedChicken.AddComponent<AbilityTornadoScript>().Initialize();
                break;
            case 4:
                Debug.Log("AbilityForceFieldScript attached");
                spawnedChicken.AddComponent<AbilityForceField>().Initialize();
                break;
            default:
                Debug.LogError("Skill not found[id]: " + skillID);
                break;
        }

        return spawnedChicken;
    }

    public GameObject SpawnChickenAt(Vector3 posToSpawn, int prefabID, int skillID, string playerName)
    {
        Quaternion spawnRotation = Quaternion.Euler(new Vector3(0.0f, 180.0f , 0.0f));
        GameObject spawnedChicken = (GameObject)Instantiate(chickenPrefabs[prefabID], transform.position, spawnRotation);
        NameManager.instance.AddPlayerName(playerName);
        spawnedChicken.AddComponent<ChickDummy>();
        var ui = spawnedChicken.GetComponentInChildren<ChickUI>().GetComponentInChildren<TextMesh>().text = playerName;

        switch (skillID)
        {
            case 0://Remember to have spawnedChicken.GetComponent<AbilityNameScript>().Initialize();
                Debug.Log("All skill attached!");
                spawnedChicken.AddComponent<AbilityTeleportScript>().Initialize();
                spawnedChicken.AddComponent<AbilityRangedAttack>().Initialize();
                spawnedChicken.AddComponent<AbilityTornadoScript>().Initialize();
                break;
            case 1:
                Debug.Log("AbilityTeleportScipt attached");
                spawnedChicken.AddComponent<AbilityTeleportScript>().Initialize();
                break;
            case 2:
                Debug.Log("AbilityRangedAttack attached");
                spawnedChicken.AddComponent<AbilityRangedAttack>().Initialize();
                break;
            case 3:
                Debug.Log("AbilityTornadoScript attached");
                spawnedChicken.AddComponent<AbilityTornadoScript>().Initialize();
                break;
            case 4:
                Debug.Log("AbilityForceFieldScript attached");
                spawnedChicken.AddComponent<AbilityForceField>().Initialize();
                break;
            default:
                Debug.LogError("Skill not found[id]: " + skillID);
                break;
        }

        return spawnedChicken;
    }

    int GetSequencedChickenSpawner()
    {
        for(int i = 0; i < chickenSpawners.Count; ++i)
        {
            if(chickenSpawners[i].CanSpawn())
            {
                return i;
            }
        }
        return -1;
    }

    int GetRandomChickenSpawner(List<ChickenSpawner> startingList)
    {
        if(startingList.Count == 0)
        {
            Debug.Log("No spawners to randomize");
            return -1;
        }

        while(startingList.Count > 0)
        {
            int nextIndex = Random.Range(0, startingList.Count);
            if (startingList[nextIndex].CanSpawn())
                return nextIndex;
            else
                startingList.RemoveAt(nextIndex);
        }
        Debug.Log("No Places to spawn");
        return -1;
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
