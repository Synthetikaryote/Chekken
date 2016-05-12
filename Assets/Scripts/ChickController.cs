using UnityEngine;
using System.Collections;

public class ChickController : MonoBehaviour
{
    //horizontal movement
    public float accelFactor;
    public float initSpeed;
    public float maxSpeed;
    bool IsAlive = false;

    public float rotationSpeedMultipier = 6.0f;
    float radLerpValue;
    float tExample = 0.0f;

    //jumping
    public float jumpHeight;
    public float doubJumpHeight;
    private const float detectDist = 0.5f;

    private bool grounded;
    private bool hasAirJump;

    private Rigidbody myBody;
    private Collider myRenderer;

    //public GameObject FeaterExplosion;
    public ParticleSystem JumpEffect;
    public ParticleSystem ExplosionEffect;

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

    void Start()
    {
        myBody = gameObject.GetComponent<Rigidbody>();
        myRenderer = gameObject.GetComponent<Collider>();
        grounded = true;
        hasAirJump = true;
    }

	void Update ()
    {
        //Cooldown Timer: It takes a cooldown value from an Ability script, and subtracts it until it reaches zero. The ability script cannot fire off again until it reaches zero.
        #region Cooldown
        if (mCooldown > 0.0f)
        {
            mCooldown = mCooldown - Time.deltaTime;
        }
        //Debug.Log(mCooldown);
        #endregion

        //ground Detection
        Vector3 myPos = gameObject.transform.position + new Vector3(0.0f, 0.5f, 0.0f);
        Vector3 modifier = new Vector3(myRenderer.bounds.size.x * 0.5f, 0.0f, 0.0f);
        Ray leftRay = new Ray(myPos + modifier, Vector3.down);
        Ray rightRay = new Ray( myPos - modifier, Vector3.down );

        Debug.DrawRay(leftRay.origin, leftRay.direction, Color.red);
        Debug.DrawRay( rightRay.origin, leftRay.direction, Color.red );

        if( Physics.Raycast( leftRay, detectDist) || Physics.Raycast(rightRay, detectDist))
        {
            grounded = true;
            hasAirJump = true;
        }
        else
        {
            grounded = false;
        }

        //Horizontal Movement
        if( Input.GetButton( "Horizontal" ) )
        {
            float newSpeed;
            //this tests if the the player is still pressing the button in the same direction. 
            if (Input.GetAxisRaw("Horizontal") * myBody.velocity.x > 0)
            {
                newSpeed = Mathf.Max( initSpeed, Mathf.Abs( myBody.velocity.x ) );
            }
            else
            {
                newSpeed = initSpeed;
                tExample = 0.0f;
            }

            newSpeed = Mathf.Min( maxSpeed, newSpeed + (accelFactor * Time.deltaTime) );
            myBody.velocity = new Vector3(newSpeed * Input.GetAxisRaw("Horizontal"), myBody.velocity.y, 0.0f);

            //rotation
            tExample += Time.deltaTime;
            radLerpValue = Mathf.Lerp(0.0f, Mathf.PI * 0.15f, tExample * rotationSpeedMultipier) * Input.GetAxisRaw("Horizontal");
            this.transform.rotation = new Quaternion(this.transform.rotation.x, 1.0f, 0.0f, radLerpValue);

            AnimateCharacter("move");

            if (Input.GetKey(KeyCode.A))
            {
                mDir = 'L';
            }
            else if(Input.GetKey(KeyCode.D))
            {
                mDir = 'R';
            }
        }
        else
        {
            myBody.velocity = new Vector3( 0.0f, myBody.velocity.y, 0.0f );
            //this.transform.rotation = new Quaternion(0, 1, 0, radLerpValue);
            radLerpValue = 0.0f;
        }
        
        //jumping
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                JumpEffect.Play();
                JumpAudio.Play();
                grounded = false;
                myBody.AddForce(0.0f, jumpHeight, 0.0f);
                AnimateCharacter("jump");
            }
            else if (hasAirJump)
            {
                JumpEffect.Play();
                JumpAudio.Play();
                hasAirJump = false;
                myBody.velocity = Vector3.zero;
                myBody.AddForce(0.0f,doubJumpHeight, 0.0f);
                AnimateCharacter("jump");
            }
        }

        //Chicken Dead, Chicken Stop moving, Explosion, Disappear, Dead Audio 
        if (Input.GetKeyDown(KeyCode.F1))
        {

            ExplosionEffect.Play();
            ExplosionAudio.Play();
            IsAlive = true;
            this.transform.FindChild("Chick").gameObject.SetActive(false);
            this.GetComponent<BoxCollider>().enabled = false;
            AnimateCharacter("dead");
            //anim.SetTrigger(die);
        }
        if (IsAlive)
        {
            if (!ExplosionEffect.IsAlive())
            {
                ChickenSpawnerManager.Instance.ChickenIsDead();
                Destroy(this.gameObject);
            }
        }
    }

    void AnimateCharacter(string animState)
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
