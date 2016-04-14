using UnityEngine;
using System.Collections;

public class ChickenSpawner : MonoBehaviour
{
    //singleton stuff
    public static ChickenSpawner Instance { get; protected set; }
    //game stuff
    public GameObject[] myChicken;
    private Vector3 spawnPosition;
    // Use this for initialization

	void Start ()
    {
        Instance = this;
        if (myChicken.Length == 0)
        {
            Debug.Log("[Chicken Spawner] no chicken prefab attached");
        }
        spawnPosition = new Vector3();
	}
    public bool SpawnChicken(int chickenID, int skillID)
    {
        //TODO : Attach skill id to chicken ID later
        if (chickenID >= myChicken.Length)
        {
            Debug.Log("[Chicken Spawner] id out of bound");
            return false;
        }
        /*
          only spawning prefab right now
          need to add skill support
         */
        Quaternion chickenRotation = new Quaternion();
        chickenRotation.y = 180;
        Instantiate(myChicken[chickenID], spawnPosition, chickenRotation);
        return true;
    }
}
