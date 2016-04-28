using UnityEngine;
using System.Collections;

public class ChickenSpawner : MonoBehaviour
{
    public Vector3 rotation = new Vector3(0.0f, 180.0f, 0.0f);
    public string playerTag = "Player";
    public bool deleteRenderer = true; // if set to true the meshRenderer will be delted during Start()
    Quaternion spawnRotation = new Quaternion();
    bool canSpawn;
    int networkStat;
    uint numChickensInSpawn = 0;
    uint secondaryChickencount = 0; /*   secondary chicken count is apart of a hack to make sure that the numChickensInSpawn
                                    does not become inaccurate due to a chicken being killed while in the spawns trigger*/
    void OnNetworkInstantiate(NetworkMessageInfo info)
    {
        NetworkView nView = GetComponent<NetworkView>();
        if (nView.isMine)
        {
            Debug.Log("yay");
            networkStat = 1;
        }
        else
        {
            Debug.Log("nay");
            networkStat = 0;
        }
        Debug.Break();
    }
    public GameObject SpawnChicken(GameObject preFabToSpawn, int skillID, string pName)
    {
        spawnRotation = Quaternion.Euler(rotation);
        GameObject chickenClone = (GameObject)Instantiate(preFabToSpawn, transform.position, spawnRotation);
        chickenClone.GetComponentInChildren<TextMesh>().text = pName;
        
        if (networkStat == -1 || networkStat == 1)
        {
            chickenClone.GetComponent<ChickController>().gameObject.SetActive(true);
            switch (skillID)
            {
                case 0:
                    Debug.Log("AbilityRangedAttack attached");
                    chickenClone.GetComponent<AbilityRangedAttack>().enabled = true;
                    break;
                case 1:
                    Debug.Log("AbilityTeleportScipt attached");
                    chickenClone.GetComponent<AbilityTeleportScript>().enabled = true;
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
        }
        return chickenClone;
    }

    public bool CanSpawn()
    {
        return canSpawn;
    }

    void Start()
    {
        networkStat = -1;
        if (deleteRenderer)
        {
            MeshRenderer meshRender = GetComponent<MeshRenderer>();
            if (meshRender != null)
            {
                Destroy(meshRender);
            }
        }
        canSpawn = true;
        ChickenSpawner spawner = gameObject.GetComponent<ChickenSpawner>();
        ChickenSpawnerManager.Instance.RegisterSpawner(ref spawner);
    }

    void OnDestroy()
    {
        ChickenSpawner spawner = gameObject.GetComponent<ChickenSpawner>();
        ChickenSpawnerManager.Instance.UnRegisterSpawner(ref spawner);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            ++numChickensInSpawn;
            canSpawn = false;
        }
    }

    // [Hack - cole ]
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            ++secondaryChickencount;
        }
    }

    // [Hack - cole]
    void FixedUpdate()
    {
        if (secondaryChickencount != numChickensInSpawn)
        {
            numChickensInSpawn = secondaryChickencount;
            canSpawn = numChickensInSpawn == 0;
        }
        secondaryChickencount = 0;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            --numChickensInSpawn;
            canSpawn = numChickensInSpawn == 0;
        }
    }
}
