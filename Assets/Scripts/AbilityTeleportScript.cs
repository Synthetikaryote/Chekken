using UnityEngine;
using System.Collections;

public class AbilityTeleportScript : AbilityBaseClass
{
    private Vector3 mTeleport;
    private GameObject mMarker;
    private Vector3 mTeleportR;
    private Vector3 mTeleportL;

    // Use this for initialization
    public override void Initialize()
    {
        mTeleport = new Vector3(15.0f, 0.0f, 0.0f);
        mCC = GetComponent<ChickController>();
        mCDown = 5.0f;//90.0f;
        mTeleportR = mTeleport;
        mTeleportL = -mTeleport;
    }

    // Update is called once per frame

    public override void ActivateAbility()
    {
        switch (mCC.GetDir())
        {
            case 'R':
                Vector3 right = new Vector3(1, 0, 0);

                if (Physics.Raycast(transform.position, right, 15))
                {
                    print("There is something in front of the object!");
                }
                else
                {
                    transform.position += mTeleportR;
                    mCC.SetCoolDown(mCDown);
                }

                break;
            case 'L':
                Vector3 left = new Vector3(-1, 0, 0);

                if (Physics.Raycast(transform.position, left, 15))
                {
                    print("There is something in front of the object!");
                }
                else
                {
                    transform.position += mTeleportL;
                    mCC.SetCoolDown(mCDown);
                }
                break;
        }
    }
}
