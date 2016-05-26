using UnityEngine;
using System.Collections;

public class AbilityRangedAttack : AbilityBaseClass {
    
    //public GameObject mPlayer;
    public GameObject mProjectile;
    public Vector3 mOffset;
    public float mForce;
    private Vector3 mForceVector;

    //Chicken Animator 
    Animator mAnimator;
    int attack = Animator.StringToHash("Attack");
    int damage = Animator.StringToHash("Damage");

    //Chicken Attack Audio
    public AudioSource AttackAudio;
    public AudioSource DamageAudio;
    // Use this for initialization
    public void Initialize()
    {
        mCC = GetComponent<ChickController>();
        //mChicken = GameObject.FindGameObjectWithTag("Chick03");
        
        mForce = 5000.0f;
        mForceVector = new Vector3(1.0f, 0.0f, 0.0f);
        mCDown = 5.0f;//200.0f;
        mAnimator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && mCC.GetCoolDown() <= 0.0f)
        {
            mAnimator.SetTrigger(attack);
            AttackAudio.Play();
            switch (mCC.GetDir())
            {
                case 'R':
                    mOffset = new Vector3(3.5f, 1.5f, 0.0f);
                    Vector3 firePositionR = transform.position + mOffset;//transform.forward + mOffset;
                    GameObject bR = GameObject.Instantiate(mProjectile, firePositionR, transform.rotation) as GameObject;
                    if (bR != null)
                    {
                        Rigidbody rb = bR.GetComponent<Rigidbody>();
                        Vector3 force = mForceVector * mForce;
                        rb.AddForce(force);
                    }
                    break;
                case 'L':
                    mOffset = new Vector3(-3.5f, 1.5f, 0.0f);
                    Vector3 firePositionL = transform.position + mOffset;//transform.forward + mOffset;
                    GameObject bL = GameObject.Instantiate(mProjectile, firePositionL, transform.rotation) as GameObject;
                    if (bL != null)
                    {
                        Rigidbody rb = bL.GetComponent<Rigidbody>();
                        Vector3 force = mForceVector * -mForce;
                        rb.AddForce(force);
                    }
                    break;
            }
            #region Teleport Marker Attempt
            //mProjectile = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //mProjectile.transform.position = mPos;
            //mProjectile.AddComponent<Rigidbody>();
            #endregion

            mCC.SetCoolDown(mCDown);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            mAnimator.SetTrigger(damage);
            DamageAudio.Play();

        }
        //mRB = mProjectile.GetComponent<Rigidbody>();
        //mRB.AddForce(mForce);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DestroyImmediate(mProjectile);

        }
    }

}
