using UnityEngine;
using System.Collections;

public class MoveAlongPath : MonoBehaviour
{
    public GameObject[] pathNodes;
    public float minDistBeforeswitchingNodes = 0.5f;
    public float speed;
    public string playerTag = "Player";

    int curIndex;
    Vector3 dir;

    void OnEnable()
    {
        if(pathNodes.Length < 2)
        {
            Debug.LogError("[MoveAlongPath] must have 2 or more path nodes");
            gameObject.SetActive(false);
        }
        curIndex = 0;
        dir = new Vector3( 1, 0, 0);
    }
	
	void Update ()
    {
        float distFromTargetSqr = Vector3.SqrMagnitude(transform.position - pathNodes[curIndex].transform.position);
        if(distFromTargetSqr < (minDistBeforeswitchingNodes * minDistBeforeswitchingNodes))
        {
            curIndex = (curIndex + 1)%pathNodes.Length;
        }
        dir = pathNodes[curIndex].transform.position - transform.position;
        dir.Normalize();
        transform.position += (dir * speed * Time.deltaTime);
	}

    void OnCollisionStay(Collision other)
    {
        if (other.collider.CompareTag(playerTag))
        {
            ChickController otherChickContorller = other.collider.GetComponent<ChickController>();
            otherChickContorller.AddVelocity(dir * speed);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag(playerTag))
        {
            ChickController otherChickContorller = other.collider.GetComponent<ChickController>();
            otherChickContorller.AddVelocity(Vector3.zero);
        }
    }
}
