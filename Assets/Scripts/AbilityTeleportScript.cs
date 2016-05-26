using UnityEngine;
using System.Collections;

public class AbilityTeleportScript : AbilityBaseClass
{
    private Vector3 mTeleport;
    private Vector3 mPos;
    private GameObject mMarker;
    private Vector3 mTeleportR;
    private Vector3 mTeleportL;
    // Use this for initialization
    public void Initialize()
    {
        mTeleport = new Vector3(15.0f, 0.0f, 0.0f);
        mPos = transform.position;
        mCC = GetComponent<ChickController>();
        mCDown = 5.0f;//90.0f;
        mTeleportR = mTeleport;
        mTeleportL = -mTeleport;
        mCDown = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.X) && mCC.GetCoolDown() <= 0.0f)
        {

            switch(mCC.GetDir())
            {
                case 'R':
                    Vector3 right = new Vector3(1, 0, 0);

                    if (Physics.Raycast(transform.position, right, 15)) 
                    {
                        print("There is something in front of the object!");
                    }
                    else
                    {
                        mPos = mPos + mTeleportR;
                        transform.position = mPos;
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
                        mPos = mPos + mTeleportL;
                        transform.position = mPos;
                        mCC.SetCoolDown(mCDown);
                    }
                    break;
            }
        }
        mPos = transform.position;
    }

}
