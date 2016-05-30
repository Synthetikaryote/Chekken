using UnityEngine;
using System.Collections;

public class AbilityTornadoScript : AbilityBaseClass
{
    
    public Vector3 mForce;
    Animator mAnimator;
    private bool mActive;
    private Rigidbody mPlayRigidBody;
    public float mDuration;

    public Vector3 spin = Vector3.zero;

    public override void Initialize()
    {
        mCC = GetComponent<ChickController>();
        mForce = new Vector3(0.0f, 100.0f, 0.0f);
        //mForceVector = new Vector3(1.0f, 0.0f, 0.0f);
        mCDown = 5.0f;
        mAnimator = GetComponent<Animator>();
        mPlayRigidBody = GetComponent<Rigidbody>();
        mActive = false;
        mDuration = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (mActive)
        {
            if(mDuration > 0.0f)
            {
                //mPlayer.transform.Rotate(0, 1, 0);
                //tExample += Time.deltaTime;
                //radLerpValue = Mathf.Lerp(0.0f, Mathf.PI * 0.15f, tExample * rotationSpeedMultipier) * Input.GetAxisRaw("Horizontal");
                this.transform.Rotate(Vector3.up, 10f * Time.deltaTime);// 0, 180, 0);
                mDuration = mDuration - Time.deltaTime;
            }
            else
            {
                mDuration = 100.0f;
                mActive = false;
                mCC.SetCoolDown(mCDown);
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(mActive)
        {
            Debug.Log("Do Damage here");
        }

    }

    public override void ActivateAbility()
    {
        mCC.JumpEffect.Play();
        mCC.JumpAudio.Play();
        mPlayRigidBody.AddForce(0.0f, 2000.0f, 0.0f);

        this.transform.Rotate(spin * 5000.0f);
        mActive = true;
    }
}
