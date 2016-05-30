using UnityEngine;
using System.Collections;

public class AbilityRangedAttack : AbilityBaseClass {
    
    GameObject mProjectile;
    public Vector3 mOffset;
    public float mForce;
    private Vector3 mForceVector;

    //Chicken Animator 
    Animator mAnimator;
    int attack = Animator.StringToHash("Attack");
    int damage = Animator.StringToHash("Damage");

    //Chicken Attack Audio
    AudioSource AttackAudio;
    AudioSource DamageAudio;
    // Use this for initialization


    public override void Initialize() 
    {
        mCC = GetComponent<ChickController>();
        
        mForce = 5000.0f;
        mForceVector = new Vector3(1.0f, 0.0f, 0.0f);
        mCDown = 5.0f;//200.0f;
        mAnimator = GetComponent<Animator>();

        //set mProjectile
        mProjectile = ChickenSpawnerManager.Instance.mProjectile;

        GameObject AttackAudioGO = (GameObject)Instantiate(ChickenSpawnerManager.Instance.AttackAudio, gameObject.transform.position, transform.rotation);
        AttackAudioGO.transform.parent = transform;
        AttackAudio = AttackAudioGO.GetComponent<AudioSource>();

        GameObject DamageAudioGO = (GameObject)Instantiate(ChickenSpawnerManager.Instance.DamageAudio, gameObject.transform.position, transform.rotation);
        DamageAudioGO.transform.parent = transform;
        DamageAudio = DamageAudioGO.GetComponent<AudioSource>();
    }

    public override void ActivateAbility()
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

        mCC.SetCoolDown(mCDown);
    }
}
