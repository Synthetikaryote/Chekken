using UnityEngine;
using System.Collections;

public class AbilityForceField : AbilityBaseClass
{
    public GameObject mPrefab;
    public GameObject mShield;
    public GameObject mPlayer;
    public float mForce;
    private Vector3 mForceVector;
    public float lifetime = 10f;
    public float lastActivated = 0f;

    // Use this for initialization

    public override void Initialize()
    {
        mCC = GetComponent<ChickController>();
        mCDown = 5.0f;
        mPlayer = this.gameObject;
        mPrefab = ChickenSpawnerManager.Instance.mShield;
    }

    public override void ActivateAbility()
    {
        mShield = GameObject.Instantiate(mPrefab, this.transform.position, this.transform.rotation) as GameObject;
        mShield.transform.parent = mPlayer.transform;
        lastActivated = Time.time;
        mCC.SetCoolDown(mCDown);
        Destroy(mShield, lifetime);
    }

}

