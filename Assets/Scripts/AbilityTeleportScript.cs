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
    void Start()
    {
        //mChicken = GameObject.FindGameObjectWithTag("Chick03");
        mPos = mPlayer.transform.position;
        mCC = GetComponent<ChickController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.X) && mCC.mCooldown == 0.0f)
        {
            //OnDrawGizmosSelected();
            mPos = mPos + mTeleport;
            mPlayer.transform.position = mPos;
            mCC.mCooldown = mCDown;
        }
        mPos = mPlayer.transform.position;
    }

    //public void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(transform.position, 1);
    //}
}
