using UnityEngine;
using System.Collections;

public class AbilityTornadoScript : AbilityBaseClass
{    
    public Vector3 mForce;
    Animator mAnimator;
    private bool mActive;
    private Rigidbody mPlayRigidBody;
    public float mDuration;
    public float mDegrees;
    public Vector3 spin;

    public override void Initialize()
    {
        mCC = GetComponent<ChickController>();
        mForce = new Vector3(0.0f, 100.0f, 0.0f);
        //mForceVector = new Vector3(1.0f, 0.0f, 0.0f);
        mCDown = 5.0f;
        mAnimator = GetComponent<Animator>();
        mPlayRigidBody = GetComponent<Rigidbody>();
        mActive = false;
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

        this.transform.Rotate(spin * mDuration);
        mActive = true;
    }
}
