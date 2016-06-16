using UnityEngine;
using System.Collections;

public class AbilityTeleportScript : AbilityBaseClass
{
    private Vector3 mTeleport;
    private GameObject mMarker;
    private Vector3 mTeleportR;
    private Vector3 mTeleportL;
    [SerializeField]
    private Vector3 mDir;

    // Use this for initialization
    public override void Initialize()
    {
        mDir = new Vector3(1, 0, 0);
        mTeleport = new Vector3(15.0f, 0.0f, 0.0f);
        mCC = GetComponent<ChickController>();
        mCDown = 5.0f;
        mTeleportR = mTeleport;
        mTeleportL = -mTeleport;
    }

    // Update is called once per frame

    public override void ActivateAbility()
    {
        mCC.TeleportEffect.Play();
        switch (mCC.GetDir())
        {
            case 'R':

                if (Physics.Raycast(transform.position, mDir, 15))
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

                if (Physics.Raycast(transform.position, mDir * -1.0f, 15))
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
