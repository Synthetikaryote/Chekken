using UnityEngine;
using System.Collections;

public class ChickDummy : ChickController
{
    Vector3 positionLastUpdate;

    protected override void Start()
    {
        base.Start();
        Destroy(myBody);
    }

    public void UpdatePosition(Vector3 newPos)
    {
        positionLastUpdate = newPos;
        //guess on rotation
    }

    public void ActivateAbility()
    {
        ability.ActivateAbility();
    }

    void FixedUpdate()
    {
        transform.position = positionLastUpdate;
    }
}
