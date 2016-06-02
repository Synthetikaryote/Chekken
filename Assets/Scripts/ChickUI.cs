using UnityEngine;
using System.Collections;

public class ChickUI : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;

	// Use this for initialization
	void Start ()
    {
        //This will ensure that the UI elements are spawned with the appropiate chick,
        //then unparented from the chick afterwards
        transform.parent = null;
        target.GetComponent<ChickController>().ui = this;
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = target.transform.position + offset;
    }
}
