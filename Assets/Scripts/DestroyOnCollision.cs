using UnityEngine;
using System.Collections;

public class DestroyOnCollision : MonoBehaviour
{

    //void Start()
    //{ 
    //    public GameObject mEggEffect = (GameObject)Instantiate(ChickenSpawnerManager.Instance.JumpEffect, gameObject.transform.position, transform.rotation);
    //}
    //
    public void OnCollisionEnter(Collision collision)
    {
        this.GetComponent<SphereCollider>().enabled = false;
        this.GetComponent<MeshRenderer>().enabled = false;
        Destroy(this.gameObject, 10.0f);
    }

}