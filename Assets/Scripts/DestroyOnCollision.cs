using UnityEngine;
using System.Collections;

public class DestroyOnCollision : MonoBehaviour
{

    public void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
