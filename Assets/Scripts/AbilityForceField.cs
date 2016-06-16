using UnityEngine;
using System.Collections;

public class AbilityForceField : AbilityBaseClass
{
    public GameObject mPrefab;
    public GameObject mPlayer;
    public float mForce;
    private Vector3 mForceVector;
    public float lifetime = 10f;
    public float lastActivated = 0f;
    public bool mActive;

    // Use this for initialization

    public override void Initialize()
    {
        mCC = GetComponent<ChickController>();
        mCDown = 5.0f;
        mPlayer = this.gameObject;
        mPrefab = ChickenSpawnerManager.Instance.mShield;
        mActive = false;
    }

    public override void ActivateAbility()
    {
        mActive = true;
        mCC.mShield.SetActive(true);
        lastActivated = Time.time;
        mCC.SetCoolDown(mCDown);
        Invoke("DisableShield", lifetime);
    }

    void DisableShield()
    {
        mCC.mShield.SetActive(false);
        mActive = false;
    }

}

