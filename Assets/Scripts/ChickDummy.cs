using UnityEngine;
using System.Collections;

public class ChickDummy : ChickController
{
    Vector3 positionLastUpdate;

    public void UpdatePosition(Vector3 newPos)
    {
        myBody.transform.position = newPos;
        positionLastUpdate = newPos;
        //guess on rotation
    }

}
