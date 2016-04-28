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
        }
        else
        {
            Debug.Log("nay");
        }
        Debug.Break();
    }
    public GameObject SpawnChicken(GameObject preFabToSpawn, string pName)
    {
        spawnRotation = Quaternion.Euler(rotation);
        GameObject chickenClone = (GameObject)Instantiate(preFabToSpawn, transform.position, spawnRotation);
        chickenClone.GetComponentInChildren<TextMesh>().text = pName;
        
        if (!GetComponent<NetworkView>().isMine)
        {
            chickenClone.GetComponent<ChickController>().gameObject.SetActive(false);
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
