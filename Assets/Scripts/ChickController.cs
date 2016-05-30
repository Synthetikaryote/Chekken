﻿using UnityEngine;
using System.Collections;

public class ChickController : MonoBehaviour
{

    protected bool IsAlive = false;

    protected float rotationSpeedMultipier = 3.0f;
    protected float radLerpValue = 6.0f;

    protected AbilityBaseClass ability;

    //jumping
    protected float jumpHeight = 1500.0f;
    protected float doubJumpHeight = 500.0f;

    protected Rigidbody myBody;
    protected Collider myRenderer;

    //Particle System
    //public GameObject FeaterExplosion;
    public ParticleSystem JumpEffect;
    public ParticleSystem ExplosionEffect;

    //Audio
    public AudioSource JumpAudio;
    public AudioSource ExplosionAudio;

    //Attack
    public float damageRate;

    //Ability Cooldown
    protected float mCooldown;
    public float GetCoolDown() { return mCooldown; }
    public void SetCoolDown(float cooldown) { mCooldown = cooldown;  }

    //Chicken Direction
    //Direction of the player, it is 'L' for Left, or 'R' for Right
    public char GetDir()
    { return gameObject.transform.rotation.w >= 0 ? 'R' : 'L'; }

    public void AddVelocity(Vector3 vel)
    {
        //additionalVelocity = vel;
    }

    protected virtual void Start()
    {
        myBody = gameObject.GetComponent<Rigidbody>();
        myRenderer = gameObject.GetComponent<Collider>();

        ability = GetComponent<AbilityBaseClass>();

        #region InstatiateEffectsAndAudio
        //these should be done in a for loop in a vector. 
        GameObject jumpEffectGO = (GameObject)Instantiate(ChickenSpawnerManager.Instance.JumpEffect, gameObject.transform.position, transform.rotation);
        jumpEffectGO.transform.parent = transform;
        JumpEffect = jumpEffectGO.GetComponent<ParticleSystem>();

        GameObject ExplosionEffectGO = (GameObject)Instantiate(ChickenSpawnerManager.Instance.ExplosionEffect, gameObject.transform.position, transform.rotation);
        ExplosionEffectGO.transform.parent = transform;
        ExplosionEffect = ExplosionEffectGO.GetComponent<ParticleSystem>();

        GameObject JumpAudioGO = (GameObject)Instantiate(ChickenSpawnerManager.Instance.JumpAudio, gameObject.transform.position, transform.rotation);
        JumpAudioGO.transform.parent = transform;
        JumpAudio = JumpAudioGO.GetComponent<AudioSource>();

        GameObject ExplosionAudioGO = (GameObject)Instantiate(ChickenSpawnerManager.Instance.ExplosionAudio, gameObject.transform.position, transform.rotation);
        ExplosionAudioGO.transform.parent = transform;
        ExplosionAudio = ExplosionAudioGO.GetComponent<AudioSource>();
        #endregion
    }

    protected virtual void Update ()
    {
        //Cooldown Timer: It takes a cooldown value from an Ability script, and subtracts it until it reaches zero. The ability script cannot fire off again until it reaches zero.
        #region Cooldown
        if (mCooldown > 0.0f)
        {
            mCooldown = mCooldown - Time.deltaTime;
        }
        //Debug.Log(mCooldown);
        #endregion

    }

    protected void KillChicken()
    {
        ExplosionEffect.Play();
        ExplosionAudio.Play();
        IsAlive = false;
        this.transform.FindChild("Chick").gameObject.SetActive(false);
        this.GetComponent<BoxCollider>().enabled = false;
        AnimateCharacter("dead");
        //anim.SetTrigger(die);
    }

    protected void AnimateCharacter(string animState)
    {
        string passedString = "animation";
        switch (animState.ToLower())
        {
            case "idle":
                passedString += "1";
                break;
            case "move":
                passedString += "2";
                break;
            case "attack":
                passedString += "3";
                break;
            case "damage":
                passedString += "4";
                break;
            case "die":
                passedString += "5";
                break;
        }
    }

    protected void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            //Check if velocities are matching
            if (myBody.velocity == col.gameObject.GetComponent<Rigidbody>().velocity)
            {
                //Deflect each other
            }

            //Calculate damage based on velocity
            float damage = Mathf.Abs(damageRate * myBody.velocity.x);
            //Apply damage to other chick
            col.gameObject.GetComponent<HealthSystem>().TakeDamage(damage);
        }
    }
}
