using UnityEngine;
using System.Collections;

public class AbilityTornadoScript : MonoBehaviour {

    private ChickController mCC;
    public Vector3 mForce;
    Animator mAnimator;
    public float mCDown;
    private bool mActive;
    private Rigidbody mPlayRigidBody;
    public float mDuration;

    public void Initialize()
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
    void Update () {
        if (Input.GetKeyDown(KeyCode.C) && mCC.mCooldown <= 0.0f)
        {
            mCC.JumpEffect.Play();
            mCC.JumpAudio.Play();
            mPlayRigidBody.AddForce(0.0f, 2000.0f, 0.0f);
            
            while (mDuration > 0.0f)
            {
                //mPlayer.transform.Rotate(0, 1, 0);
                //tExample += Time.deltaTime;
                //radLerpValue = Mathf.Lerp(0.0f, Mathf.PI * 0.15f, tExample * rotationSpeedMultipier) * Input.GetAxisRaw("Horizontal");
                this.transform.Rotate(Vector3.up, 10f * Time.deltaTime);// 0, 180, 0);
                mDuration = mDuration - 1.0f;
            }
            
        }
        mDuration = 100.0f;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(mActive)
        {
            Debug.Log("Do Damage here");
        }

    }


}
