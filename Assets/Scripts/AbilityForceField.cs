using UnityEngine;
using System.Collections;

public class AbilityForceField : AbilityBaseClass
{
    public GameObject mPrefab;
    public GameObject mShield;
    public GameObject mPlayer;
    public float mForce;
    private Vector3 mForceVector;
    private float lifetime;

    // Use this for initialization

    public override void Initialize()
    {
        mCC = GetComponent<ChickController>();
        lifetime = 10.0f;
        mCDown = 5.0f;
        mPlayer = this.gameObject;
        mPrefab = ChickenSpawnerManager.Instance.mShield;
    }

    public override void ActivateAbility()
    {
        mShield = GameObject.Instantiate(mPrefab, this.transform.position, this.transform.rotation) as GameObject;
        mShield.transform.parent = mPlayer.transform;
        mCC.SetCoolDown(mCDown);
        Destroy(mShield, lifetime);
    }

}

