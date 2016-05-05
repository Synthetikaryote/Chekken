using UnityEngine;
using System.Collections;

public class AbilityTeleportScript : MonoBehaviour
{

    public GameObject mPlayer;
    public Vector3 mTeleport;
    private Vector3 mPos;
    private GameObject mMarker;
    public float mCDown;
    private ChickController mCC;
    // Use this for initialization
    public void Initialize()
    {
        //mChicken = GameObject.FindGameObjectWithTag("Chick03");
        mPlayer = gameObject;
        mPos = mPlayer.transform.position;
        mCC = GetComponent<ChickController>();
        mCDown = 90.0f;
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        if (Input.GetKeyDown(KeyCode.X) && mCC.mCooldown == 0.0f)
=======
        if (Input.GetKeyUp(KeyCode.X) && mCC.mCooldown <= 0.0f)
>>>>>>> origin/master
        {
            //OnDrawGizmosSelected();
            mPos = mPos + mTeleport;
            mPlayer.transform.position = mPos;
            mCC.mCooldown = mCDown;
        }
<<<<<<< HEAD

        mPos = mPlayer.transform.position;
=======
        mPos = transform.position;
>>>>>>> origin/master
    }

    //public void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(transform.position, 1);
    //}
}
