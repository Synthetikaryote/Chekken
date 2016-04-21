using UnityEngine;
using System.Collections;

public class SpawnerTest : MonoBehaviour
{
    // this script is only used for testing purposes
	void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            bool result = ChickenSpawnerManager.Instance.SpawnChicken(0, 1, "test");
            if(result)
            {
                Debug.Log("Spawning chicken was successful");
            }
            else
            {
                Debug.Log("Spawning chicken failed");
            }
        }
    }
}
