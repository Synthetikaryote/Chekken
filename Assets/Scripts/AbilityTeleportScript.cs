using UnityEngine;
using System.Collections;

public class AbilityTeleportScript : MonoBehaviour
{

    public GameObject mPlayer;
    private Vector3 mTeleport;
    private Vector3 mPos;
    private GameObject mMarker;
    public float mCDown;
    private ChickController mCC;
    private Vector3 mTeleportR;
    private Vector3 mTeleportL;
    // Use this for initialization
    public void Initialize()
    {
        mTeleport = new Vector3(15.0f, 0.0f, 0.0f);
        mPlayer = gameObject;
        mPos = mPlayer.transform.position;
        mCC = GetComponent<ChickController>();
        mCDown = 5.0f;//90.0f;
        mTeleportR = mTeleport;
        mTeleportL = -mTeleport;
        mCDown = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.X) && mCC.mCooldown <= 0.0f)
        {
            //OnDrawGizmosSelected();

            switch(mCC.mDir)
            {
                case 'R':
                    mPos = mPos + mTeleportR;
                    mPlayer.transform.position = mPos;
                    mCC.mCooldown = mCDown;
                    break;
                case 'L':
                    mPos = mPos + mTeleportL;
                    mPlayer.transform.position = mPos;
                    mCC.mCooldown = mCDown;
                    break;
            }
        }
        mPos = transform.position;
    }

    //public void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(transform.position, 1);
    //}
}
