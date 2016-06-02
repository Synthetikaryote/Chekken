using UnityEngine;
using System.Collections;

public class AbilityForceField : AbilityBaseClass
{
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
        mCDown = 5.0f;//200.0f;
        
    }

    public override void ActivateAbility()
    {
        
    }

}
