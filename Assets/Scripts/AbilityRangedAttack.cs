using UnityEngine;
using System.Collections;

public class AbilityRangedAttack : MonoBehaviour {

    public float mCDown;
    public GameObject mPlayer;
    private Vector3 mPos;
    public GameObject mProjectile;
    public Vector3 mOffset;
    private ChickController mCC;
    public float mForce;
    private Vector3 mForceVector;

    // Use this for initialization
    void Start()
    {
        mCC = GetComponent<ChickController>();
        //mChicken = GameObject.FindGameObjectWithTag("Chick03");
        mPos = mPlayer.transform.position;
        mForceVector = new Vector3(1.0f, 0.0f, 0.0f);
        mCDown = 200.0f;
    }

    // Update is called once per frame
    void Update()
    {
        mPos = mPlayer.transform.position + mOffset;
        if (Input.GetKeyUp(KeyCode.Z) && mCC.mCooldown == 0.0f)
        {
            Vector3 firePosition = transform.position + mOffset;//transform.forward + mOffset;
            GameObject b = GameObject.Instantiate(mProjectile, firePosition, transform.rotation) as GameObject;
            //mProjectile = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //mProjectile.transform.position = mPos;
            //mProjectile.AddComponent<Rigidbody>();
            if (b != null)
            {
                Rigidbody rb = b.GetComponent<Rigidbody>();
                Vector3 force = mForceVector * mForce;
                rb.AddForce(force);
            }

            mCC.mCooldown = mCDown;
            
        }
        //mRB = mProjectile.GetComponent<Rigidbody>();
        //mRB.AddForce(mForce);
        if(Input.GetKeyUp(KeyCode.Q))
        {
            DestroyImmediate(mProjectile);

        }
    }

}
