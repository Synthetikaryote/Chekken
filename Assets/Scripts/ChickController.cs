using UnityEngine;
using System.Collections;

public class ChickController : MonoBehaviour
{
    
    protected bool IsAlive = false;

    public float rotationSpeedMultipier = 6.0f;
    protected float radLerpValue;

    //jumping
    public float jumpHeight;
    public float doubJumpHeight;

    protected Rigidbody myBody;
    protected Collider myRenderer;

    //Particle System
    //public GameObject FeaterExplosion;
    public ParticleSystem JumpEffect;
    public ParticleSystem ExplosionEffect;

    //Audio
    public AudioSource JumpAudio;
    public AudioSource ExplosionAudio;

    //Ability Cooldown
    public float mCooldown;
    
    //Chicken Direction
    public char mDir; //Direction of the player, it is 'L' for Left, or 'R' for Right
    
    public void AddVelocity(Vector3 vel)
    {
        //additionalVelocity = vel;
    }

    protected void Start()
    {
        myBody = gameObject.GetComponent<Rigidbody>();
        myRenderer = gameObject.GetComponent<Collider>();
    }

    protected void Update ()
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
}
