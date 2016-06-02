using UnityEngine;
using System.Collections;

public class AbilityForceField : AbilityBaseClass
{
    public GameObject mPrefab;
    public GameObject mShield;
    public GameObject mPlayer;
    public float mForce;
    private Vector3 mForceVector;
    
    
    // Use this for initialization

    public override void Initialize()
    {
        mCC = GetComponent<ChickController>();
        mCDown = 5.0f;//200.0f;
        mShield = GameObject.Instantiate(mPrefab, this.transform.position, this.transform.rotation) as GameObject;
        mShield.transform.parent = mPlayer.transform;
    }

    public override void ActivateAbility()
    {

        mCC.SetCoolDown(mCDown);
    }

}
