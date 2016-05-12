using UnityEngine;
using System.Collections;

public class AbilityRangedAttack : MonoBehaviour {

    public float mCDown;
    //public GameObject mPlayer;
    private Vector3 mPos;
    public GameObject mProjectile;
    public Vector3 mOffset;
    private ChickController mCC;
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
        mPos = transform.position;
        mOffset = new Vector3(5.0f, 0.0f, 0.0f);
        mForce = 5000.0f;
        mForceVector = new Vector3(1.0f, 0.0f, 0.0f);
        mCDown = 5.0f;
        mAnimator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        mPos =transform.position + mOffset;
        if (Input.GetKeyDown(KeyCode.Z) && mCC.mCooldown <= 0.0f)
        {
            mAnimator.SetTrigger(attack);
            AttackAudio.Play();
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
